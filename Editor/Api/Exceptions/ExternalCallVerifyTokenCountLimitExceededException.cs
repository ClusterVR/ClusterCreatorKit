using ClusterVR.CreatorKit.Editor.Api.RPC;

namespace ClusterVR.CreatorKit.Editor.Api.Exceptions
{
    public sealed class ExternalCallVerifyTokenCountLimitExceededException : CreatorKitUserException
    {
        public ExternalCallVerifyTokenCountLimitExceededException(Failure failure) : base(failure) { }
    }
}
