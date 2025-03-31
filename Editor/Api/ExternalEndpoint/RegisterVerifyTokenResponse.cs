using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Api.ExternalEndpoint
{
    [Serializable]
    public sealed class RegisterVerifyTokenResponse
    {
        [SerializeField] string registeredAt;
        [SerializeField] string tokenId;
        [SerializeField] string verifyToken;

        public string RegisteredAt => registeredAt;
        public string TokenId => tokenId;
        public string VerifyToken => verifyToken;
    }
}
