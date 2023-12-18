using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Api.ExternalCall
{
    [Serializable]
    public class RegisterWebRPCURLResponse
    {
        [SerializeField] string url;
        [SerializeField] string verifyToken;

        public string Url => url;
        public string VerifyToken => verifyToken;
    }
}
