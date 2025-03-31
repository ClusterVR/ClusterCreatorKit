using ClusterVR.CreatorKit.Editor.Api.RPC;

namespace ClusterVR.CreatorKit.Editor.Api.Exceptions
{
    public sealed class ExternalCallInvalidUrlException : CreatorKitUserException
    {
        public ExternalCallInvalidUrlException(Failure failure) : base(failure) { }
    }
}
