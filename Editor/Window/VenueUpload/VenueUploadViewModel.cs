using System;
using System.Threading;
using ClusterVR.CreatorKit.Editor.Api.Venue;
using ClusterVR.CreatorKit.Editor.Repository;
using ClusterVR.CreatorKit.Editor.Utils;
using ClusterVR.CreatorKit.Editor.Window.View;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Window.VenueUpload
{
    public sealed class VenueUploadViewModel : IRequireTokenAuthMainView, IDisposable
    {
        IDisposable mainPaneDisposable;
        CancellationTokenSource cancellationTokenSource;

        readonly Reactive<SideMenuVenueList> sideMenuVenueList = new();
        readonly Reactive<EditAndUploadVenueViewModel> editAndUploadVenueViewModel = new();
        public IReadOnlyReactive<SideMenuVenueList> SideMenuVenueList => sideMenuVenueList;
        public IReadOnlyReactive<EditAndUploadVenueViewModel> EditAndUploadVenueViewModel => editAndUploadVenueViewModel;

        TokenAuthRepository TokenAuthRepository => TokenAuthRepository.Instance;
        VenueRepository VenueRepository => VenueRepository.Instance;

        public (VisualElement, IDisposable) LoginAndCreateView()
        {
            Logout();
            cancellationTokenSource = new CancellationTokenSource();

            var view = new VenueUploadView();
            var userInfo = TokenAuthRepository.GetLoggedIn();

            SetSideMenuVenueList(new SideMenuVenueList(userInfo));

            mainPaneDisposable = ReactiveBinder.Bind(VenueRepository.CurrentVenue, currentVenue =>
            {
                var editAndUploadViewModel = currentVenue == null ? null : new EditAndUploadVenueViewModel(userInfo, currentVenue);
                SetEditAndUploadVenueViewModel(editAndUploadViewModel);
            });

            return (view, view.Bind(this));
        }

        public void Logout()
        {
            mainPaneDisposable?.Dispose();
            mainPaneDisposable = null;
            SetSideMenuVenueList(null);
            SetEditAndUploadVenueViewModel(null);
            cancellationTokenSource?.Cancel();
            cancellationTokenSource?.Dispose();
            cancellationTokenSource = null;
        }

        void SetSideMenuVenueList(SideMenuVenueList value)
        {
            sideMenuVenueList.Val?.Dispose();
            sideMenuVenueList.Val = value;
        }

        void SetEditAndUploadVenueViewModel(EditAndUploadVenueViewModel value)
        {
            editAndUploadVenueViewModel.Val?.Dispose();
            editAndUploadVenueViewModel.Val = value;
        }

        public void Dispose()
        {
            Logout();
        }
    }
}
