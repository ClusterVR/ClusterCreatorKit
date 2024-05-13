using System;
using System.Collections.Generic;
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
        readonly string worldManagementUrl;
        readonly Action venueChangeCallback;
        readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        Venue venue;
        ExportedAssetInfo exportedAssetInfo;
        UploadVenueService currentUploadService;
        string errorMessage;

        bool previewUploadSelected;
        bool previewUploadWindowsSelected;
        bool previewUploadMacSelected;
        bool previewUploadIOSSelected;
        bool previewUploadAndroidSelected;

        bool executeUpload;
        bool isPreviewUpload;
        bool isBeta;

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

                var tempAssetsDirName = venue.VenueId.Value;
                var guid = AssetDatabase.CreateFolder("Assets", tempAssetsDirName);
                var tempAssetsDirPath = AssetDatabase.GUIDToAssetPath(guid);

                void DeleteTempAssetsDirectory()
                {
                    if (!string.IsNullOrEmpty(tempAssetsDirPath))
                    {
                        AssetDatabase.DeleteAsset(tempAssetsDirPath);
                    }
                }

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
                }
                catch (Exception e)
                {
                    DeleteTempAssetsDirectory();
                    errorMessage = "ワールドのビルドに失敗しました。";
                    Debug.LogError(e);
                    return;
                }

                currentUploadService = new UploadVenueService(userInfo.VerifiedToken, venue,
                    WorldDescriptorCreator.Create(SceneManager.GetActiveScene()),
                    isBeta, isPreviewUpload,
                    exportedAssetInfo,
                    completionResponse =>
                    {
                        DeleteTempAssetsDirectory();
                        errorMessage = "";
                        if (isPreviewUpload)
                        {
                            EditorUtility.DisplayDialog("テスト用のアップロードが完了しました", "入室後「デベロッパーツール」>「テスト用のスペースをはじめる」から利用可能です。", "閉じる");
                        }
                        else
                        {
                            var openWorldManagementUrl = EditorUtility.DisplayDialog("アップロードが完了しました", "アップロードが完了しました", "ワールド管理ページを開く", "閉じる");
                            if (openWorldManagementUrl)
                            {
                                Application.OpenURL(worldManagementUrl);
                            }
                        }
                        venueChangeCallback?.Invoke();
                    }, exception =>
                    {
                        DeleteTempAssetsDirectory();
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

            var canUpload = isVenueUploadSettingValid && betaSettingValid;

            using (new EditorGUI.DisabledScope(!canUpload))
            {
                var uploadButton = GUILayout.Button($"{(venue.IsBeta ? "ベータ機能が有効な " : "")}'{venue.Name}' としてアップロードする");
                if (uploadButton)
                {
                    executeUpload = EditorUtility.DisplayDialog("ワールドをアップロードする", $"'{venue.Name}'としてアップロードします。よろしいですか？",
                        "アップロード", "キャンセル");
                    isPreviewUpload = false;
                    isBeta = ClusterCreatorKitSettings.instance.IsBeta;
                }
            }

            if (GUILayout.Button("ワールド管理ページを開く"))
            {
                Application.OpenURL(worldManagementUrl);
            }

            EditorGUILayout.Space();

            var alreadyUploaded = !string.IsNullOrEmpty(venue.WorldDetailUrl);
            var canSelectPreviewUpload = canUpload && alreadyUploaded;
            using (new EditorGUI.DisabledScope(!canSelectPreviewUpload))
            {
                var previewSelectAreaStyle = new GUIStyle(EditorStyles.helpBox) { padding = { bottom = 6 } };
                using (new GUILayout.VerticalScope(previewSelectAreaStyle))
                {
                    previewUploadSelected = GUILayout.Toggle(previewUploadSelected, "テスト用にアップロードする");
                    using (new GUILayout.HorizontalScope())
                    {
                        EditorGUILayout.Space(10f, false);
                        using (new GUILayout.VerticalScope())
                        {
                            var previewUploadDescription = "選択したデバイスでテスト用のスペースが利用できるようになります。";
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
                var previewDisabledDescription = "※テスト用にアップロードを行うには、一度アップロードすることが必要です";
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
                        EditorGUILayout.HelpBox("テストのはじめ方\n<b>入室後「デベロッパーメニュー」>「テスト用のスペースをはじめる」から利用可能です。</b>", MessageType.Info);
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
                var previewUploadButton = GUILayout.Button("テスト用にアップロードする");
                if (previewUploadButton)
                {
                    executeUpload = EditorUtility.DisplayDialog("ワールドをアップロードする", $"'{venue.Name}'としてアップロードします。よろしいですか？",
                        "アップロード", "キャンセル");
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

            foreach (var platformInfo in exportedAssetInfo.PlatformInfos)
            {
                ShowBuiltAssetBundleSize(platformInfo);
            }
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

        void ShowBuiltAssetBundleSize(ExportedPlatformAssetInfo info)
        {
            var buildTargetName = info.BuildTarget.DisplayName();
            {
                if (TryGetSceneAndDependAssetTotalSize(info.MainSceneInfo, info.VenueAssetInfos, out var size))
                {
                    EditorGUILayout.LabelField($"{buildTargetName} メインシーン サイズ", $"{(double) size / (1024 * 1024):F2} MB"); // Byte => MByte
                }
            }
            var subSceneIndex = 1;
            foreach (var sceneInfo in info.SubSceneInfos)
            {
                if (TryGetFileSize(sceneInfo.BuiltAssetBundlePath, out var size))
                {
                    EditorGUILayout.LabelField($"{buildTargetName} サブシーン{subSceneIndex} サイズ", $"{(double) size / (1024 * 1024):F2} MB"); // Byte => MByte
                    ++subSceneIndex;
                }
            }
        }

        bool TryGetFileSize(string path, out long size)
        {
            if (!File.Exists(path))
            {
                size = default;
                return false;
            }
            size = new FileInfo(path).Length;
            return true;
        }

        bool TryGetSceneAndDependAssetTotalSize(ExportedSceneInfo sceneInfo, IReadOnlyList<ExportedVenueAssetInfo> venueAssetInfos, out long size)
        {
            if (!TryGetFileSize(sceneInfo.BuiltAssetBundlePath, out size))
            {
                return false;
            }

            foreach (var assetIdDependsOn in sceneInfo.AssetIdsDependsOn)
            {
                var asset = venueAssetInfos.FirstOrDefault(i => i.Id == assetIdDependsOn);
                if (asset != null && TryGetFileSize(asset.BuiltAssetBundlePath, out var venueAssetSize))
                {
                    size += venueAssetSize;
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
