using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Api.ExternalEndpoint
{
    [Serializable]
    public sealed class RegisterEndpointPayload
    {
        [SerializeField] string url;

        public RegisterEndpointPayload(string url)
        {
            this.url = url;
        }
    }
}
