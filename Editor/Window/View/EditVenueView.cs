using System;
using System.Linq;
using System.Threading;
using ClusterVR.CreatorKit.Editor.Api.RPC;
using ClusterVR.CreatorKit.Editor.Api.User;
using ClusterVR.CreatorKit.Editor.Api.Venue;
using ClusterVR.CreatorKit.Translation;
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
                                TranslationTable.cck_select_image,
                                "",
                                new[] { TranslationTable.cck_image_files, "png,jpg,jpeg", "All files", "*" }
                            );
                        thumbnailView.SetImagePath(newThumbnailPath);
                        UpdateVenue();
                    }
                })
                {
                    text = TranslationTable.cck_image_selection,
                    style =
                    {
                        marginTop = 4
                    }
                };
                thumbnailSection.Add(changeImageButton);
                thumbnailSection.Add(new Label() { text = TranslationTable.cck_recommended_image_size });

                topSection.Add(thumbnailSection);
            }

            {
                var editSection = new VisualElement() { style = { flexGrow = 1, marginLeft = 8 } };

                var venueIdSection = new VisualElement() { style = { flexDirection = FlexDirection.Row } };
                venueIdSection.Add(new Label(TranslationUtility.GetMessage(TranslationTable.cck_venue_id, venue.VenueId.Value))
                {
                    style = { color = new StyleColor(Color.gray) }
                });
                venueIdSection.Add(new Button(() => EditorGUIUtility.systemCopyBuffer = venue.VenueId.Value)
                {
                    text = TranslationTable.cck_copy,
                    style = { height = 16 }
                });

                editSection.Add(venueIdSection);

                editSection.Add(new Label(TranslationTable.cck_world_name)
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

                editSection.Add(new Label(TranslationTable.cck_world_description) { style = { marginTop = 4 } });
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
                    text = TranslationTable.cck_description_limit
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
                    text = TranslationTable.cck_save_change
                };
                var cancelEdit = new Button(() =>
                {
                    venueName.SetValueWithoutNotify(venue.Name);
                    venueDesc.SetValueWithoutNotify(venue.Description);
                    reactiveEdited.Val = false;
                })
                {
                    text = TranslationTable.cck_cancel
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
                    errorMessage = TranslationUtility.GetMessage(TranslationTable.cck_world_info_save_failed, exception.Message);
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
