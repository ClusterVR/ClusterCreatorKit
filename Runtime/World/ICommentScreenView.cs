using System;

namespace ClusterVR.CreatorKit.World
{
    public interface ICommentScreenView
    {
        void AddComment(Comment comment);
        event Action OnDestroyed;
    }
}
