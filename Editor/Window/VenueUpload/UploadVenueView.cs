using System;
using System.Linq;
using ClusterVR.CreatorKit.Editor.Analytics;
using ClusterVR.CreatorKit.Editor.Api.RPC;
using ClusterVR.CreatorKit.Editor.Api.Venue;
using ClusterVR.CreatorKit.Editor.Builder;
using ClusterVR.CreatorKit.Editor.Enquete;
using ClusterVR.CreatorKit.Editor.ProjectSettings;
using ClusterVR.CreatorKit.Editor.Utils;
using ClusterVR.CreatorKit.Translation;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Window.VenueUpload
{
    public sealed class UploadVenueView : VisualElement
    {
        Venue venue;

        BuildSummary currentBuildSummary;
        UploadVenueService currentUploadService;
        string errorMessage;

        event Action OnUploadButtonClicked;
        event Action OnPreviewUploadButtonClicked;
        event Action OnRetryUploadButtonClicked;
        event Action OnOpenWorldDetailButtonClicked;
        event Action OnOpenWorldManagementPageButtonClicked;

        event Action<bool> OnPreviewUploadSelected;
        event Action<bool> OnPreviewUploadWindowsSelected;
        event Action<bool> OnPreviewUploadMacSelected;
        event Action<bool> OnPreviewUploadIOSSelected;
        event Action<bool> OnPreviewUploadAndroidSelected;

        bool previewUploadSelected;
        bool previewUploadWindowsSelected;
        bool previewUploadMacSelected;
        bool previewUploadIOSSelected;
        bool previewUploadAndroidSelected;

        public UploadVenueView()
        {
            hierarchy.Add(new IMGUIContainer(DrawUI));
        }

        public IDisposable Bind(UploadVenueViewModel viewModel)
        {
            OnPreviewUploadSelected += viewModel.SetPreviewUploadSelected;
            OnPreviewUploadWindowsSelected += viewModel.SetPreviewUploadWindowsSelected;
            OnPreviewUploadMacSelected += viewModel.SetPreviewUploadMacSelected;
            OnPreviewUploadIOSSelected += viewModel.SetPreviewUploadIOSSelected;
            OnPreviewUploadAndroidSelected += viewModel.SetPreviewUploadAndroidSelected;
            OnUploadButtonClicked += viewModel.ConfirmUpload;
            OnPreviewUploadButtonClicked += viewModel.ConfirmPreviewUpload;
            OnRetryUploadButtonClicked += viewModel.RetryUpload;
            OnOpenWorldManagementPageButtonClicked += viewModel.OpenWorldManagementPage;
            OnOpenWorldDetailButtonClicked += viewModel.OpenWorldDetailUrlOfCurrentVenue;

            return Disposable.Create(() =>
                {
                    OnPreviewUploadSelected -= viewModel.SetPreviewUploadSelected;
                    OnPreviewUploadWindowsSelected -= viewModel.SetPreviewUploadWindowsSelected;
                    OnPreviewUploadMacSelected -= viewModel.SetPreviewUploadMacSelected;
                    OnPreviewUploadIOSSelected -= viewModel.SetPreviewUploadIOSSelected;
                    OnPreviewUploadAndroidSelected -= viewModel.SetPreviewUploadAndroidSelected;
                    OnUploadButtonClicked -= viewModel.ConfirmUpload;
                    OnPreviewUploadButtonClicked -= viewModel.ConfirmPreviewUpload;
                    OnRetryUploadButtonClicked -= viewModel.RetryUpload;
                    OnOpenWorldManagementPageButtonClicked -= viewModel.OpenWorldManagementPage;
                    OnOpenWorldDetailButtonClicked -= viewModel.OpenWorldDetailUrlOfCurrentVenue;
                },
                ReactiveBinder.Bind(viewModel.PreviewUploadSelected, value => previewUploadSelected = value),
                ReactiveBinder.Bind(viewModel.PreviewUploadWindowsSelected, value => previewUploadWindowsSelected = value),
                ReactiveBinder.Bind(viewModel.PreviewUploadMacSelected, value => previewUploadMacSelected = value),
                ReactiveBinder.Bind(viewModel.PreviewUploadIOSSelected, value => previewUploadIOSSelected = value),
                ReactiveBinder.Bind(viewModel.PreviewUploadAndroidSelected, value => previewUploadAndroidSelected = value),
                ReactiveBinder.Bind(viewModel.Venue, venue => this.venue = venue),
                ReactiveBinder.Bind(viewModel.CurrentBuildSummary, buildSummary => this.currentBuildSummary = buildSummary),
                ReactiveBinder.Bind(viewModel.CurrentUploadService, currentUploadService => this.currentUploadService = currentUploadService),
                ReactiveBinder.Bind(viewModel.ErrorMessage, errorMessage => this.errorMessage = errorMessage));
        }

        void DrawUI()
        {
            if (venue == null)
            {
                return;
            }
            EditorGUILayout.Space();
            EditorGUILayout.HelpBox(TranslationTable.cck_keep_scene_open_for_upload, MessageType.Info);

            var isVenueUploadSettingValid = IsVenueUploadSettingValid(out var uploadSettingErrorMessage);
            if (!isVenueUploadSettingValid)
            {
                EditorGUILayout.HelpBox(uploadSettingErrorMessage, MessageType.Error);
            }

            var betaSettingValid = venue.IsBeta == ClusterCreatorKitSettings.instance.IsBeta;
            if (!betaSettingValid)
            {
                var message = venue.IsBeta
                    ? TranslationTable.cck_beta_feature_world_upload_condition
                    : TranslationTable.cck_nonbeta_feature_world_upload_condition;
                EditorGUILayout.HelpBox(message, MessageType.Error);
            }

            var canUpload = isVenueUploadSettingValid && betaSettingValid;

            using (new EditorGUI.DisabledScope(!canUpload))
            {
                var uploadButton = GUILayout.Button(
                    TranslationUtility.GetMessage(TranslationTable.cck_upload_as_named_venue, (venue.IsBeta ? TranslationTable.cck_beta_features_enabled : ""), venue.Name));
                if (uploadButton)
                {
                    OnUploadButtonClicked?.Invoke();
                }
            }

            var alreadyUploaded = !string.IsNullOrEmpty(venue.WorldDetailUrl);
            if (alreadyUploaded)
            {
                if (GUILayout.Button(TranslationTable.cck_open_world_detail_page))
                {
                    OnOpenWorldDetailButtonClicked?.Invoke();
                }
            }
            else
            {
                if (GUILayout.Button(TranslationTable.cck_open_world_management_page))
                {
                    OnOpenWorldManagementPageButtonClicked?.Invoke();
                }
            }

            EditorGUILayout.Space();

            var canSelectPreviewUpload = canUpload && alreadyUploaded;
            using (new EditorGUI.DisabledScope(!canSelectPreviewUpload))
            {
                var previewSelectAreaStyle = new GUIStyle(EditorStyles.helpBox) { padding = { bottom = 6 } };
                using (new GUILayout.VerticalScope(previewSelectAreaStyle))
                {
                    OnPreviewUploadSelected?.Invoke(GUILayout.Toggle(previewUploadSelected, TranslationTable.cck_upload_for_testing));
                    using (new GUILayout.HorizontalScope())
                    {
                        EditorGUILayout.Space(10f, false);
                        using (new GUILayout.VerticalScope())
                        {
                            var previewUploadDescription = TranslationTable.cck_test_space_available_on_selected_device;
                            GUILayout.Label(previewUploadDescription);
                            if (previewUploadSelected)
                            {
                                using (new EditorGUI.DisabledScope(!previewUploadSelected))
                                {
                                    OnPreviewUploadWindowsSelected?.Invoke(GUILayout.Toggle(previewUploadWindowsSelected, "Windows"));
                                    OnPreviewUploadMacSelected?.Invoke(GUILayout.Toggle(previewUploadMacSelected, "Mac"));
                                    OnPreviewUploadIOSSelected?.Invoke(GUILayout.Toggle(previewUploadIOSSelected, "iOS"));
                                    OnPreviewUploadAndroidSelected?.Invoke(GUILayout.Toggle(previewUploadAndroidSelected, "Android / Quest"));
                                }
                            }
                        }
                    }
                }
            }

            if (!alreadyUploaded)
            {
                var previewDisabledDescription = TranslationTable.cck_upload_required_for_testing;
                GUILayout.Label(previewDisabledDescription);
            }

            if (previewUploadSelected)
            {
                using (new EditorGUI.DisabledScope(!canSelectPreviewUpload))
                {
                    var defaultRichText = EditorStyles.helpBox.richText;
                    var defaultSize = EditorStyles.helpBox.fontSize;
                    EditorStyles.helpBox.richText = true;
                    EditorStyles.helpBox.fontSize = defaultSize + 1;
                    try
                    {
                        EditorGUILayout.HelpBox(TranslationTable.cck_how_to_start_testing, MessageType.Info);
                    }
                    finally
                    {
                        EditorStyles.helpBox.richText = defaultRichText;
                        EditorStyles.helpBox.fontSize = defaultSize;
                    }
                }
            }

            var anyPreviewPlatformSelected = previewUploadWindowsSelected || previewUploadMacSelected || previewUploadIOSSelected || previewUploadAndroidSelected;
            var canPreviewUpload = previewUploadSelected && anyPreviewPlatformSelected;
            using (new EditorGUI.DisabledScope(!canPreviewUpload))
            {
                var previewUploadButton = GUILayout.Button(TranslationTable.cck_upload_for_testing);
                if (previewUploadButton)
                {
                    OnPreviewUploadButtonClicked?.Invoke();
                }
            }

            EditorGUILayout.Space();

            if (!string.IsNullOrEmpty(errorMessage))
            {
                EditorGUILayout.HelpBox(errorMessage, MessageType.Error);
            }

            if (currentUploadService != null)
            {
                ShowUploadInfo();
            }
        }

        void ShowUploadInfo()
        {
            if (!currentUploadService.IsProcessing)
            {
                EditorUtility.ClearProgressBar();

                var succeeded = true;
                foreach (var status in currentUploadService.UploadStatus.OrderBy(x => x.Key))
                {
                    var text = status.Value ? TranslationTable.cck_success : TranslationTable.cck_failed;
                    succeeded &= status.Value;
                    EditorGUILayout.LabelField(status.Key.ToString(), text);
                }

                if (succeeded && EnqueteService.ShouldShowEnqueteRequest())
                {
                    EnqueteService.ShowEnqueteDialog();
                }
            }
            else
            {
                var statesValue = currentUploadService.UploadStatus.Values.ToList();
                var finishedProcessCount = statesValue.Count(x => x);
                var allProcessCount = statesValue.Count;
                EditorUtility.DisplayProgressBar(TranslationTable.cck_venue_upload,
                    TranslationUtility.GetMessage(TranslationTable.cck_upload_progress, finishedProcessCount, allProcessCount),
                    (float) finishedProcessCount / allProcessCount);
            }

            if (!currentUploadService.IsProcessing && currentUploadService.UploadStatus.Values.Any(x => !x))
            {
                if (GUILayout.Button(TranslationTable.cck_upload_retry))
                {
                    OnRetryUploadButtonClicked?.Invoke();
                }
                var helpMessage = TranslationUtility.GetMessage(TranslationTable.cck_use_upload_button_after_scene_edit,
                    (venue.IsBeta ? TranslationTable.cck_beta_features_enabled : ""), venue.Name);
                EditorGUILayout.HelpBox(helpMessage, MessageType.Info);
            }

            EditorGUILayout.Space();

            foreach (var platformSummary in currentBuildSummary.PlatformSummaries)
            {
                ShowBuiltAssetBundleSize(platformSummary);
            }
        }

        bool IsVenueUploadSettingValid(out string uploadSettingErrorMessage)
        {
            if (venue.GetThumbnailUrlForDisplay().IsEmpty())
            {
                uploadSettingErrorMessage = TranslationTable.cck_set_thumbnail_image;
                return false;
            }

            if (EditorApplication.isPlaying)
            {
                uploadSettingErrorMessage = TranslationTable.cck_stop_editor_playback;
                return false;
            }

            uploadSettingErrorMessage = default;
            return true;
        }

        void ShowBuiltAssetBundleSize(PlatformBuildSummary summary)
        {
            var buildTargetName = summary.BuildTarget.DisplayName();
            {
                var size = summary.MainSceneSummary.TotalSize;
                EditorGUILayout.LabelField(TranslationUtility.GetMessage(TranslationTable.cck_main_scene_size, buildTargetName), $"{(double) size / (1024 * 1024):F2} MB"); // Byte => MByte
            }
            var subSceneIndex = 1;
            foreach (var subSceneSummary in summary.SubSceneSummaries)
            {
                var size = subSceneSummary.TotalSize;
                EditorGUILayout.LabelField(TranslationUtility.GetMessage(TranslationTable.cck_sub_scene_size, buildTargetName, subSceneIndex), $"{(double) size / (1024 * 1024):F2} MB"); // Byte => MByte
                ++subSceneIndex;
            }
        }

    }
}
