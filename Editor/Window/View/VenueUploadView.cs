using System;
using System.Collections.Generic;
using System.Threading;
using ClusterVR.CreatorKit.Editor.Api.User;
using ClusterVR.CreatorKit.Editor.Api.Venue;
using UnityEngine;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Window.View
{
    public sealed class VenueUploadView : IRequireTokenAuthMainView, IDisposable
    {
        readonly List<IDisposable> disposables = new List<IDisposable>();
        CancellationTokenSource cancellationTokenSource;
        VenueID currentVenueId;
        EditAndUploadVenueView currentEditAndUploadVenueView;

        public VisualElement LoginAndCreateView(UserInfo userInfo)
        {
            Logout();
            cancellationTokenSource = new CancellationTokenSource();

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

            var sideMenu = new SideMenuVenueList(userInfo);
            disposables.Add(sideMenu);
            sideMenu.AddView(sidePane);

            var mainPaneDisposable = ReactiveBinder.Bind(sideMenu.reactiveCurrentVenue, currentVenue =>
            {
                if (currentVenue == null)
                {
                    mainPane.Clear();
                    currentEditAndUploadVenueView?.Dispose();
                    currentEditAndUploadVenueView = null;
                    currentVenueId = null;
                    return;
                }

                if (currentVenue.VenueId != currentVenueId)
                {
                    mainPane.Clear();
                    currentEditAndUploadVenueView?.Dispose();
                    currentEditAndUploadVenueView = null;

                    var venueContent = new ScrollView(ScrollViewMode.Vertical)
                    {
                        style = { flexGrow = 1 }
                    };
                    currentEditAndUploadVenueView = new EditAndUploadVenueView(userInfo, sideMenu.RefetchVenueWithoutChangingSelection);
                    venueContent.Add(currentEditAndUploadVenueView.CreateView());
                    mainPane.Add(venueContent);
                    currentVenueId = currentVenue.VenueId;
                }

                currentEditAndUploadVenueView.SetVenue(currentVenue);
            });
            disposables.Add(mainPaneDisposable);

            return container;
        }

        public void Logout()
        {
            foreach (var disposable in disposables)
            {
                disposable.Dispose();
            }
            disposables.Clear();
            currentEditAndUploadVenueView?.Dispose();
            currentEditAndUploadVenueView = null;
            currentVenueId = null;
            cancellationTokenSource?.Cancel();
            cancellationTokenSource?.Dispose();
            cancellationTokenSource = null;
        }

        public void Dispose()
        {
            Logout();
        }
    }
}
