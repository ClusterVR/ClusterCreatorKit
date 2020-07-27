using System;
using ClusterVR.CreatorKit.Gimmick;

namespace ClusterVR.CreatorKit.Operation
{
    public interface IGlobalLogic : IGlobalGimmick
    {
        event RunGlobalLogicEventHandler OnRunGlobalLogic;
    }

    public delegate void RunGlobalLogicEventHandler(RunGlobalLogicEventArgs args);

    public class RunGlobalLogicEventArgs : EventArgs
    {
        public Logic Logic { get; }

        public RunGlobalLogicEventArgs(Logic logic)
        {
            Logic = logic;
        }
    }
}
