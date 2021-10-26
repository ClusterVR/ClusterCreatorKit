using System;
using System.Collections.Generic;
using System.Linq;

namespace ClusterVR.CreatorKit.Gimmick
{
    public sealed class GimmickStateValueSet
    {
        readonly IGimmick gimmick;
        readonly IStateValueSet stateValueSet;

        public IGimmick Gimmick => gimmick;

        public GimmickStateValueSet(IGimmick gimmick)
        {
            this.gimmick = gimmick;
            stateValueSet = StateValueSet.Create(gimmick.ParameterType);
        }

        public IEnumerable<string> Keys()
        {
            return stateValueSet.GetFieldNames().Select(f => f.BuildKey(gimmick.Key));
        }

        public void Run(string key, StateValue value, DateTime current)
        {
            stateValueSet.Update(key, value);
            gimmick.Run(stateValueSet.ToGimmickValue(), current);
        }

        public bool TryRerun<TRerunnableGimmick>(string key, StateValue value, DateTime current) where TRerunnableGimmick : IRerunnableGimmick
        {
            if (!(gimmick is TRerunnableGimmick rerunnableGimmick))
            {
                return false;
            }

            stateValueSet.Update(key, value);
            rerunnableGimmick.Rerun(stateValueSet.ToGimmickValue(), current);
            return true;
        }
    }
}
