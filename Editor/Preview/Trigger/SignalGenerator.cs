using System;
using ClusterVR.CreatorKit.Trigger;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Preview.Trigger
{
    public sealed class SignalGenerator : ISignalGenerator
    {
        const int MaxCountPerFrame = 500;
        int lastFrameCount;
        int indexInFrame;
        DateTime now;

        public bool TryGet(out StateValue value)
        {
            var currentFrameCount = Time.frameCount;
            if (lastFrameCount < currentFrameCount)
            {
                lastFrameCount = currentFrameCount;
                indexInFrame = 0;
                now = DateTime.UtcNow;
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
