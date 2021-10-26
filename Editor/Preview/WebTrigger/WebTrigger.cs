using System;

namespace ClusterVR.CreatorKit.Editor.Preview.WebTrigger
{
    [Serializable]
    public sealed class WebTrigger
    {
        public Trigger[] triggers;
    }

    [Serializable]
    public sealed class Trigger
    {
        public string category;
        public bool showConfirmDialog;
        public string displayName;
        public State[] state;
        public float[] color;
    }

    [Serializable]
    public sealed class State
    {
        public string key;
        public string type;
        public string value;
    }
}
