using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.World;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Preview.World
{
    public sealed class MainScreenPresenter
    {
        readonly IList<IMainScreenView> mainScreenViews;

        public MainScreenPresenter(IEnumerable<IMainScreenView> mainScreenViews)
        {
            this.mainScreenViews = mainScreenViews.ToList();

            foreach (var mainScreenView in mainScreenViews)
            {
                mainScreenView.OnDestroyed += () => this.mainScreenViews.Remove(mainScreenView);
            }
        }

        public void SetImage(Texture targetTexture)
        {
            foreach (var mainScreenView in mainScreenViews)
            {
                mainScreenView.UpdateContent(targetTexture);
            }
        }
    }
}
