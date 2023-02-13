using System;

namespace ClusterVR.CreatorKit.ItemExporter.ExporterHooks
{
    public sealed class MissingAudioClipException : Exception
    {
        public readonly string Id;

        public MissingAudioClipException(string id)
        {
            Id = id;
        }
    }
}
