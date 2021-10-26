using ClusterVR.CreatorKit.Editor.Api.User;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Window.View
{
    public sealed class VenueUploadWindow : EditorWindow
    {
        [MenuItem("Cluster/ワールドアップロード", priority = 301)]
        public static void Open()
        {
            var window = GetWindow<VenueUploadWindow>();
            window.titleContent = new GUIContent("ワールドアップロード");
        }

        void OnEnable()
        {
            Input.imeCompositionMode = IMECompositionMode.On;
            AwaitRefreshingAndCreateView();
        }

        void OnDisable()
        {
            Input.imeCompositionMode = IMECompositionMode.Auto;
        }

        void AwaitRefreshingAndCreateView()
        {
            EditorApplication.update -= AwaitRefreshingAndCreateView;

            if (EditorApplication.isUpdating)
            {
                EditorApplication.update += AwaitRefreshingAndCreateView;
                return;
            }

            CreateView();
        }

        void CreateView()
        {
            rootVisualElement.styleSheets.Add(
                AssetDatabase.LoadAssetAtPath<StyleSheet>(
                    "Packages/mu.cluster.cluster-creator-kit/Editor/Window/Uss/ClusterStyle.uss"));

            var tokenAuth = new TokenAuthWidget();
            var tokenAuthView = tokenAuth.CreateView(); // .Bindで作り直すとなぜかYogaNodeがStackoverflowするので使い回す

            VisualElement venueUi = null;
            ReactiveBinder.Bind(tokenAuth.reactiveUserInfo, userInfo =>
            {
                if (venueUi != null)
                {
                    rootVisualElement.Remove(venueUi);
                    venueUi = null;
                }

                venueUi = CreateVenueUi(tokenAuth, tokenAuthView, userInfo);
                rootVisualElement.Add(venueUi);
                tokenAuthView.style.display = userInfo.HasValue ? DisplayStyle.None : DisplayStyle.Flex;
            });
        }

        static VisualElement CreateVenueUi(TokenAuthWidget tokenAuth, VisualElement tokenAuthView, UserInfo? userInfo)
        {
            var container = new VisualElement
            {
                style =
                {
                    flexDirection = FlexDirection.Row,
                    flexGrow = 1
                }
            };
            var sidePane = new VisualElement
            {
                style =
                {
#if UNITY_2019_3_OR_NEWER
                    borderLeftColor = new StyleColor(Color.gray),
                    borderRightColor = new StyleColor(Color.gray),
                    borderTopColor = new StyleColor(Color.gray),
                    borderBottomColor = new StyleColor(Color.gray),
#else
                    borderColor = new StyleColor(Color.gray),
#endif
                    borderRightWidth = 1,
                    width = 250
                }
            };
            sidePane.EnableInClassList("pane", true);
            var mainPane = new VisualElement
            {
                style = { flexGrow = 1 }
            };
            mainPane.EnableInClassList("pane", true);
            container.Add(sidePane);
            container.Add(mainPane);

            sidePane.Add(tokenAuthView);

            if (userInfo.HasValue)
            {
                var sideMenu = new SideMenuVenueList(userInfo.Value);
                sideMenu.AddView(sidePane);
                ReactiveBinder.Bind(sideMenu.reactiveForceLogout, forceLogout =>
                {
                    if (forceLogout)
                    {
                        tokenAuth.Logout();
                    }
                });

                ReactiveBinder.Bind(sideMenu.reactiveCurrentVenue, currentVenue =>
                {
                    mainPane.Clear();
                    if (currentVenue != null)
                    {
                        var venueContent = new ScrollView(ScrollViewMode.Vertical) { style = { flexGrow = 1 } };
                        new EditAndUploadVenueView(userInfo.Value, currentVenue,
                            () => { sideMenu.RefetchVenueWithoutChangingSelection(); }).AddView(venueContent);
                        mainPane.Add(venueContent);
                    }
                });
            }
            else
            {
                mainPane.Clear();
            }

            return container;
        }
    }
}
