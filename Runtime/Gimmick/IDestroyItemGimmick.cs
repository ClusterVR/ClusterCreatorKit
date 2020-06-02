using System;
using ClusterVR.CreatorKit.Item;

namespace ClusterVR.CreatorKit.Gimmick
{
    public interface IDestroyItemGimmick
    {
        event DestroyItemEventHandler OnDestroyItem;
    }
    
    public delegate void DestroyItemEventHandler(DestroyItemEventArgs args);
    public class DestroyItemEventArgs : EventArgs
    {
        public IItem Item;
        public double TimestampDiffSeconds;
    }
}