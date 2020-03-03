using System.Collections.Generic;
using ClusterVR.CreatorKit.World;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Preview.World
{
    public class MainScreenPresenter
    {
        readonly IEnumerable<IMainScreenView> mainScreenViews;

        public MainScreenPresenter(IEnumerable<IMainScreenView> mainScreenViews)
        {
            this.mainScreenViews = mainScreenViews;
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
