using System;

namespace ClusterVR.CreatorKit.Gimmick
{
    public interface IRerunnableGimmick : IGimmick
    {
        void Rerun(GimmickValue value, DateTime current);
    }
}
