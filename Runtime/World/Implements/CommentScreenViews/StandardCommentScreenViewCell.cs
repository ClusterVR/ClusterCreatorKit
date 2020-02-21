using UnityEngine;
using UnityEngine.UI;

namespace ClusterVR.CreatorKit.World.Implements.CommentScreenViews
{
    public class StandardCommentScreenViewCell : MonoBehaviour
    {
        [SerializeField] Text displayNameText;
        [SerializeField] Text userNameText;
        [SerializeField] Text bodyText;
        [SerializeField] Image profilePhotoImage;
        [SerializeField] Image youtubeIcon;

        void Start()
        {
            this.DisableRichText();
            this.DisableImageRayCastTarget();
        }

        public void Show(Comment comment)
        {
            displayNameText.text = comment.CommentedBy.DisplayName;
            userNameText.text = comment.CommentedBy.UserName;
            bodyText.text = comment.Body;
            comment.CommentedBy.LoadPhoto(profilePhotoImage);

            userNameText.transform.parent.gameObject.SetActive(!comment.IsYoutubeLiveComment);
            youtubeIcon.enabled = comment.IsYoutubeLiveComment;
        }
    }
}
