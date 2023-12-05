using System;
using UnityEngine.Networking;

namespace ClusterVR.CreatorKit.Editor.Api.RPC
{
    public sealed class ApiErrorResponseParseFailedException : Exception
    {
        public ApiErrorResponseParseFailedException(UnityWebRequest www, Exception innerException)
            : base($"Failed to parse API error response body for {www.method} {www.url}. statusCode: {www.responseCode} bodyText: {www.downloadHandler.text}",
                innerException)
        {
        }
    }
}
