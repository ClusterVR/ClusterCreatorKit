using System;

namespace ClusterVR.CreatorKit.Editor.Api.RPC
{
    public sealed class Failure : Exception
    {
        public int StatusCode { get; }
        public Error Error { get; }

        public Failure(int statusCode, Error error)
        {
            StatusCode = statusCode;
            Error = error;
        }
    }
}
