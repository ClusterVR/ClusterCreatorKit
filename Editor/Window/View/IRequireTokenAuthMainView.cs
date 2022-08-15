using ClusterVR.CreatorKit.Editor.Api.User;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Window.View
{
    public interface IRequireTokenAuthMainView
    {
        VisualElement LoginAndCreateView(UserInfo userInfo);
        void Logout();
    }
}
