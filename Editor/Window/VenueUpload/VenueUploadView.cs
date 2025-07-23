using System;
using ClusterVR.CreatorKit.Editor.Utils;
using ClusterVR.CreatorKit.Editor.Utils.Extensions;
using UnityEngine;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Window.VenueUpload
{
    public sealed class VenueUploadView : VisualElement
    {
        readonly VisualElement mainPane;
        readonly SideMenuVenueListView sideMenuListView;
        readonly EditAndUploadVenueView editAndUploadView;

        IDisposable sideMenuVenueListDisposable;
        IDisposable editAndUploadViewDisposable;

        public VenueUploadView()
        {
            style.flexGrow = 1;
            style.flexDirection = FlexDirection.Row;
            var sidePane = new VisualElement
            {
                style =
                {
                    borderLeftColor = new StyleColor(Color.gray),
                    borderRightColor = new StyleColor(Color.gray),
                    borderTopColor = new StyleColor(Color.gray),
                    borderBottomColor = new StyleColor(Color.gray),
                    borderRightWidth = 1,
                    width = 250
                }
            };
            sidePane.EnableInClassList("pane", true);
            mainPane = new VisualElement
            {
                style = { flexGrow = 1 }
            };
            mainPane.EnableInClassList("pane", true);
            hierarchy.Add(sidePane);
            hierarchy.Add(mainPane);

            sideMenuListView = new SideMenuVenueListView();
            sidePane.Add(sideMenuListView);

            var venueContent = new ScrollView(ScrollViewMode.Vertical)
            {
                style = { flexGrow = 1 }
            };
            editAndUploadView = new EditAndUploadVenueView();
            venueContent.Add(editAndUploadView);
            mainPane.Add(venueContent);
        }

        public IDisposable Bind(VenueUploadViewModel viewModel)
        {
            return Disposable.Create(
                ReactiveBinder.Bind(viewModel.SideMenuVenueList, SetSideMenuVenueList),
                ReactiveBinder.Bind(viewModel.EditAndUploadVenueViewModel, SetEditAndUploadVenueViewModel));
        }

        void SetSideMenuVenueList(SideMenuVenueList sideMenuVenueList)
        {
            sideMenuVenueListDisposable?.Dispose();
            sideMenuVenueListDisposable = null;

            if (sideMenuVenueList != null)
            {
                sideMenuVenueListDisposable = sideMenuListView.Bind(sideMenuVenueList);
            }
        }

        void SetEditAndUploadVenueViewModel(EditAndUploadVenueViewModel editAndUploadVenueViewModel)
        {
            editAndUploadViewDisposable?.Dispose();
            editAndUploadViewDisposable = null;

            if (editAndUploadVenueViewModel != null)
            {
                editAndUploadViewDisposable = editAndUploadView.Bind(editAndUploadVenueViewModel);
            }

            mainPane.SetVisibility(editAndUploadVenueViewModel != null);
        }
    }
}
