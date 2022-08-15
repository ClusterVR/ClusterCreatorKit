using System.Threading;
using System.Threading.Tasks;

namespace VGltf.Unity
{
    public sealed class DefaultTimeSlicer : ITimeSlicer
    {
        public Task Slice(CancellationToken ct) => Task.CompletedTask;

        public static ITimeSlicer Default = new DefaultTimeSlicer();
    }
}
