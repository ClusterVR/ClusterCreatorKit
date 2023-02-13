using System;

namespace ClusterVR.CreatorKit.Item
{
    public interface IScriptableItem
    {
        IItem Item { get; }

        event Action<string> OnSourceCodeChanged;

        void Construct(string sourceCode);
        string GetSourceCode(bool refresh = false);
        void SetSourceCode(string sourceCode);
        bool IsValid(bool refresh = false);
    }
}
