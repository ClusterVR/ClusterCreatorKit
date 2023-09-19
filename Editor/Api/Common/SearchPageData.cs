using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Api.Common
{
    [Serializable]
    public sealed class SearchPageData
    {
        [SerializeField] int first;
        [SerializeField] int last;
        [SerializeField] int next;
        [SerializeField] int prev;

        public int First => first;
        public int Last => last;
        public int Next => next;
        public int Prev => prev;
    }
}
