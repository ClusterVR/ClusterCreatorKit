using System;
using System.Collections.Generic;

namespace ClusterVR.CreatorKit.Editor.Window.View
{
    public abstract class ReactiveBase
    {
    }

    public sealed class Reactive<T> : ReactiveBase
    {
        T val;

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
        readonly ReactiveBase reactiveBinder;
        readonly Action action;

        public ReactiveBinderDisposer(ReactiveBase reactiveBinder, Action action)
        {
            this.reactiveBinder = reactiveBinder;
            this.action = action;
        }

        public void Dispose()
        {
            ReactiveBinder.Remove(reactiveBinder, action);
        }
    }

    public sealed class ReactiveBinder
    {
        static Dictionary<ReactiveBase, List<Action>> bindings = new Dictionary<ReactiveBase, List<Action>>();

        public static void NotifyChanged(ReactiveBase rv)
        {
            if (bindings.TryGetValue(rv, out var actions))
            {
                foreach (var action in actions)
                {
                    action();
                }
            }
        }

        public static IDisposable Bind<S>(Reactive<S> rv, Action<S> action)
        {
            return Add(rv, () => action(rv.Val));
        }

        public static void Remove(ReactiveBase rv, Action action)
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

        static IDisposable Add(ReactiveBase rv, Action action)
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
