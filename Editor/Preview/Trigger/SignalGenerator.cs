using System;
using ClusterVR.CreatorKit.Common;
using ClusterVR.CreatorKit.Trigger;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Preview.Trigger
{
    public sealed class SignalGenerator : ISignalGenerator
    {
        const int MaxCountPerFrame = 500;
        readonly ITimeProvider timeProvider;
        int lastFrameCount;
        int indexInFrame;
        DateTime now;

        public SignalGenerator(ITimeProvider timeProvider)
        {
            this.timeProvider = timeProvider;
        }

        public bool TryGet(out StateValue value)
        {
            var currentFrameCount = Time.frameCount;
            if (lastFrameCount < currentFrameCount)
            {
                lastFrameCount = currentFrameCount;
                indexInFrame = 0;
                now = timeProvider.GetTime();
            }

            if (indexInFrame == MaxCountPerFrame)
            {
                value = default;
                return false;
            }

            value = new StateValue(now + TimeSpan.FromTicks(100000L / MaxCountPerFrame * indexInFrame));
            ++indexInFrame;
            return true;
        }
    }
}
