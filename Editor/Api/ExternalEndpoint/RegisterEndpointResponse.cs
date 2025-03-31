using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Api.ExternalEndpoint
{
    [Serializable]
    public sealed class RegisterEndpointResponse
    {
        [SerializeField] string endpointId;
        [SerializeField] string url;

        public string EndpointId => endpointId;
        public string Url => url;
    }
}
