using System;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Api.RPC
{
    [Serializable]
    public sealed class Error
    {
        [SerializeField] string code;
        [SerializeField] string message;

        [SerializeField] string title;
        [SerializeField] string type;
        [SerializeField] string detail;

        public string Code => code;
        public string Message => message;

        public string Title => title;
        public string Type => type;
        public string Detail => detail;
    }
}
