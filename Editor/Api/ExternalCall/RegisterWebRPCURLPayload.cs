using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Api.ExternalCall
{
    [Serializable]
    public sealed class RegisterWebRPCURLPayload
    {
        [SerializeField] string url;

        public RegisterWebRPCURLPayload(string url)
        {
            this.url = url;
        }
    }
}
