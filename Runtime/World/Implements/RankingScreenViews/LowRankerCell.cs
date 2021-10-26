using UnityEngine;
using UnityEngine.UI;

namespace ClusterVR.CreatorKit.World.Implements.RankingScreenViews
{
    public sealed class LowRankerCell : RankingScreenCell
    {
        [SerializeField] GameObject rankinMessage;
        [SerializeField] Text rank;

        public void Rankin()
        {
            Hide();
            rankinMessage.SetActive(true);
        }

        public void Rankout(Ranking selfRanking)
        {
            Show(selfRanking.User, true);
            if (selfRanking.Rank == 0)
            {
                rank.text = "-";
            }
            else
            {
                rank.text = selfRanking.Rank.ToString("N0");
            }

            rankinMessage.SetActive(false);
        }
    }
}
