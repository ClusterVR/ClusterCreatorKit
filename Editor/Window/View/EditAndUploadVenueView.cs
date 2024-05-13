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

        public EditAndUploadVenueView(UserInfo userInfo, Action venueChangeCallback)
        {
            var thumbnail = new ImageView();
            editVenueView = new EditVenueView(userInfo, thumbnail, venueChangeCallback);
            uploadVenueView = new UploadVenueView(userInfo, thumbnail, venueChangeCallback);
        }

        public void SetVenue(Venue venue)
        {
            Assert.IsNotNull(venue);
            editVenueView.SetVenue(venue);
            uploadVenueView.SetVenue(venue);
        }

        public VisualElement CreateView()
        {
            var view = new VisualElement();
            view.Add(editVenueView.CreateView());
            view.Add(uploadVenueView.CreateView());
            return view;
        }

        public void Dispose()
        {
            editVenueView?.Dispose();
            uploadVenueView?.Dispose();
        }
    }
}
