using System;
using ClusterVR.CreatorKit.Common;

namespace ClusterVR.CreatorKit.Editor.Preview.Common
{
    public sealed class TimeProvider : ITimeProvider
    {
        DateTime ITimeProvider.GetTime() => DateTime.UtcNow;
    }
}
