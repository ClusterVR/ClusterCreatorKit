using System;

namespace ClusterVR.CreatorKit.Editor.Preview.WebTrigger
{
    [Serializable]
    public class WebTrigger
    {
        public Trigger[] triggers;
    }

    [Serializable]
    public class Trigger
    {
        public string category;
        public bool showConfirmDialog;
        public string displayName;
        public State[] state;
        public float[] color;
    }

    [Serializable]
    public class State
    {
        public string key;
        public string type;
        public string value;
    }
}
