using System.Collections.Generic;

namespace ClusterVR.CreatorKit.Gimmick
{
    public interface IGimmickUpdater
    {
        void OnStateUpdated(IEnumerable<string> keys);
    }
}
