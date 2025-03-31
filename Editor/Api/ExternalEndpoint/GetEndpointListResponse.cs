using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Api.ExternalEndpoint
{
    [Serializable]
    public sealed class GetEndpointListResponse
    {
        [SerializeField] ExternalCallEndpoint[] endpoints;

        public ExternalCallEndpoint[] Endpoints => endpoints;
    }
}
