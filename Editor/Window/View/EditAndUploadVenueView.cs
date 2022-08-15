using System;
using ClusterVR.CreatorKit.Editor.Api.User;
using ClusterVR.CreatorKit.Editor.Api.Venue;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Window.View
{
    public sealed class EditAndUploadVenueView : IDisposable
    {
        readonly EditVenueView editVenueView;
        readonly UploadVenueView uploadVenueView;

        public EditAndUploadVenueView(UserInfo userInfo, Venue venue, Action venueChangeCallback)
        {
            Assert.IsNotNull(venue);

            var thumbnail = new ImageView();
            editVenueView = new EditVenueView(userInfo, venue, thumbnail, venueChangeCallback);
            uploadVenueView = new UploadVenueView(userInfo, venue, thumbnail);
        }

        public void AddView(VisualElement parent)
        {
            var editVenueTab = editVenueView.CreateView();
            var uploadVenueTab = uploadVenueView.CreateView();
            parent.Add(editVenueTab);
            parent.Add(uploadVenueTab);
        }

        public void Dispose()
        {
            editVenueView?.Dispose();
            uploadVenueView?.Dispose();
        }
    }
}
