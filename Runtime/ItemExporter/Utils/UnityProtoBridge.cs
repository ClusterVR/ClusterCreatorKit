using System.Collections.Generic;
using Google.Protobuf.Collections;

namespace ClusterVR.CreatorKit.ItemExporter.Utils
{
    public static class UnityProtoBridge
    {
        public static Proto.Vector3 ToProto(this UnityEngine.Vector3 v)
        {
            return new Proto.Vector3
            {
                Elements =
                {
                    new RepeatedField<float>
                    {
                        v.x,
                        v.y,
                        v.z
                    }
                }
            };
        }

        public static IEnumerable<float> Flatten(this IEnumerable<UnityEngine.Vector3> vectors)
        {
            foreach (var v in vectors)
            {
                yield return v.x;
                yield return v.y;
                yield return v.z;
            }
        }
    }
}
