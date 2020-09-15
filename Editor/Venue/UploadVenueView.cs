using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ClusterVR.CreatorKit.Editor.Core;
using ClusterVR.CreatorKit.Editor.Core.Venue;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Venue
{
    public class UploadVenueView
    {
        readonly UserInfo userInfo;
        readonly Core.Venue.Json.Venue venue;
        string worldDetailUrl;
        readonly string worldManagementUrl;

        bool executeUpload;
        string errorMessage;
        UploadVenueService currentUploadService;
        ImageView thumbnail;

        public UploadVenueView(UserInfo userInfo, Core.Venue.Json.Venue venue, ImageView thumbnail)
        {
            Assert.IsNotNull(venue);
            this.userInfo = userInfo;
            this.venue = venue;
            this.thumbnail = thumbnail;
            worldDetailUrl = venue.WorldDetailUrl;
            worldManagementUrl = ClusterVR.CreatorKit.Editor.Core.Constants.WebBaseUrl + "/account/worlds";
        }

        public VisualElement CreateView()
        {
            return new IMGUIContainer(() => {Process(); DrawUI();});
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
                    AssetExporter.ExportCurrentSceneResource(venue.VenueId.Value, false); //Notice UnityPackage が大きくなりすぎてあげれないので一旦やめる
                }
                catch (Exception e)
                {
                    errorMessage = $"現在のSceneのUnityPackage作成時にエラーが発生しました。 {e.Message}";
                    return;
                }

                currentUploadService = new UploadVenueService(
                    userInfo.VerifiedToken,
                    venue,
                    completionResponse =>
                    {
                        errorMessage = "";
                        worldDetailUrl = completionResponse.Url;
                        if (EditorPrefsUtils.OpenWorldManagementPageAfterUpload)
                        {
                            Application.OpenURL(worldManagementUrl);
                        }
                    },
                    exception =>
                    {
                        Debug.LogException(exception);
                        EditorWindow.GetWindow<VenueUploadWindow>().Repaint();
                        if (exception is FileNotFoundException)
                        {
                            errorMessage = $"ワールドのアップロードに失敗しました。必要なBuild Supportが全てインストールされているか確認してください。";
                            var ok = EditorUtility.DisplayDialog("ビルドに失敗しました", "必要なBuild Supportが全てインストールされているか確認してください。", "詳細を開く", "閉じる");
                            if (ok) Application.OpenURL("https://clustervr.gitbook.io/creatorkit/installation/install-unity");
                        }
                        else
                        {
                            errorMessage = $"ワールドのアップロードに失敗しました。時間をあけてリトライしてみてください。";
                        }
                    });
                currentUploadService.Run();
                errorMessage = null;
            }
        }
        
        void DrawUI()
        {
            EditorGUILayout.Space();
            EditorPrefsUtils.OpenWorldManagementPageAfterUpload = EditorGUILayout.ToggleLeft("アップロード後にワールド管理ページを開く", EditorPrefsUtils.OpenWorldManagementPageAfterUpload);
            EditorGUILayout.HelpBox("アップロードするシーンを開いておいてください。", MessageType.Info);

            var isVenueUploadSettingValid = IsVenueUploadSettingValid(out var uploadSettingErrorMessage);
            if (!isVenueUploadSettingValid) EditorGUILayout.HelpBox(uploadSettingErrorMessage, MessageType.Error);
            using (new EditorGUI.DisabledScope(!isVenueUploadSettingValid))
            {
                var uploadButton = GUILayout.Button($"'{venue.Name}'としてアップロードする");
                if (uploadButton)
                {
                    executeUpload = EditorUtility.DisplayDialog(
                        "ワールドをアップロードする",
                        $"'{venue.Name}'としてアップロードします。よろしいですか？",
                        "アップロード",
                        "キャンセル"
                    );
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
                EditorUtility.DisplayProgressBar(
                    "Venue Upload",
                    $"upload processing {finishedProcessCount} of {allProcessCount}",
                    (float) finishedProcessCount / allProcessCount
                );
            }

            if (!currentUploadService.IsProcessing
                && currentUploadService.UploadStatus.Values.Any(x => !x))
            {
                if (GUILayout.Button("アップロードリトライ"))
                {
                    currentUploadService.Run();
                    errorMessage = null;
                }
            }

            EditorGUILayout.Space();

            if (File.Exists(EditorPrefsUtils.LastBuildWin))
            {
                var fileInfo = new FileInfo(EditorPrefsUtils.LastBuildWin);
                EditorGUILayout.LabelField("Windowsサイズ", $"{(double) fileInfo.Length / (1024 * 1024):F2} MB"); // Byte => MByte
            }

            if (File.Exists(EditorPrefsUtils.LastBuildMac))
            {
                var fileInfo = new FileInfo(EditorPrefsUtils.LastBuildMac);
                EditorGUILayout.LabelField("Macサイズ",$"{(double) fileInfo.Length / (1024 * 1024):F2} MB"); // Byte => MByte
            }

            if (File.Exists(EditorPrefsUtils.LastBuildAndroid))
            {
                var fileInfo = new FileInfo(EditorPrefsUtils.LastBuildAndroid);
                EditorGUILayout.LabelField("Androidサイズ",$"{(double) fileInfo.Length / (1024 * 1024):F2} MB"); // Byte => MByte
            }

            if (File.Exists(EditorPrefsUtils.LastBuildIOS))
            {
                var fileInfo = new FileInfo(EditorPrefsUtils.LastBuildIOS);
                EditorGUILayout.LabelField("iOSサイズ",$"{(double) fileInfo.Length / (1024 * 1024):F2} MB"); // Byte => MByte
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
    }
}
