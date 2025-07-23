using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ClusterVR.CreatorKit.Editor.Analytics;
using ClusterVR.CreatorKit.Editor.Api.RPC;
using ClusterVR.CreatorKit.Editor.Api.User;
using ClusterVR.CreatorKit.Editor.Api.Venue;
using ClusterVR.CreatorKit.Editor.Builder;
using ClusterVR.CreatorKit.Editor.EditorEvents;
using ClusterVR.CreatorKit.Editor.ProjectSettings;
using ClusterVR.CreatorKit.Editor.Utils;
using ClusterVR.CreatorKit.Editor.Utils.Extensions;
using ClusterVR.CreatorKit.Editor.Validator;
using ClusterVR.CreatorKit.Editor.VenueInfo;
using ClusterVR.CreatorKit.Proto;
using ClusterVR.CreatorKit.Translation;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

namespace ClusterVR.CreatorKit.Editor.Window.VenueUpload
{
    public sealed class UploadVenueViewModel : IDisposable
    {
        const string InstallUnityUrl = "https://docs.cluster.mu/creatorkit/installation/install-unity/";

        readonly UserInfo userInfo;

        readonly CancellationTokenSource cancellationTokenSource = new();

        readonly Reactive<Venue> venue = new();
        internal IReadOnlyReactive<Venue> Venue => venue;

        ExportedAssetInfo exportedAssetInfo;
        string tempAssetsDirPath;

        readonly Reactive<bool> previewUploadSelected = new();
        readonly Reactive<bool> previewUploadWindowsSelected = new();
        readonly Reactive<bool> previewUploadMacSelected = new();
        readonly Reactive<bool> previewUploadIOSSelected = new();
        readonly Reactive<bool> previewUploadAndroidSelected = new();

        readonly Reactive<BuildSummary> currentBuildSummary = new();
        readonly Reactive<UploadVenueService> currentUploadService = new();
        readonly Reactive<string> errorMessage = new();

        internal IReadOnlyReactive<bool> PreviewUploadSelected => previewUploadSelected;
        internal IReadOnlyReactive<bool> PreviewUploadWindowsSelected => previewUploadWindowsSelected;
        internal IReadOnlyReactive<bool> PreviewUploadMacSelected => previewUploadMacSelected;
        internal IReadOnlyReactive<bool> PreviewUploadIOSSelected => previewUploadIOSSelected;
        internal IReadOnlyReactive<bool> PreviewUploadAndroidSelected => previewUploadAndroidSelected;
        internal IReadOnlyReactive<BuildSummary> CurrentBuildSummary => currentBuildSummary;
        internal IReadOnlyReactive<UploadVenueService> CurrentUploadService => currentUploadService;
        internal IReadOnlyReactive<string> ErrorMessage => errorMessage;

        DateTime uploadStartAt;

        public UploadVenueViewModel(UserInfo userInfo, Venue venue)
        {
            this.userInfo = userInfo;
            SetVenue(venue);

#if UNITY_EDITOR_WIN
            previewUploadWindowsSelected.Val = true;
#elif UNITY_EDITOR_OSX
            previewUploadMacSelected.Val = true;
#endif
        }

        void SetVenue(Venue venue)
        {
            Assert.IsNotNull(venue);
            this.venue.Val = venue;
        }

        public void ConfirmUpload()
        {
            var executeUpload = EditorUtility.DisplayDialog(TranslationTable.cck_upload_world,
                TranslationUtility.GetMessage(TranslationTable.cck_confirm_upload_named_venue, venue.Val.Name),
                TranslationTable.cck_upload, TranslationTable.cck_cancel);
            var isPreviewUpload = false;
            var isBeta = ClusterCreatorKitSettings.instance.IsBeta;
            if (executeUpload)
            {
                StartUpload(isPreviewUpload, isBeta);
            }
        }

        public void ConfirmPreviewUpload()
        {
            var executeUpload = EditorUtility.DisplayDialog(TranslationTable.cck_upload_world,
                TranslationUtility.GetMessage(TranslationTable.cck_confirm_upload_named_venue, venue.Val.Name),
                TranslationTable.cck_upload, TranslationTable.cck_cancel);
            var isPreviewUpload = true;
            var isBeta = ClusterCreatorKitSettings.instance.IsBeta;
            if (executeUpload)
            {
                StartUpload(isPreviewUpload, isBeta);
            }
        }

        public void StartUpload(bool isPreviewUpload, bool isBeta)
        {
            ProcessAsync(isPreviewUpload, isBeta,
                previewUploadWindowsSelected.Val,
                previewUploadMacSelected.Val,
                previewUploadIOSSelected.Val,
                previewUploadAndroidSelected.Val).Forget();
        }

        public void RetryUpload()
        {
            if (!currentUploadService.Val.IsProcessing)
            {
                currentUploadService.Val.Run(cancellationTokenSource.Token);
                errorMessage.Val = null;
            }
        }

        public void SetPreviewUploadSelected(bool value) => previewUploadSelected.Val = value;
        public void SetPreviewUploadWindowsSelected(bool value) => previewUploadWindowsSelected.Val = value;
        public void SetPreviewUploadMacSelected(bool value) => previewUploadMacSelected.Val = value;
        public void SetPreviewUploadIOSSelected(bool value) => previewUploadIOSSelected.Val = value;
        public void SetPreviewUploadAndroidSelected(bool value) => previewUploadAndroidSelected.Val = value;

        async Task ProcessAsync(bool isPreviewUpload, bool isBeta,
            bool isPreviewWindows, bool isPreviewMac, bool isPreviewIOS, bool isPreviewAndroid)
        {
            if (venue == null)
            {
                return;
            }
            currentUploadService.Val = null;
            this.errorMessage.Val = "";

            LogWorldUploadStart(isPreviewUpload, isBeta, isPreviewWindows, isPreviewMac, isPreviewIOS, isPreviewAndroid);

            if (!WorldUploadEvents.InvokeOnWorldUploadStart(SceneManager.GetActiveScene()))
            {
                Debug.LogError(TranslationTable.cck_build_stopped_user_defined_callback_error);
                LogWorldUploadFailed(isPreviewUpload, isBeta, isPreviewWindows, isPreviewMac, isPreviewIOS, isPreviewAndroid);
                WorldUploadEvents.InvokeOnWorldUploadEnd(false);
                return;
            }

            if (!VenueValidator.ValidateVenue(isBeta, out var errorMessage, out var invalidObjects))
            {
                this.errorMessage.Val = errorMessage;
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
                LogWorldUploadFailed(isPreviewUpload, isBeta, isPreviewWindows, isPreviewMac, isPreviewIOS, isPreviewAndroid);
                WorldUploadEvents.InvokeOnWorldUploadEnd(false);
                return;
            }

            if (!TryExportAssets(venue.Val, isPreviewUpload, isBeta,
                    isPreviewWindows, isPreviewMac, isPreviewIOS, isPreviewAndroid))
            {
                LogWorldUploadFailed(isPreviewUpload, isBeta, isPreviewWindows, isPreviewMac, isPreviewIOS, isPreviewAndroid);
                WorldUploadEvents.InvokeOnWorldUploadEnd(false);
                return;
            }

            var displayIds = VenueInfoConstructor.FindDisplayItemIds();

            currentUploadService.Val = new UploadVenueService(userInfo.VerifiedToken, venue.Val,
                WorldDescriptorCreator.Create(SceneManager.GetActiveScene()),
                isBeta, isPreviewUpload,
                displayIds,
                exportedAssetInfo);

            try
            {
                var completionResponse = await currentUploadService.Val.RunAsync(cancellationTokenSource.Token);
                LogWorldUploadComplete(currentBuildSummary.Val, isPreviewUpload, isBeta, isPreviewWindows, isPreviewMac,
                    isPreviewIOS, isPreviewAndroid);
                if (isPreviewUpload)
                {
                    EditorUtility.DisplayDialog(TranslationTable.cck_test_upload_complete,
                        TranslationTable.cck_start_test_space, TranslationTable.cck_close);
                }
                else
                {
                    var openWorldManagementUrl = EditorUtility.DisplayDialog(
                        TranslationTable.cck_upload_complete, TranslationTable.cck_upload_complete,
                        TranslationTable.cck_open_world_detail_page, TranslationTable.cck_close);
                    if (openWorldManagementUrl)
                    {
                        OpenWorldDetailUrlOnUploadComplete(completionResponse.Url);
                    }
                }
                ReloadGroupVenuesService.Instance.ReloadGroupVenuesAsync(cancellationTokenSource.Token).Forget();
                WorldUploadEvents.InvokeOnWorldUploadEnd(true);
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
                EditorWindow.GetWindow<VenueUploadWindow>().Repaint();
                LogWorldUploadFailed(isPreviewUpload, isBeta, isPreviewWindows, isPreviewMac, isPreviewIOS,
                    isPreviewAndroid);
                if (exception is FileNotFoundException)
                {
                    this.errorMessage.Val = TranslationTable.cck_world_upload_failed_build_support_check;
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
                    this.errorMessage.Val = TranslationTable.cck_world_upload_failed_retry_later;
                    if (exception is Failure failure && !string.IsNullOrEmpty(failure.Error.Detail))
                    {
                        this.errorMessage.Val += $"\nerror: {failure.Error.Detail}";
                    }
                }
                WorldUploadEvents.InvokeOnWorldUploadEnd(false);
            }
            finally
            {
                DeleteTempAssetsDirectory();
            }
        }

        bool TryExportAssets(Venue venue, bool isPreviewUpload, bool isBeta,
            bool isPreviewWindows, bool isPreviewMac, bool isPreviewIOS, bool isPreviewAndroid)
        {
            exportedAssetInfo = null;
            currentBuildSummary.Val = null;
            var tempAssetsDirName = venue.VenueId.Value;
            var guid = AssetDatabase.CreateFolder("Assets", tempAssetsDirName);
            tempAssetsDirPath = AssetDatabase.GUIDToAssetPath(guid);

            ItemIdAssigner.AssignItemId();
            ItemTemplateIdAssigner.Execute();
            HumanoidAnimationAssigner.Execute(tempAssetsDirPath);
            LayerCorrector.CorrectLayer();
            SubSceneNameAssigner.Execute();

            var useWindows = !isPreviewUpload || isPreviewWindows;
            var useMac = !isPreviewUpload || isPreviewMac;
            var useIOS = !isPreviewUpload || isPreviewIOS;
            var useAndroid = !isPreviewUpload || isPreviewAndroid;
            try
            {
                exportedAssetInfo = AssetExporter.ExportCurrentSceneResource(venue.VenueId.Value, useWindows, useMac, useIOS, useAndroid);
                currentBuildSummary.Val = BuildSummary.FromExportedAssetInfo(exportedAssetInfo);
            }
            catch (Exception e)
            {
                DeleteTempAssetsDirectory();
                errorMessage.Val = TranslationTable.cck_world_build_failed;
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

        void LogWorldUploadStart(bool isPreviewUpload, bool isBeta,
            bool isPreviewWindows, bool isPreviewMac, bool isPreviewIOS, bool isPreviewAndroid)
        {
            uploadStartAt = DateTime.Now;
            PanamaLogger.LogCckWorldUploadStart(new CckWorldUploadStart
            {
                IsBeta = isBeta,
                IsPreview = isPreviewUpload,
                PreviewWin = isPreviewUpload && isPreviewWindows,
                PreviewMac = isPreviewUpload && isPreviewMac,
                PreviewAndroid = isPreviewUpload && isPreviewAndroid,
                PreviewIos = isPreviewUpload && isPreviewIOS
            });
        }

        void LogWorldUploadComplete(BuildSummary buildSummary, bool isPreviewUpload, bool isBeta,
            bool isPreviewWindows, bool isPreviewMac, bool isPreviewIOS, bool isPreviewAndroid)
        {
            PanamaLogger.LogCckWorldUploadComplete(new CckWorldUploadComplete
            {
                IsBeta = isBeta,
                IsPreview = isPreviewUpload,
                PreviewWin = isPreviewUpload && isPreviewWindows,
                PreviewMac = isPreviewUpload && isPreviewMac,
                PreviewAndroid = isPreviewUpload && isPreviewAndroid,
                PreviewIos = isPreviewUpload && isPreviewIOS,
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

        void LogWorldUploadFailed(bool isPreviewUpload, bool isBeta,
            bool isPreviewWindows, bool isPreviewMac, bool isPreviewIOS, bool isPreviewAndroid)
        {
            PanamaLogger.LogCckWorldUploadFailed(new CckWorldUploadFailed
            {
                IsBeta = isBeta,
                IsPreview = isPreviewUpload,
                PreviewWin = isPreviewUpload && isPreviewWindows,
                PreviewMac = isPreviewUpload && isPreviewMac,
                PreviewAndroid = isPreviewUpload && isPreviewAndroid,
                PreviewIos = isPreviewUpload && isPreviewIOS,
                DurationMs = (ulong) (DateTime.Now - uploadStartAt).TotalMilliseconds,
            });
        }

        public void OpenWorldManagementPage()
        {
            OpenUrl($"{Api.RPC.Constants.WebBaseUrl}/account/worlds", "UploadVenueView_OpenWorldPage");
        }

        public void OpenWorldDetailUrlOfCurrentVenue()
        {
            OpenUrl(venue.Val.WorldDetailUrl, "UploadVenueView_OpenWorldPage");
        }

        void OpenWorldDetailUrlOnUploadComplete(string url)
        {
            OpenUrl(url, "UploadVenueView_UploadComplete");
        }

        static void OpenUrl(string url, string from)
        {
            Application.OpenURL(url);
            PanamaLogger.LogCckOpenLink(url, from);
        }

        public void Dispose()
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
        }
    }
}
