using System.Collections.Generic;
using ClusterVR.CreatorKit.World;

namespace ClusterVR.CreatorKit.Editor.Preview.World
{
    public class CommentScreenPresenter
    {
        readonly IEnumerable<ICommentScreenView> commentScreenViews;
        public CommentScreenPresenter(IEnumerable<ICommentScreenView> commentScreenViews)
        {
            this.commentScreenViews = commentScreenViews;
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
            var user = new User(displayName,userName,x => {});
            var comment = new Comment(user,content,false);
            SendComment(comment);
        }
    }
}


