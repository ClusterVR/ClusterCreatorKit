using System;
using ClusterVR.CreatorKit.Editor.Api.RPC;

namespace ClusterVR.CreatorKit.Editor.Api.Exceptions
{
    public abstract class CreatorKitException : Exception
    {
        public readonly Error Error;

        private protected CreatorKitException(Failure failure) : base(failure.Message, failure)
        {
            Error = failure.Error;
        }
    }

    public abstract class CreatorKitUserException : CreatorKitException
    {
        protected CreatorKitUserException(Failure failure) : base(failure) { }
    }

    public abstract class CreatorKitInternalException : CreatorKitException
    {
        protected CreatorKitInternalException(Failure failure) : base(failure) { }
    }
}
