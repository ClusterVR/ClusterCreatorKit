using System;
using ClusterVR.CreatorKit.Gimmick;

namespace ClusterVR.CreatorKit.Operation
{
    public interface IPlayerLogic : IPlayerGimmick, ILogic
    {
        event RunPlayerLogicEventHandler OnRunPlayerLogic;
    }

    public delegate void RunPlayerLogicEventHandler(RunPlayerLogicEventArgs args);

    public sealed class RunPlayerLogicEventArgs : EventArgs
    {
        public Logic Logic { get; }

        public RunPlayerLogicEventArgs(Logic logic)
        {
            Logic = logic;
        }
    }
}
