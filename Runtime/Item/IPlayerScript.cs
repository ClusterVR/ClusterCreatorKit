namespace ClusterVR.CreatorKit.Item
{
    public interface IPlayerScript
    {
        IItem Item { get; }

        void Construct(string sourceCode);
        string GetSourceCode(bool refresh = false);
    }
}
