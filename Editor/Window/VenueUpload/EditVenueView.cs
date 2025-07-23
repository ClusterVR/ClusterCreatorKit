using System;
using ClusterVR.CreatorKit.Editor.Api.Venue;
using ClusterVR.CreatorKit.Editor.Utils;
using ClusterVR.CreatorKit.Translation;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Window.VenueUpload
{
    public sealed class EditVenueView : VisualElement
    {
        readonly ImageView thumbnailView;
        readonly Label venueIdLabel;
        readonly TextField venueNameField;
        readonly TextField venueDescField;
        readonly VisualElement actionsContainer;

        Venue originalVenue;
        string errorMessage;

        Action onChangeImageButtonClicked;
        Action<string> onVenueNameFieldChanged;
        Action<string> onVenueDescFieldChanged;
        Action onApplyEditButtonClicked;
        Action onCancelEditButtonClicked;

        public EditVenueView()
        {
            var topSection = new VisualElement { style = { flexDirection = FlexDirection.Row } };
            Add(topSection);

            {
                var thumbnailSection = new VisualElement();

                thumbnailView = new ImageView();
                thumbnailSection.Add(thumbnailView);

                var changeImageButton = new Button
                {
                    text = TranslationTable.cck_image_selection,
                    style =
                    {
                        marginTop = 4
                    }
                };
                thumbnailSection.Add(changeImageButton);
                thumbnailSection.Add(new Label { text = TranslationTable.cck_recommended_image_size });

                topSection.Add(thumbnailSection);

                changeImageButton.clicked += () => onChangeImageButtonClicked?.Invoke();
            }

            {
                var editSection = new VisualElement { style = { flexGrow = 1, marginLeft = 8 } };

                var venueIdSection = new VisualElement { style = { flexDirection = FlexDirection.Row } };
                venueIdLabel = new Label
                {
                    style = { color = new StyleColor(Color.gray) }
                };
                venueIdSection.Add(venueIdLabel);
                var copyVenueIdButton = new Button
                {
                    text = TranslationTable.cck_copy,
                    style = { height = 16 }
                };
                venueIdSection.Add(copyVenueIdButton);
                copyVenueIdButton.clicked += CopyVenueId;

                editSection.Add(venueIdSection);

                editSection.Add(new Label(TranslationTable.cck_world_name)
                {
                    style = { marginTop = 4 }
                });
                venueNameField = new TextField();
                venueNameField.RegisterValueChangedCallback(ev => onVenueNameFieldChanged.Invoke(ev.newValue));
                editSection.Add(venueNameField);

                editSection.Add(new Label(TranslationTable.cck_world_description) { style = { marginTop = 4 } });
                venueDescField = new TextField
                {
                    multiline = true,
                    style =
                    {
                        height = 40,
                        unityTextAlign = TextAnchor.UpperLeft
                    }
                };
                foreach (var child in venueDescField.Children())
                {
                    child.style.unityTextAlign = TextAnchor.UpperLeft;
                }

                venueDescField.RegisterValueChangedCallback(ev => onVenueDescFieldChanged.Invoke(ev.newValue));
                editSection.Add(venueDescField);
                editSection.Add(new Label
                {
                    text = TranslationTable.cck_description_limit
                });

                actionsContainer = new VisualElement
                {
                    style =
                    {
                        flexDirection = FlexDirection.Row
                    }
                };

                var applyEdit = new Button
                {
                    text = TranslationTable.cck_save_change
                };
                var cancelEdit = new Button
                {
                    text = TranslationTable.cck_cancel
                };
                actionsContainer.Add(applyEdit);
                actionsContainer.Add(cancelEdit);
                editSection.Add(actionsContainer);
                applyEdit.clicked += () => onApplyEditButtonClicked?.Invoke();
                cancelEdit.clicked += () =>
                {
                    venueNameField.SetValueWithoutNotify(originalVenue.Name);
                    venueDescField.SetValueWithoutNotify(originalVenue.Description);
                    onCancelEditButtonClicked?.Invoke();
                };

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

        public IDisposable Bind(EditVenueViewModel viewModel)
        {
            onChangeImageButtonClicked += viewModel.RequestChangeImage;
            onVenueNameFieldChanged += viewModel.SetVenueName;
            onVenueDescFieldChanged += viewModel.SetVenueDesc;
            onApplyEditButtonClicked += viewModel.ApplyEdit;
            onCancelEditButtonClicked += viewModel.CancelEdit;

            return Disposable.Create(() =>
                {
                    onChangeImageButtonClicked -= viewModel.RequestChangeImage;
                    onVenueNameFieldChanged -= viewModel.SetVenueName;
                    onVenueDescFieldChanged -= viewModel.SetVenueDesc;
                    onApplyEditButtonClicked -= viewModel.ApplyEdit;
                    onCancelEditButtonClicked -= viewModel.CancelEdit;
                },
                thumbnailView.Bind(viewModel.Thumbnail),
                ReactiveBinder.Bind(viewModel.Venue, venue => originalVenue = venue),
                ReactiveBinder.Bind(viewModel.VenueId, SetVenueId),
                ReactiveBinder.Bind(viewModel.NewVenueName, venueName =>
                {
                    if (venueNameField.value != venueName) venueNameField.SetValueWithoutNotify(venueName);
                }),
                ReactiveBinder.Bind(viewModel.NewVenueDesc, venueDesc =>
                {
                    if (venueDescField.value != venueDesc) venueDescField.SetValueWithoutNotify(venueDesc);
                }),
                ReactiveBinder.Bind(viewModel.ErrorMessage, errorMessage => this.errorMessage = errorMessage),
                ReactiveBinder.Bind(viewModel.IsDirty, SetIsDirty)
            );
        }

        void SetVenueId(string venueId)
        {
            venueIdLabel.text = TranslationUtility.GetMessage(TranslationTable.cck_venue_id, venueId);
        }

        void SetIsDirty(bool dirty)
        {
            actionsContainer.style.display = dirty ? DisplayStyle.Flex : DisplayStyle.None;
        }

        void CopyVenueId()
        {
            EditorGUIUtility.systemCopyBuffer = originalVenue?.VenueId.Value;
        }
    }
}
