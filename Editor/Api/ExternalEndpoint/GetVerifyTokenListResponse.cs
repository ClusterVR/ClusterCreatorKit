using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Api.ExternalEndpoint
{
    [Serializable]
    public sealed class GetVerifyTokenListResponse
    {
        [SerializeField] ExternalCallVerifyToken[] tokens;

        public ExternalCallVerifyToken[] Tokens => tokens;
    }
}
