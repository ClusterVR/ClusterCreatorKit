using UnityEngine;
using UnityEngine.UI;

namespace ClusterVR.CreatorKit.World.Implements.RankingScreenViews
{
    public class RankingScreenCell : MonoBehaviour
    {
        [SerializeField] Image icon;
        [SerializeField] Text displayName;
        [SerializeField] Text userName;
        [SerializeField] GameObject ranker;
        [SerializeField] GameObject outLine;
        [SerializeField] GameObject background;

        void Start()
        {
            this.DisableRichText();
            this.DisableImageRayCastTarget();
        }

        public void Show(User user, bool isSelf)
        {
            user.LoadPhoto(icon);

            displayName.text = user.DisplayName;
            userName.text = user.UserName;

            ranker.SetActive(true);
            if (background)
            {
                background.SetActive(true);
            }

            if (outLine == null)
            {
                return;
            }

            outLine.SetActive(isSelf);
        }

        public void Hide()
        {
            ranker.SetActive(false);
            if (background)
            {
                background.SetActive(false);
            }
        }
    }
}
