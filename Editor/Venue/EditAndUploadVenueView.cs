using System;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Venue
{
    public class EditAndUploadVenueView
    {
        readonly EditVenueView editVenueView;
        readonly UploadVenueView uploadVenueView;

        public EditAndUploadVenueView(UserInfo userInfo, Core.Venue.Json.Venue venue, Action venueChangeCallback)
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
    }
}
