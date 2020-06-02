using System;
using System.Collections.Generic;

namespace ClusterVR.CreatorKit.World
{
    public interface IRankingScreenView
    {
        void SetVisibility(bool isVisible);
        void UpdateCells(List<Ranking> rankings, Ranking selfRanking);
        event Action OnDestroyed;
    }
}
