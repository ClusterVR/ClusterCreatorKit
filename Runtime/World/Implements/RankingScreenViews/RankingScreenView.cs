using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ClusterVR.CreatorKit.World.Implements.RankingScreenViews
{
    public class RankingScreenView : MonoBehaviour, IRankingScreenView
    {
        [SerializeField] List<RankingScreenCell> boardCells;
        [SerializeField] LowRankerCell lowRankerCell;

        public event Action OnDestroyed;

        void Start()
        {
            this.DisableRichText();
            this.DisableImageRayCastTarget();
        }

        public void SetVisibility(bool isVisible)
        {
            gameObject.SetActive(isVisible);
        }

        public void UpdateCells(List<Ranking> rankings, Ranking selfRanking)
        {
            for (var i = 0; i < boardCells.Count; i++)
            {
                var ranking = rankings.FirstOrDefault(r => r.Rank == i + 1);
                if (ranking == null)
                {
                    boardCells[i].Hide();
                }
                else
                {
                    var isSelf = selfRanking.User.UserName.Equals(ranking.User.UserName);
                    boardCells[i].Show(ranking.User, isSelf);
                }
            }

            if (selfRanking.Rank == 0 || selfRanking.Rank > 10)
            {
                lowRankerCell.Rankout(selfRanking);
            }
            else
            {
                lowRankerCell.Rankin();
            }
        }

        void OnDestroy()
        {
            OnDestroyed?.Invoke();
        }
    }
}
