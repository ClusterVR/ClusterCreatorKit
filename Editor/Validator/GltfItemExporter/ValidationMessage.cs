namespace ClusterVR.CreatorKit.Editor.Validator.GltfItemExporter
{
    public readonly struct ValidationMessage
    {
        public enum MessageType
        {
            Error,      // アップロード不可
            Warning,    // アップロード可能だが、何らかの問題がある
            Info,       // アップロード可能、問題のないメッセージ
        }

        public string Message { get; }

        public MessageType Type { get; }

        public ValidationMessage(string message, MessageType type)
        {
            Message = message;
            Type = type;
        }
    }
}
