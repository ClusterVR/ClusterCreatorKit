using System;
using System.Collections.Generic;

namespace ClusterVR.CreatorKit.Editor
{
    public class ReactiveBase
    {
    }

    /// Lightweight reactive value.
    public class Reactive<T> : ReactiveBase
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

    /// Lightweight reactive value subscriptions.
    public class ReactiveBinder
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

        public static void Bind<S>(Reactive<S> rv, Action<S> action)
        {
            Add(rv, () => action(rv.Val));
        }

        public static void Bind<S1, S2>(Reactive<S1> rv1, Reactive<S2> rv2, Action<S1, S2> action)
        {
            Add(rv1, () => action(rv1.Val, rv2.Val));
            Add(rv2, () => action(rv1.Val, rv2.Val));
        }

        static void Add(ReactiveBase rv, Action action)
        {
            if (!bindings.TryGetValue(rv, out var actionList)) {
                actionList = new List<Action>();
                bindings.Add(rv, actionList);
            }
            actionList.Add(action);

            action(); // 初期値送信
        }
    }
}
