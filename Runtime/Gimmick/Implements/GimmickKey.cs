using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.Gimmick.Implements
{
    [Serializable]
    public sealed class GimmickKey
    {
        [SerializeField] GimmickTarget target;
        [SerializeField] string key;

        public GimmickTarget Target => target;
        public string Key => key;

        public GimmickKey(GimmickTarget target, string key = "")
        {
            this.target = target;
            this.key = key;
        }
    }
}