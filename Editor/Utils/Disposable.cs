using System;

namespace ClusterVR.CreatorKit.Editor.Utils
{
    public sealed class Disposable : IDisposable
    {
        readonly Action action;

        public Disposable(Action action)
        {
            this.action = action;
        }

        void IDisposable.Dispose()
        {
            action?.Invoke();
        }
    }
}
