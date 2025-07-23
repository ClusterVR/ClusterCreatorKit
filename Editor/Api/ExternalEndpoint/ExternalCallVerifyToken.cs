using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Api.ExternalEndpoint
{
    [Serializable]
    public sealed class ExternalCallVerifyToken
    {
        public const int MaxVerifyTokenCount = 2;

        [SerializeField] string registeredAt;
        [SerializeField] string tokenId;
        readonly string verifyToken;

        public DateTime RegisteredAt => DateTime.Parse(registeredAt);
        public string TokenId => tokenId;
        public string VerifyToken => verifyToken;

        public ExternalCallVerifyToken(string registeredAt, string tokenId, string verifyToken)
        {
            this.registeredAt = registeredAt;
            this.tokenId = tokenId;
            this.verifyToken = verifyToken;
        }
    }
}
