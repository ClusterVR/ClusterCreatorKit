using System;
using ClusterVR.CreatorKit.Editor.Api.User;
using ClusterVR.CreatorKit.Editor.Api.Venue;

namespace ClusterVR.CreatorKit.Editor.Window.VenueUpload
{
    public sealed class EditAndUploadVenueViewModel : IDisposable
    {
        internal readonly EditVenueViewModel EditVenueViewModel;
        internal readonly UploadVenueViewModel UploadVenueViewModel;

        public EditAndUploadVenueViewModel(UserInfo userInfo, Venue venue)
        {
            EditVenueViewModel = new EditVenueViewModel(userInfo, venue);
            UploadVenueViewModel = new UploadVenueViewModel(userInfo, venue);
        }

        public void Dispose()
        {
            EditVenueViewModel?.Dispose();
            UploadVenueViewModel?.Dispose();
        }
    }
}
