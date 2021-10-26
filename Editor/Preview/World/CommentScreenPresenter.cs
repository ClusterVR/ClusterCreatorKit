using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.World;

namespace ClusterVR.CreatorKit.Editor.Preview.World
{
    public sealed class CommentScreenPresenter
    {
        readonly IList<ICommentScreenView> commentScreenViews;

        public CommentScreenPresenter(IEnumerable<ICommentScreenView> commentScreenViews)
        {
            this.commentScreenViews = commentScreenViews.ToList();

            foreach (var commentScreen in commentScreenViews)
            {
                commentScreen.OnDestroyed += () => this.commentScreenViews.Remove(commentScreen);
            }
        }

        void SendComment(Comment comment)
        {
            foreach (var commentScreenView in commentScreenViews)
            {
                commentScreenView.AddComment(comment);
            }
        }

        public void SendCommentFromEditorUI(string displayName, string userName, string content)
        {
            var user = new User(displayName, userName, x => { });
            var comment = new Comment(user, content, false);
            SendComment(comment);
        }
    }
}
