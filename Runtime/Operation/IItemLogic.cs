using System;
using ClusterVR.CreatorKit.Gimmick;
using ClusterVR.CreatorKit.Trigger;

namespace ClusterVR.CreatorKit.Operation
{
    public interface IItemLogic : IItemGimmick, ILogic
    {
        event RunItemLogicEventHandler OnRunItemLogic;
    }

    public delegate void RunItemLogicEventHandler(IItemLogic sender, RunItemLogicEventArgs args);

    public sealed class RunItemLogicEventArgs : EventArgs
    {
        public Logic Logic { get; }

        public RunItemLogicEventArgs(Logic logic)
        {
            Logic = logic;
        }
    }
}
