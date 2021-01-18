using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Api.User
{
    [Serializable]
    public class User
    {
        [SerializeField] string username;

        public string Username => username;

        public override string ToString()
        {
            return $"Username: {username}";
        }
    }
}
