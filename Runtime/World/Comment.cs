namespace ClusterVR.CreatorKit.World
{
    public struct Comment
    {
        public User CommentedBy { get; }
        public string Body { get; }
        public bool IsYoutubeLiveComment { get; }

        public Comment(User commentedBy, string body, bool isYoutubeLiveComment)
        {
            CommentedBy = commentedBy;
            Body = body;
            IsYoutubeLiveComment = isYoutubeLiveComment;
        }
    }
}
