using System;

namespace ClusterVR.CreatorKit.ItemExporter.ExporterHooks
{
    public sealed class MissingHumanoidAnimationException : Exception
    {
        public readonly string Id;

        public MissingHumanoidAnimationException(string id)
        {
            Id = id;
        }
    }
}
