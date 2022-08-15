using System;
using System.IO;
using System.Linq;
using System.Threading;
using ClusterVR.CreatorKit.Editor.Api.RPC;
using ClusterVR.CreatorKit.Editor.Api.User;
using ClusterVR.CreatorKit.Editor.Api.Venue;
using ClusterVR.CreatorKit.Editor.Builder;
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

                if (!VenueValidator.ValidateVenue(out errorMessage, out var invalidObjects))
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
                LayerCorrector.CorrectLayer();

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
                    WorldDescriptorCreator.Create(SceneManager.GetActiveScene()), completionResponse =>
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
            using (new EditorGUI.DisabledScope(!isVenueUploadSettingValid))
            {
                var uploadButton = GUILayout.Button($"'{venue.Name}'としてアップロードする");
                if (uploadButton)
                {
                    executeUpload = EditorUtility.DisplayDialog("ワールドをアップロードする", $"'{venue.Name}'としてアップロードします。よろしいですか？",
                        "アップロード", "キャンセル");
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
                foreach (var status in currentUploadService.UploadStatus)
                {
                    var text = status.Value ? "Success" : "Failed";
                    EditorGUILayout.LabelField(status.Key.ToString(), text);
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

            var windowsPath = BuiltAssetBundlePaths.instance.Find(BuildTarget.StandaloneWindows);
            if (File.Exists(windowsPath))
            {
                var fileInfo = new FileInfo(windowsPath);
                EditorGUILayout.LabelField("Windowsサイズ",
                    $"{(double) fileInfo.Length / (1024 * 1024):F2} MB"); // Byte => MByte
            }

            var macPath = BuiltAssetBundlePaths.instance.Find(BuildTarget.StandaloneOSX);
            if (File.Exists(macPath))
            {
                var fileInfo = new FileInfo(macPath);
                EditorGUILayout.LabelField("Macサイズ",
                    $"{(double) fileInfo.Length / (1024 * 1024):F2} MB"); // Byte => MByte
            }

            var androidPath = BuiltAssetBundlePaths.instance.Find(BuildTarget.Android);
            if (File.Exists(androidPath))
            {
                var fileInfo = new FileInfo(androidPath);
                EditorGUILayout.LabelField("Androidサイズ",
                    $"{(double) fileInfo.Length / (1024 * 1024):F2} MB"); // Byte => MByte
            }

            var iosPath = BuiltAssetBundlePaths.instance.Find(BuildTarget.iOS);
            if (File.Exists(iosPath))
            {
                var fileInfo = new FileInfo(iosPath);
                EditorGUILayout.LabelField("iOSサイズ",
                    $"{(double) fileInfo.Length / (1024 * 1024):F2} MB"); // Byte => MByte
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

        public void Dispose()
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
        }
    }
}
