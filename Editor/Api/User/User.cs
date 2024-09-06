using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Api.User
{
    [Serializable]
    public sealed class User
    {
        [SerializeField] string userId;
        [SerializeField] string username;

        public string UserId => userId;
        public string Username => username;

        public override string ToString()
        {
            return $"Username: {username}";
        }
    }
}
