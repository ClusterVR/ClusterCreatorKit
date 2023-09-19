using System;
using System.IO;
using System.Text;
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

        public bool IsBeta
        {
            get => m_isBeta;
            set
            {
                m_isBeta = value;
                Save();
            }
        }

        static ClusterCreatorKitSettings Load()
        {
            if (!File.Exists(Path))
            {
                return new ClusterCreatorKitSettings();
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
