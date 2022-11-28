using UnityEngine;

namespace ClusterVR.CreatorKit.Item
{
    public readonly struct AttachCaseToAvatar
    {
        public readonly HumanBodyBones DefaultHumanBodyBoneName;

        public AttachCaseToAvatar(HumanBodyBones defaultHumanBodyBoneName)
        {
            DefaultHumanBodyBoneName = defaultHumanBodyBoneName;
        }
    }
}
