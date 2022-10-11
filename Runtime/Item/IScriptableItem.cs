using System;

namespace ClusterVR.CreatorKit.Item
{
    public interface IScriptableItem
    {
        IItem Item { get; }

        event Action<string> OnSourceCodeChanged;

        string GetSourceCode();
        void SetSourceCode(string sourceCode);
        bool IsValid();
    }
}
