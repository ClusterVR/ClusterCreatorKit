using System;
using ClusterVR.CreatorKit.Gimmick;

namespace ClusterVR.CreatorKit.Operation
{
    public interface IItemLogic : IItemGimmick
    {
        event RunItemLogicEventHandler OnRunItemLogic;
    }

    public delegate void RunItemLogicEventHandler(IItemLogic sender, RunItemLogicEventArgs args);

    public class RunItemLogicEventArgs : EventArgs
    {
        public Logic Logic { get; }

        public RunItemLogicEventArgs(Logic logic)
        {
            Logic = logic;
        }
    }
}
