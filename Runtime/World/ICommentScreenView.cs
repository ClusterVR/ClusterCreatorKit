using System;

namespace ClusterVR.CreatorKit.World
{
    public interface ICommentScreenView
    {
        void AddComment(Comment comment);
        void RemoveComment(string commentId);
        event Action OnDestroyed;
    }
}
