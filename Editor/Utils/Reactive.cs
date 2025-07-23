using System;
using System.Collections.Generic;

namespace ClusterVR.CreatorKit.Editor.Utils
{
    public interface IReactiveBase
    {
    }

    public interface IReadOnlyReactive<out T> : IReactiveBase
    {
        T Val { get; }
    }

    public sealed class Reactive<T> : IReadOnlyReactive<T>
    {
        T val;

        public Reactive() { }
        public Reactive(T initial)
        {
            val = initial;
        }

        public T Val
        {
            get => val;
            set
            {
                val = value;
                ReactiveBinder.NotifyChanged(this);
            }
        }
    }

    public sealed class ReactiveBinderDisposer : IDisposable
    {
        readonly IReactiveBase reactiveBinder;
        readonly Action action;

        public ReactiveBinderDisposer(IReactiveBase reactiveBinder, Action action)
        {
            this.reactiveBinder = reactiveBinder;
            this.action = action;
        }

        public void Dispose()
        {
            ReactiveBinder.Remove(reactiveBinder, action);
        }
    }

    public static class ReactiveBinder
    {
        static readonly Dictionary<IReactiveBase, List<Action>> bindings = new Dictionary<IReactiveBase, List<Action>>();

        public static void NotifyChanged(IReactiveBase rv)
        {
            if (bindings.TryGetValue(rv, out var actions))
            {
                foreach (var action in actions.ToArray())
                {
                    action();
                }
            }
        }

        public static IDisposable Bind<S>(IReadOnlyReactive<S> rv, Action<S> action)
        {
            return Add(rv, () => action(rv.Val));
        }

        public static void Remove(IReactiveBase rv, Action action)
        {
            if (bindings.TryGetValue(rv, out var actions))
            {
                actions.Remove(action);
                if (actions.Count == 0)
                {
                    bindings.Remove(rv);
                }
            }
        }

        static IDisposable Add(IReactiveBase rv, Action action)
        {
            if (!bindings.TryGetValue(rv, out var actionList))
            {
                actionList = new List<Action>();
                bindings.Add(rv, actionList);
            }

            actionList.Add(action);

            action(); // 初期値送信

            return new ReactiveBinderDisposer(rv, action);
        }
    }
}
