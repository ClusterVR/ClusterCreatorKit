using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.World;

namespace ClusterVR.CreatorKit.Editor.Preview.World
{
    public class RankingScreenPresenter
    {
        readonly IList<IRankingScreenView> rankingScreenViews;

        public RankingScreenPresenter(IEnumerable<IRankingScreenView> rankingScreenViews)
        {
            this.rankingScreenViews = rankingScreenViews.ToList();

            foreach (var rankingScreenView in rankingScreenViews)
            {
                rankingScreenView.OnDestroyed += () => this.rankingScreenViews.Remove(rankingScreenView);
            }
        }

        public void SetRanking(int playerCount)
        {
            var rankingData = GenerateRankingData(playerCount);
            foreach (var rankingScreenView in rankingScreenViews)
            {
                rankingScreenView.UpdateCells(rankingData.rankings, rankingData.selfRanking);
            }
        }

        static RankingData GenerateRankingData(int playerCount)
        {
            var rankingData = new RankingData {rankings = new List<Ranking>()};
            for (var i = 0; i < playerCount; i++)
            {
                var user = new User("displayName" + i, "userName" + i, _ => { });
                var ranking = new Ranking(i, user);
                if (i == 0)
                {
                    rankingData.selfRanking = ranking;
                }

                rankingData.rankings.Add(ranking);
            }

            return rankingData;
        }

        struct RankingData
        {
            public List<Ranking> rankings;
            public Ranking selfRanking;
        }
    }
}