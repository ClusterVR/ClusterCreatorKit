using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Preview.Trigger
{
    public sealed class SignalGenerator
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

            // 100000Ticks = 10ms = 1フレーム未満
            value = new StateValue(now + TimeSpan.FromTicks(100000L / MaxCountPerFrame * indexInFrame));
            ++indexInFrame;
            return true;
        }
    }
}