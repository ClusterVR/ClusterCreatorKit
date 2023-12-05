using System;
using System.IO;
using System.Linq;
using System.Threading;
using ClusterVR.CreatorKit.Editor.Api.RPC;
using ClusterVR.CreatorKit.Editor.Api.User;
using ClusterVR.CreatorKit.Editor.Api.Venue;
using ClusterVR.CreatorKit.Editor.Builder;
using ClusterVR.CreatorKit.Editor.Enquete;
using ClusterVR.CreatorKit.Editor.ProjectSettings;
using ClusterVR.CreatorKit.Editor.Validator;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Window.View
{
    public sealed class UploadVenueView : IDisposable
    {
        readonly ImageView thumbnail;
        readonly UserInfo userInfo;
        readonly Venue venue;
        readonly string worldManagementUrl;
        readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        UploadVenueService currentUploadService;
        string errorMessage;

        bool executeUpload;
        bool isBeta;

        public UploadVenueView(UserInfo userInfo, Venue venue, ImageView thumbnail)
        {
            Assert.IsNotNull(venue);
            this.userInfo = userInfo;
            this.venue = venue;
            this.thumbnail = thumbnail;
            worldManagementUrl = Api.RPC.Constants.WebBaseUrl + "/account/worlds";
        }

        public VisualElement CreateView()
        {
            return new IMGUIContainer(() =>
            {
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

                if (!VenueValidator.ValidateVenue(isBeta, out errorMessage, out var invalidObjects))
                {
                    EditorUtility.DisplayDialog("Cluster Creator Kit", errorMessage, "閉じる");
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
                    return;
                }

                ItemIdAssigner.AssignItemId();
                ItemTemplateIdAssigner.Execute();
                HumanoidAnimationAssigner.Execute();
                LayerCorrector.CorrectLayer();
                SubSceneNameAssigner.Execute();

                try
                {
                    AssetExporter.ExportCurrentSceneResource(venue.VenueId.Value);
                }
                catch (Exception e)
                {
                    errorMessage = "ワールドのビルドに失敗しました。";
                    Debug.LogError(e);
                    return;
                }

                currentUploadService = new UploadVenueService(userInfo.VerifiedToken, venue,
                    WorldDescriptorCreator.Create(SceneManager.GetActiveScene()),
                    isBeta,
                    completionResponse =>
                    {
                        errorMessage = "";
                        if (EditorPrefsUtils.OpenWorldManagementPageAfterUpload)
                        {
                            Application.OpenURL(worldManagementUrl);
                        }
                    }, exception =>
                    {
                        Debug.LogException(exception);
                        EditorWindow.GetWindow<VenueUploadWindow>().Repaint();
                        if (exception is FileNotFoundException)
                        {
                            errorMessage = $"ワールドのアップロードに失敗しました。必要なBuild Supportが全てインストールされているか確認してください。";
                            var ok = EditorUtility.DisplayDialog("ビルドに失敗しました",
                                "必要なBuild Supportが全てインストールされているか確認してください。", "詳細を開く", "閉じる");
                            if (ok)
                            {
                                Application.OpenURL(
                                    "https://docs.cluster.mu/creatorkit/installation/install-unity/");
                            }
                        }
                        else
                        {
                            errorMessage = $"ワールドのアップロードに失敗しました。時間をあけてリトライしてみてください。";
                        }
                    });
                currentUploadService.Run(cancellationTokenSource.Token);
                errorMessage = null;
            }
        }

        void DrawUI()
        {
            EditorGUILayout.Space();
            EditorPrefsUtils.OpenWorldManagementPageAfterUpload = EditorGUILayout.ToggleLeft("アップロード後にワールド管理ページを開く",
                EditorPrefsUtils.OpenWorldManagementPageAfterUpload);
            EditorGUILayout.HelpBox("アップロードするシーンを開いておいてください。", MessageType.Info);

            var isVenueUploadSettingValid = IsVenueUploadSettingValid(out var uploadSettingErrorMessage);
            if (!isVenueUploadSettingValid)
            {
                EditorGUILayout.HelpBox(uploadSettingErrorMessage, MessageType.Error);
            }

            var betaSettingValid = venue.IsBeta == ClusterCreatorKitSettings.instance.IsBeta;
            if (!betaSettingValid)
            {
                var message = venue.IsBeta ? "ベータ機能を有効にしたワールドには、ベータ機能を有効にする設定が ON になっていないとアップロードできません" : "ベータ機能が無効なワールドには、ベータ機能を有効にする設定が OFF になっていないとアップロードできません";
                EditorGUILayout.HelpBox(message, MessageType.Error);
            }

            using (new EditorGUI.DisabledScope(!isVenueUploadSettingValid || !betaSettingValid))
            {
                var uploadButton = GUILayout.Button($"{(venue.IsBeta ? "ベータ機能が有効な " : "")}'{venue.Name}' としてアップロードする");
                if (uploadButton)
                {
                    executeUpload = EditorUtility.DisplayDialog("ワールドをアップロードする", $"'{venue.Name}'としてアップロードします。よろしいですか？",
                        "アップロード", "キャンセル");
                    isBeta = ClusterCreatorKitSettings.instance.IsBeta;
                }
            }

            if (GUILayout.Button("ワールド管理ページを開く"))
            {
                Application.OpenURL(worldManagementUrl);
            }

            EditorGUILayout.Space();

            if (!string.IsNullOrEmpty(errorMessage))
            {
                EditorGUILayout.HelpBox(errorMessage, MessageType.Error);
            }

            if (currentUploadService == null)
            {
                return;
            }

            if (!currentUploadService.IsProcessing)
            {
                EditorUtility.ClearProgressBar();

                var succeeded = true;
                foreach (var status in currentUploadService.UploadStatus)
                {
                    var text = status.Value ? "Success" : "Failed";
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
                EditorUtility.DisplayProgressBar("Venue Upload",
                    $"upload processing {finishedProcessCount} of {allProcessCount}",
                    (float) finishedProcessCount / allProcessCount);
            }

            if (!currentUploadService.IsProcessing && currentUploadService.UploadStatus.Values.Any(x => !x))
            {
                if (GUILayout.Button("アップロードリトライ"))
                {
                    currentUploadService.Run(cancellationTokenSource.Token);
                    errorMessage = null;
                }
            }

            EditorGUILayout.Space();

            ShowBuiltAssetBundleSize(BuildTarget.StandaloneWindows);
            ShowBuiltAssetBundleSize(BuildTarget.StandaloneOSX);
            ShowBuiltAssetBundleSize(BuildTarget.Android);
            ShowBuiltAssetBundleSize(BuildTarget.iOS);
        }

        bool IsVenueUploadSettingValid(out string uploadSettingErrorMessage)
        {
            if (thumbnail.IsEmpty)
            {
                uploadSettingErrorMessage = "サムネイル画像を設定してください。";
                return false;
            }

            if (EditorApplication.isPlaying)
            {
                uploadSettingErrorMessage = "エディターの再生を停止してください。";
                return false;
            }

            uploadSettingErrorMessage = default;
            return true;
        }

        void ShowBuiltAssetBundleSize(BuildTarget target)
        {
            var buildTargetName = target.DisplayName();
            var assetBundlePaths = BuiltAssetBundlePaths.instance.SelectBuildTargetAssetBundlePaths(target)
                .OrderBy(a => a.SceneType == AssetSceneType.Main ? 0 : 1);
            var subSceneIndex = 1;
            foreach (var assetBundlePath in assetBundlePaths)
            {
                if (TryGetTotalSize(assetBundlePath, out var size))
                {
                    var sceneTypeName = assetBundlePath.SceneType == AssetSceneType.Main ? "メインシーン" : $"サブシーン{subSceneIndex}";
                    EditorGUILayout.LabelField($"{buildTargetName} {sceneTypeName} サイズ",
                        $"{(double) size / (1024 * 1024):F2} MB"); // Byte => MByte

                    if (assetBundlePath.SceneType == AssetSceneType.Sub)
                    {
                        subSceneIndex++;
                    }
                }
            }
        }

        bool TryGetTotalSize(AssetBundlePath assetBundlePath, out long size)
        {
            if (!File.Exists(assetBundlePath.Path))
            {
                size = default;
                return false;
            }
            size = new FileInfo(assetBundlePath.Path).Length;
            if (assetBundlePath.SceneType == AssetSceneType.Main)
            {
                if (assetBundlePath.AssetIdsDependsOn != null)
                {
                    foreach (var assetIds in assetBundlePath.AssetIdsDependsOn)
                    {
                        var asset = BuiltAssetBundlePaths.instance.SelectBuildTargetVenueAssetPaths(assetBundlePath.Target)
                            .FirstOrDefault(v => v.Path.EndsWith(assetIds, StringComparison.Ordinal));
                        if (asset != null && File.Exists(asset.Path))
                        {
                            size += new FileInfo(asset.Path).Length;
                        }
                    }
                }
            }
            return true;
        }

        public void Dispose()
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
        }
    }
}
