using System;
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

        bool executeUpload;
        string errorMessage;
        UploadVenueService currentUploadService;

        public UploadVenueView(UserInfo userInfo, Core.Venue.Json.Venue venue)
        {
            Assert.IsNotNull(venue);
            this.userInfo = userInfo;
            this.venue = venue;
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

                if (!VenueValidator.ValidateVenue(out errorMessage))
                {
                    Debug.LogError(errorMessage);
                    EditorUtility.DisplayDialog("Cluster Creator Kit", errorMessage, "閉じる");
                    return;
                }

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
                    () => errorMessage = "",
                    exception =>
                    {
                        Debug.LogException(exception);
                        errorMessage = $"ワールドのアップロードに失敗しました。時間をあけてリトライしてみてください。";
                        EditorWindow.GetWindow<VenueUploadWindow>().Repaint();
                    });
                currentUploadService.Run();
                errorMessage = null;
            }
        }

        void DrawUI()
        {
            EditorGUILayout.Space();
            EditorGUILayout.HelpBox("アップロードするシーンを開いておいてください。", MessageType.Info);

            if (GUILayout.Button($"'{venue.Name}'としてアップロードする"))
            {
                executeUpload = EditorUtility.DisplayDialog(
                    "ワールドをアップロードする",
                    $"公開ワールド'{venue.Name}'としてアップロードします。よろしいですか？",
                    "アップロード",
                    "キャンセル"
                );
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
    }
}
