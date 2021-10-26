using System;
using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.Trigger;

namespace ClusterVR.CreatorKit.Operation
{
    public static class LogicExtensions
    {
        public static IEnumerable<TriggerParam> GetTriggerParams(this Logic logic)
        {
            return logic.Statements
                .Select(s => s.SingleStatement)
                .Where(s => s != null)
                .Select(s => s.TargetState)
                .Select(s => new TriggerParam(s.Target.Convert(), null, s.Key, s.ParameterType, new TriggerValue()));
        }

        static TriggerTarget Convert(this TargetStateTarget target)
        {
            switch (target)
            {
                case TargetStateTarget.Global: return TriggerTarget.Global;
                case TargetStateTarget.Item: return TriggerTarget.Item;
                case TargetStateTarget.Player: return TriggerTarget.Player;
                default: throw new NotImplementedException();
            }
        }
    }
}
