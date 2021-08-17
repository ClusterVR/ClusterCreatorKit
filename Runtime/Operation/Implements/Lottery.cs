using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ClusterVR.CreatorKit.Operation.Implements
{
    public static class Lottery
    {
        public static bool TryGetWeightRandom<T>(T[] choices, Func<T, float> weightGetter, out T result)
        {
            result = default;
            if (choices.Length == 0)
            {
                return false;
            }
            var totalWeight = choices.Sum(weightGetter);
            if (Mathf.Approximately(totalWeight, 0f))
            {
                return false;
            }
            var select = Random.Range(0f, totalWeight);
            foreach (var choice in choices)
            {
                select -= weightGetter(choice);
                if (select <= 0f)
                {
                    result = choice;
                    return true;
                }
            }

            result = choices[0]; // 誤差対策
            return true;
        }
    }
}
