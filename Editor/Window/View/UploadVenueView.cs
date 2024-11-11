using System;
using System.IO;
using System.Linq;
using System.Threading;
using ClusterVR.CreatorKit.Editor.Analytics;
using ClusterVR.CreatorKit.Editor.Api.RPC;
using ClusterVR.CreatorKit.Editor.Api.User;
using ClusterVR.CreatorKit.Editor.Api.Venue;
using ClusterVR.CreatorKit.Editor.Builder;
using ClusterVR.CreatorKit.Editor.EditorEvents;
using ClusterVR.CreatorKit.Editor.Enquete;
using ClusterVR.CreatorKit.Editor.ProjectSettings;
using ClusterVR.CreatorKit.Editor.Validator;
using ClusterVR.CreatorKit.Editor.VenueInfo;
using ClusterVR.CreatorKit.Proto;
using ClusterVR.CreatorKit.Translation;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Window.View
{
    public sealed class UploadVenueView : IDisposable
    {
        const string InstallUnityUrl = "https://docs.cluster.mu/creatorkit/installation/install-unity/";

        readonly ImageView thumbnail;
        readonly UserInfo userInfo;
        readonly string worldManagementUrl;
        readonly Action venueChangeCallback;
        readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        Venue venue;
        ExportedAssetInfo exportedAssetInfo;
        BuildSummary buildSummary;
        UploadVenueService currentUploadService;
        string errorMessage;
        string tempAssetsDirPath;

        bool previewUploadSelected;
        bool previewUploadWindowsSelected;
        bool previewUploadMacSelected;
        bool previewUploadIOSSelected;
        bool previewUploadAndroidSelected;

        bool executeUpload;
        bool isPreviewUpload;
        bool isBeta;

        DateTime uploadStartAt;

        public UploadVenueView(UserInfo userInfo, ImageView thumbnail, Action venueChangeCallback)
        {
            this.userInfo = userInfo;
            this.thumbnail = thumbnail;
            this.venueChangeCallback = venueChangeCallback;
            worldManagementUrl = Api.RPC.Constants.WebBaseUrl + "/account/worlds";

#if UNITY_EDITOR_WIN
            previewUploadWindowsSelected = true;
#elif UNITY_EDITOR_OSX
            previewUploadMacSelected = true;
#endif
        }

        public void SetVenue(Venue venue)
        {
            Assert.IsNotNull(venue);
            this.venue = venue;
        }

        public VisualElement CreateView()
        {
            return new IMGUIContainer(() =>
            {
                if (venue == null)
                {
                    return;
                }
                Process();
                DrawUI();
            });
        }

        void Process()
        {
            if (executeUpload)
            {
                executeUpload = false;
                currentUploadService = null;

                LogWorldUploadStart();

                if (!WorldUploadEvents.InvokeOnWorldUploadStart(SceneManager.GetActiveScene()))
                {
                    Debug.LogError(TranslationTable.cck_build_stopped_user_defined_callback_error);
                    LogWorldUploadFailed();
                    WorldUploadEvents.InvokeOnWorldUploadEnd(false);
                    return;
                }

                if (!VenueValidator.ValidateVenue(isBeta, out errorMessage, out var invalidObjects))
                {
                    EditorUtility.DisplayDialog("Cluster Creator Kit", errorMessage, TranslationTable.cck_close);
                    if (invalidObjects.Any())
                    {
                        foreach (var invalidObject in invalidObjects)
                        {
                            Debug.LogError(errorMessage, invalidObject);
                            EditorGUIUtility.PingObject(invalidObject);
                        }
                    }
                    else
                    {
                        Debug.LogError(errorMessage);
                    }

                    Selection.objects = invalidObjects.ToArray();
                    LogWorldUploadFailed();
                    WorldUploadEvents.InvokeOnWorldUploadEnd(false);
                    return;
                }

                if (!TryExportAssets(venue, out exportedAssetInfo, out buildSummary))
                {
                    LogWorldUploadFailed();
                    WorldUploadEvents.InvokeOnWorldUploadEnd(false);
                    return;
                }

                var displayIds = VenueInfoConstructor.FindDisplayItemIds();

                currentUploadService = new UploadVenueService(userInfo.VerifiedToken, venue,
                    WorldDescriptorCreator.Create(SceneManager.GetActiveScene()),
                    isBeta, isPreviewUpload,
                    displayIds,
                    exportedAssetInfo,
                    completionResponse =>
                    {
                        DeleteTempAssetsDirectory();
                        errorMessage = "";
                        LogWorldUploadComplete(buildSummary);
                        if (isPreviewUpload)
                        {
                            EditorUtility.DisplayDialog(TranslationTable.cck_test_upload_complete,
                                TranslationTable.cck_start_test_space, TranslationTable.cck_close);
                        }
                        else
                        {
                            var openWorldManagementUrl = EditorUtility.DisplayDialog(
                                TranslationTable.cck_upload_complete, TranslationTable.cck_upload_complete,
                                TranslationTable.cck_open_world_management_page, TranslationTable.cck_close);
                            if (openWorldManagementUrl)
                            {
                                Application.OpenURL(worldManagementUrl);
                                PanamaLogger.LogCckOpenLink(worldManagementUrl, "UploadVenueView_UploadComplete");
                            }
                        }
                        venueChangeCallback?.Invoke();
                        WorldUploadEvents.InvokeOnWorldUploadEnd(true);
                    }, exception =>
                    {
                        DeleteTempAssetsDirectory();
                        Debug.LogException(exception);
                        EditorWindow.GetWindow<VenueUploadWindow>().Repaint();
                        LogWorldUploadFailed();
                        if (exception is FileNotFoundException)
                        {
                            errorMessage = TranslationTable.cck_world_upload_failed_build_support_check;
                            var ok = EditorUtility.DisplayDialog(TranslationTable.cck_build_failed,
                                TranslationTable.cck_build_support_installation_check,
                                TranslationTable.cck_details_open, TranslationTable.cck_close);
                            if (ok)
                            {
                                Application.OpenURL(InstallUnityUrl);
                                PanamaLogger.LogCckOpenLink(InstallUnityUrl, "UploadVenueView_Error");
                            }
                        }
                        else
                        {
                            errorMessage = TranslationTable.cck_world_upload_failed_retry_later;
                        }
                        WorldUploadEvents.InvokeOnWorldUploadEnd(false);
                    });
                currentUploadService.Run(cancellationTokenSource.Token);
                errorMessage = null;
            }
        }

        bool TryExportAssets(Venue venue, out ExportedAssetInfo exportedAssetInfo, out BuildSummary buildSummary)
        {
            exportedAssetInfo = null;
            buildSummary = null;
            var tempAssetsDirName = venue.VenueId.Value;
            var guid = AssetDatabase.CreateFolder("Assets", tempAssetsDirName);
            tempAssetsDirPath = AssetDatabase.GUIDToAssetPath(guid);

            ItemIdAssigner.AssignItemId();
            ItemTemplateIdAssigner.Execute();
            HumanoidAnimationAssigner.Execute(tempAssetsDirPath);
            LayerCorrector.CorrectLayer();
            SubSceneNameAssigner.Execute();

            var useWindows = !isPreviewUpload || previewUploadWindowsSelected;
            var useMac = !isPreviewUpload || previewUploadMacSelected;
            var useIOS = !isPreviewUpload || previewUploadIOSSelected;
            var useAndroid = !isPreviewUpload || previewUploadAndroidSelected;
            try
            {
                exportedAssetInfo = AssetExporter.ExportCurrentSceneResource(venue.VenueId.Value, useWindows, useMac, useIOS, useAndroid);
                buildSummary = BuildSummary.FromExportedAssetInfo(exportedAssetInfo);
            }
            catch (Exception e)
            {
                DeleteTempAssetsDirectory();
                errorMessage = TranslationTable.cck_world_build_failed;
                Debug.LogError(e);
                return false;
            }
            return true;
        }

        void DeleteTempAssetsDirectory()
        {
            if (!string.IsNullOrEmpty(tempAssetsDirPath))
            {
                AssetDatabase.DeleteAsset(tempAssetsDirPath);
            }
        }

        void DrawUI()
        {
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
                    executeUpload = EditorUtility.DisplayDialog(TranslationTable.cck_upload_world,
                        TranslationUtility.GetMessage(TranslationTable.cck_confirm_upload_named_venue, venue.Name),
                        TranslationTable.cck_upload, TranslationTable.cck_cancel);
                    isPreviewUpload = false;
                    isBeta = ClusterCreatorKitSettings.instance.IsBeta;
                }
            }

            if (GUILayout.Button(TranslationTable.cck_open_world_management_page))
            {
                Application.OpenURL(worldManagementUrl);
                PanamaLogger.LogCckOpenLink(worldManagementUrl, "UploadVenueView_OpenWorldPage");
            }

            EditorGUILayout.Space();

            var alreadyUploaded = !string.IsNullOrEmpty(venue.WorldDetailUrl);
            var canSelectPreviewUpload = canUpload && alreadyUploaded;
            using (new EditorGUI.DisabledScope(!canSelectPreviewUpload))
            {
                var previewSelectAreaStyle = new GUIStyle(EditorStyles.helpBox) { padding = { bottom = 6 } };
                using (new GUILayout.VerticalScope(previewSelectAreaStyle))
                {
                    previewUploadSelected = GUILayout.Toggle(previewUploadSelected, TranslationTable.cck_upload_for_testing);
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
                                    previewUploadWindowsSelected = GUILayout.Toggle(previewUploadWindowsSelected, "Windows");
                                    previewUploadMacSelected = GUILayout.Toggle(previewUploadMacSelected, "Mac");
                                    previewUploadIOSSelected = GUILayout.Toggle(previewUploadIOSSelected, "iOS");
                                    previewUploadAndroidSelected = GUILayout.Toggle(previewUploadAndroidSelected, "Android / Quest");
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
                    executeUpload = EditorUtility.DisplayDialog(TranslationTable.cck_upload_world,
                        TranslationUtility.GetMessage(TranslationTable.cck_confirm_upload_named_venue, venue.Name),
                        TranslationTable.cck_upload, TranslationTable.cck_cancel);
                    isPreviewUpload = true;
                    isBeta = ClusterCreatorKitSettings.instance.IsBeta;
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
                    currentUploadService.Run(cancellationTokenSource.Token);
                    errorMessage = null;
                }
            }

            EditorGUILayout.Space();

            foreach (var platformSummary in buildSummary.PlatformSummaries)
            {
                ShowBuiltAssetBundleSize(platformSummary);
            }
        }

        bool IsVenueUploadSettingValid(out string uploadSettingErrorMessage)
        {
            if (thumbnail.IsEmpty)
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

        void LogWorldUploadStart()
        {
            uploadStartAt = DateTime.Now;
            PanamaLogger.LogCckWorldUploadStart(new CckWorldUploadStart
            {
                IsBeta = isBeta,
                IsPreview = isPreviewUpload,
                PreviewWin = isPreviewUpload && previewUploadWindowsSelected,
                PreviewMac = isPreviewUpload && previewUploadMacSelected,
                PreviewAndroid = isPreviewUpload && previewUploadAndroidSelected,
                PreviewIos = isPreviewUpload && previewUploadIOSSelected
            });
        }

        void LogWorldUploadComplete(BuildSummary buildSummary)
        {
            PanamaLogger.LogCckWorldUploadComplete(new CckWorldUploadComplete
            {
                IsBeta = isBeta,
                IsPreview = isPreviewUpload,
                PreviewWin = isPreviewUpload && previewUploadWindowsSelected,
                PreviewMac = isPreviewUpload && previewUploadMacSelected,
                PreviewAndroid = isPreviewUpload && previewUploadAndroidSelected,
                PreviewIos = isPreviewUpload && previewUploadIOSSelected,
                DurationMs = (ulong) (DateTime.Now - uploadStartAt).TotalMilliseconds,
                BuildStats =
                {
                    buildSummary.PlatformSummaries.SelectMany(platformSummary =>
                    {
                        var platform = platformSummary.BuildTarget.DisplayName();
                        return new[]
                        {
                            new CckWorldUploadComplete.Types.CckWorldBuildStatsValue
                            {
                                Platform = platform,
                                SceneIndex = 0,
                                BuildSize = (ulong) platformSummary.MainSceneSummary.TotalSize
                            }
                        }.Concat(platformSummary.SubSceneSummaries.Select((subSceneSummary, i) => new CckWorldUploadComplete.Types.CckWorldBuildStatsValue
                        {
                            Platform = platform,
                            SceneIndex = i + 1,
                            BuildSize = (ulong) subSceneSummary.TotalSize
                        }));
                    })
                },
                SceneStats = new CckWorldUploadComplete.Types.CckWorldSceneStatsValue
                {
                    Components =
                    {
                        ComponentUsageGatherer.GatherCreatorKitComponentUsage()
                            .Select(usage => new CckWorldUploadComplete.Types.CckWorldSceneStatsValue.Types.CckWorldSceneStatsComponentsValue
                            {
                                Name = usage.Key,
                                Count = (uint) usage.Value
                            })
                    }
                }
            });
        }

        void LogWorldUploadFailed()
        {
            PanamaLogger.LogCckWorldUploadFailed(new CckWorldUploadFailed
            {
                IsBeta = isBeta,
                IsPreview = isPreviewUpload,
                PreviewWin = isPreviewUpload && previewUploadWindowsSelected,
                PreviewMac = isPreviewUpload && previewUploadMacSelected,
                PreviewAndroid = isPreviewUpload && previewUploadAndroidSelected,
                PreviewIos = isPreviewUpload && previewUploadIOSSelected,
                DurationMs = (ulong) (DateTime.Now - uploadStartAt).TotalMilliseconds,
            });
        }

        public void Dispose()
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
        }
    }
}
