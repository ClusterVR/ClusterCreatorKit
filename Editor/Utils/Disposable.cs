using System;

namespace ClusterVR.CreatorKit.Editor.Utils
{
    public sealed class Disposable : IDisposable
    {
        bool isDisposed;
        readonly Action action;

        public Disposable(Action action)
        {
            this.action = action;
        }

        public static Disposable Create(params IDisposable[] disposables) =>
            new(() =>
            {
                foreach (var disposable in disposables)
                {
                    disposable?.Dispose();
                }
            });

        public static Disposable Create(Action action, params IDisposable[] disposables) =>
            new(() =>
            {
                action?.Invoke();
                foreach (var disposable in disposables)
                {
                    disposable?.Dispose();
                }
            });

        public void Dispose()
        {
            if (!isDisposed)
            {
                isDisposed = true;
                action?.Invoke();
            }
        }
    }
}
