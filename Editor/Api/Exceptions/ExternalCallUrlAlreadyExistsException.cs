using ClusterVR.CreatorKit.Editor.Api.RPC;

namespace ClusterVR.CreatorKit.Editor.Api.Exceptions
{
    public sealed class ExternalCallUrlAlreadyExistsException : CreatorKitUserException
    {
        public ExternalCallUrlAlreadyExistsException(Failure failure) : base(failure) { }
    }
}
