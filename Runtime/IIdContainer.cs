using System.Collections.Generic;

namespace ClusterVR.CreatorKit
{
    public interface IIdContainer
    {
        IEnumerable<string> Ids { get; }
    }
}
