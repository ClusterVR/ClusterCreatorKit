using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Api.ExternalCall
{
    [Serializable]
    public sealed class GetWebRPCURLResponse
    {
        [SerializeField] string url;

        public string Url => url;
    }
}
