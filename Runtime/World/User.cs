using System;
using UnityEngine.UI;

namespace ClusterVR.CreatorKit.World
{
    public struct User
    {
        public string DisplayName { get; }
        public string UserName { get; }
        public Action<Image> LoadPhoto { get; }

        public User(string displayName, string userName, Action<Image> loadPhoto)
        {
            DisplayName = displayName;
            UserName = userName;
            LoadPhoto = loadPhoto;
        }
    }
}
