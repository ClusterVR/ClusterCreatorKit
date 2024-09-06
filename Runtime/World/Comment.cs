namespace ClusterVR.CreatorKit.World
{
    public struct Comment
    {
        public string Id { get; }
        public User CommentedBy { get; }
        public string Body { get; }
        public bool IsYoutubeLiveComment { get; }

        public Comment(User commentedBy, string body, bool isYoutubeLiveComment)
        {
            Id = null;
            CommentedBy = commentedBy;
            Body = body;
            IsYoutubeLiveComment = isYoutubeLiveComment;
        }

        public Comment(string id, User commentedBy, string body, bool isYoutubeLiveComment)
        {
            Id = id;
            CommentedBy = commentedBy;
            Body = body;
            IsYoutubeLiveComment = isYoutubeLiveComment;
        }
    }
}
