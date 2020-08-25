using System;
using ClusterVR.CreatorKit.Editor.Core.Venue;
using ClusterVR.CreatorKit.Editor.Core.Venue.Json;
using ClusterVR.CreatorKit.Editor.Venue;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor
{
    public static class VenueUploadCLI
    {
        static void ValidateScene()
        {
            OpenTargetScene();

            if (!VenueValidator.ValidateVenue(out var errorMessage, out _))
            {
                Debug.LogError($"ValidationError:{errorMessage}");
            }

            Debug.Log("Passed Validation");
        }

        static void BuildScene()
        {
            OpenTargetScene();

            var venueId = GetEnv("VENUE_ID");
            AssetExporter.ExportCurrentSceneResource(venueId, false);

            Debug.Log("Finished Build");
        }

        static void OpenTargetScene()
        {
            var sceneAssetPath = GetEnv("SCENE_PATH");
            EditorSceneManager.OpenScene(sceneAssetPath);
        }

        static void UploadScene()
        {
            Debug.Log("Upload Start");

            var accessToken = GetEnv("ACCESS_TOKEN");
            var venueId = new VenueID(GetEnv("VENUE_ID"));

            var worldDetailUrl = AssetUploader.Upload(accessToken, venueId);

            Debug.Log($"Upload End: {worldDetailUrl}");
        }

        static string GetEnv(string key)
        {
            var v = Environment.GetEnvironmentVariable(key);
            if (v == null)
            {
                throw new Exception(key + " environment value is not found");
            }

            Debug.Log("use environment value for " + key);
            Debug.Log(v);

            return v;
        }
    }
}
