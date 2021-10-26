using System;
using ClusterVR.CreatorKit.Item;

namespace ClusterVR.CreatorKit.Gimmick
{
    public interface IGetOffItemGimmick : IItemGimmick
    {
        event Action<IRidableItem> OnGetOff;
    }
}
