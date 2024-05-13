using System;
using System.Linq;
using System.Threading;
using ClusterVR.CreatorKit.Editor.Api.RPC;
using ClusterVR.CreatorKit.Editor.Api.User;
using ClusterVR.CreatorKit.Editor.Api.Venue;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Window.View
{
    public sealed class EditVenueView : IDisposable
    {
        readonly Reactive<bool> reactiveEdited = new Reactive<bool>();

        readonly ImageView thumbnailView;
        readonly UserInfo userInfo;
        readonly Action venueChangeCallback;
        readonly VisualElement view;

        Venue venue;
        string errorMessage;
        string newThumbnailPath;
        string newVenueDesc;
        string newVenueName;
        bool updatingVenue;

        IDisposable disposable;
        readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        public EditVenueView(UserInfo userInfo, ImageView thumbnailView, Action venueChangeCallback)
        {
            this.userInfo = userInfo;
            this.venueChangeCallback = venueChangeCallback;

            this.thumbnailView = thumbnailView;
            view = new VisualElement();
        }

        public void SetVenue(Venue venue)
        {
            Assert.IsNotNull(venue);
            this.venue = venue;
            newVenueName = venue.Name;
            newVenueDesc = venue.Description;
            var thumbnailUrl = venue.ThumbnailUrls.FirstOrDefault(x => x != null);
            thumbnailView.SetImageUrl(thumbnailUrl ?? new ThumbnailUrl(""));
            UpdateView();
        }

        public VisualElement CreateView() => view;

        void UpdateView()
        {
            view.Clear();
            var topSection = new VisualElement() { style = { flexDirection = FlexDirection.Row } };
            view.Add(topSection);

            {
                var thumbnailSection = new VisualElement();

                thumbnailSection.Add(thumbnailView.CreateView());

                var changeImageButton = new Button(() =>
                {
                    if (!updatingVenue)
                    {
                        newThumbnailPath =
                            EditorUtility.OpenFilePanelWithFilters(
                                "画像を選択",
                                "",
                                new[] { "Image files", "png,jpg,jpeg", "All files", "*" }
                            );
                        thumbnailView.SetImagePath(newThumbnailPath);
                        UpdateVenue();
                    }
                })
                {
                    text = "画像の選択",
                    style =
                    {
                        marginTop = 4
                    }
                };
                thumbnailSection.Add(changeImageButton);
                thumbnailSection.Add(new Label() { text = "※推奨サイズ：1920×1080px" });

                topSection.Add(thumbnailSection);
            }

            {
                var editSection = new VisualElement() { style = { flexGrow = 1, marginLeft = 8 } };

                var venueIdSection = new VisualElement() { style = { flexDirection = FlexDirection.Row } };
                venueIdSection.Add(new Label($"ワールドID {venue.VenueId.Value}")
                {
                    style = { color = new StyleColor(Color.gray) }
                });
                venueIdSection.Add(new Button(() => EditorGUIUtility.systemCopyBuffer = venue.VenueId.Value)
                {
                    text = "copy",
                    style = { height = 16 }
                });

                editSection.Add(venueIdSection);

                editSection.Add(new Label("ワールド名")
                {
                    style = { marginTop = 4 }
                });
                var venueName = new TextField();
                venueName.value = venue.Name;
                venueName.RegisterValueChangedCallback(ev =>
                {
                    newVenueName = ev.newValue;
                    reactiveEdited.Val = true;
                });
                editSection.Add(venueName);

                editSection.Add(new Label("ワールドの説明") { style = { marginTop = 4 } });
                var venueDesc = new TextField
                {
                    multiline = true,
                    style =
                    {
                        height = 40,
                        unityTextAlign = TextAnchor.UpperLeft
                    }
                };
                foreach (var child in venueDesc.Children())
                {
                    child.style.unityTextAlign = TextAnchor.UpperLeft;
                }

                venueDesc.value = venue.Description;
                venueDesc.RegisterValueChangedCallback(ev =>
                {
                    newVenueDesc = ev.newValue;
                    reactiveEdited.Val = true;
                });
                editSection.Add(venueDesc);
                editSection.Add(new Label
                {
                    text = "※255文字以内で入力してください"
                });

                var buttons = new VisualElement
                {
                    style =
                    {
                        flexDirection = FlexDirection.Row
                    }
                };
                var applyEdit = new Button(() =>
                {
                    if (!updatingVenue)
                    {
                        UpdateVenue();
                    }
                })
                {
                    text = "変更を保存"
                };
                var cancelEdit = new Button(() =>
                {
                    venueName.SetValueWithoutNotify(venue.Name);
                    venueDesc.SetValueWithoutNotify(venue.Description);
                    reactiveEdited.Val = false;
                })
                {
                    text = "キャンセル"
                };
                buttons.Add(applyEdit);
                buttons.Add(cancelEdit);
                disposable?.Dispose();
                disposable = ReactiveBinder.Bind(reactiveEdited,
                    edited => { buttons.style.display = edited ? DisplayStyle.Flex : DisplayStyle.None; });

                editSection.Add(buttons);

                editSection.Add(new IMGUIContainer(() =>
                {
                    if (!string.IsNullOrEmpty(errorMessage))
                    {
                        EditorGUILayout.HelpBox(errorMessage, MessageType.Error);
                    }
                }));

                topSection.Add(editSection);
            }
        }

        void UpdateVenue()
        {
            updatingVenue = true;

            var patchVenueService = new PatchVenueSettingService(
                userInfo.VerifiedToken,
                venue,
                newVenueName,
                newVenueDesc,
                venue.ThumbnailUrls.ToList(),
                newThumbnailPath,
                _ =>
                {
                    updatingVenue = false;
                    venueChangeCallback();
                },
                exception =>
                {
                    updatingVenue = false;
                    errorMessage = $"ワールド情報の保存に失敗しました。{exception.Message}";
                });
            patchVenueService.Run(cancellationTokenSource.Token);
            errorMessage = null;
        }

        public void Dispose()
        {
            thumbnailView?.Dispose();
            disposable?.Dispose();
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
        }
    }
}
