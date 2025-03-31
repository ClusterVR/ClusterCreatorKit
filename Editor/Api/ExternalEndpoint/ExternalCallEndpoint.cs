using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Api.ExternalEndpoint
{
    [Serializable]
    public sealed class ExternalCallEndpoint
    {
        public const int MaxEndpointCount = 100;

        [SerializeField] string endpointId;
        [SerializeField] string url;

        public string EndpointId => endpointId;
        public string Url => url;

        public ExternalCallEndpoint(string endpointId, string url)
        {
            this.endpointId = endpointId;
            this.url = url;
        }
    }
}
