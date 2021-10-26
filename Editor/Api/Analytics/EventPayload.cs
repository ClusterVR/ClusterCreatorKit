using System;
using ClusterVR.CreatorKit.Editor.Api.RPC;
using ClusterVR.CreatorKit.Proto;
using Google.Protobuf;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Api.Analytics
{
    [Serializable]
    public sealed class EventPayload
    {
        [SerializeField] string payload;

        public EventPayload(CreatorKitEventPayload payload)
        {
            this.payload = payload.ToByteArray().ToSafeBase64();
        }
    }
}
