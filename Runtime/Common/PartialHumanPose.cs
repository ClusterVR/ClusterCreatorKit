using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ClusterVR.CreatorKit.Common
{
    public readonly struct PartialHumanPose
    {
        public readonly Vector3? CenterPosition;
        public readonly Quaternion? CenterRotation;
        public readonly IReadOnlyList<float?> Muscles;

        public bool HasValue => CenterPosition.HasValue || CenterRotation.HasValue || (Muscles?.Any(m => m.HasValue) ?? false);

        public PartialHumanPose(Vector3? centerPosition, Quaternion? centerRotation, IReadOnlyList<float?> muscles)
        {
            CenterPosition = centerPosition;
            CenterRotation = centerRotation;
            Muscles = muscles;
        }
    }
}
