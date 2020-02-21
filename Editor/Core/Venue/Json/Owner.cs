using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Core.Venue.Json
{
    [Serializable]
    public class Owner
    {
        [SerializeField] string userId;
        [SerializeField] string username;
        [SerializeField] string displayName;
        [SerializeField] string photoUrl;

        public UserID UserId => new UserID(userId);
        public string Username => username;
        public string DisplayName => displayName;
        public string PhotoUrl => photoUrl;
    }
}
