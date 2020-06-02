using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.Gimmick.Implements
{
    [Serializable]
    public class GimmickKey
    {
        [SerializeField] Target target;
        [SerializeField] string key;

        public Target Target => target;
        public string Key => key;

        public GimmickKey(Target target, string key = "")
        {
            this.target = target;
            this.key = key;
        }
    }
}