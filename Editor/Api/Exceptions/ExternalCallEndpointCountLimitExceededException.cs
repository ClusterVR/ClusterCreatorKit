using ClusterVR.CreatorKit.Editor.Api.RPC;

namespace ClusterVR.CreatorKit.Editor.Api.Exceptions
{
    public sealed class ExternalCallEndpointCountLimitExceededException : CreatorKitUserException
    {
        public ExternalCallEndpointCountLimitExceededException(Failure failure) : base(failure) { }
    }
}
