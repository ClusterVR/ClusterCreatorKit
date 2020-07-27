using System;
using ClusterVR.CreatorKit.Gimmick;

namespace ClusterVR.CreatorKit.Operation
{
    public interface IPlayerLogic : IPlayerGimmick
    {
        event RunPlayerLogicEventHandler OnRunPlayerLogic;
    }

    public delegate void RunPlayerLogicEventHandler(RunPlayerLogicEventArgs args);

    public class RunPlayerLogicEventArgs : EventArgs
    {
        public Logic Logic { get; }

        public RunPlayerLogicEventArgs(Logic logic)
        {
            Logic = logic;
        }
    }
}
