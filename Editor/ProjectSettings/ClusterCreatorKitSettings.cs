using System;
using System.IO;
using System.Text;
using ClusterVR.CreatorKit.Editor.Analytics;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.ProjectSettings
{
    [Serializable]
    public sealed class ClusterCreatorKitSettings
    {
        const string Path = "ProjectSettings/ClusterCreatorKitSettings.json";
        static ClusterCreatorKitSettings internalInstance;

        public static ClusterCreatorKitSettings instance
            => internalInstance == null ? internalInstance = Load() : internalInstance;

        [SerializeField] bool m_isBeta;
        [SerializeField] string m_cckVersion;

        public bool IsBeta
        {
            get => m_isBeta;
            set
            {
                m_isBeta = value;
                Save();
            }
        }

        public string CckVersion
        {
            get => m_cckVersion;
            set
            {
                m_cckVersion = value;
                Save();
            }
        }

        static ClusterCreatorKitSettings Load()
        {
            if (!File.Exists(Path))
            {
                var settings = new ClusterCreatorKitSettings();
                settings.Save();
                PanamaLogger.LogCckNewInstall();
                return settings;
            }

            try
            {
                var json = File.ReadAllText(Path, Encoding.UTF8);
                return JsonUtility.FromJson<ClusterCreatorKitSettings>(json);
            }
            catch
            {
                return new ClusterCreatorKitSettings();
            }
        }

        void Save()
        {
            File.WriteAllText(Path, JsonUtility.ToJson(this), Encoding.UTF8);
        }
    }
}
