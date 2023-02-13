using System;

namespace ClusterVR.CreatorKit.ItemExporter.ExporterHooks
{
    public sealed class ExtractAudioDataFailedException : Exception
    {
        public readonly string Id;

        public ExtractAudioDataFailedException(string id)
        {
            Id = id;
        }
    }
}
