using System.Threading;
using System.Threading.Tasks;

namespace VGltf.Unity
{
    public interface ITimeSlicer
    {
        Task Slice(CancellationToken ct);
    }
}
