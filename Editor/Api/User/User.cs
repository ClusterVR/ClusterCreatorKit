using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Api.User
{
    [Serializable]
    public sealed class User
    {
        [SerializeField] string username;

        public string Username => username;

        public override string ToString()
        {
            return $"Username: {username}";
        }
    }
}
