using System;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Window.View
{
    public interface IRequireTokenAuthMainView
    {
        (VisualElement, IDisposable) LoginAndCreateView();
        void Logout();
    }
}
