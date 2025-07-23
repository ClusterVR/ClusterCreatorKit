using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ClusterVR.CreatorKit.Editor.Api.RPC;
using ClusterVR.CreatorKit.Editor.Api.User;
using ClusterVR.CreatorKit.Editor.Api.Venue;
using ClusterVR.CreatorKit.Editor.Utils;
using ClusterVR.CreatorKit.Editor.Utils.Extensions;
using ClusterVR.CreatorKit.Translation;
using UnityEditor;
using UnityEngine.Assertions;

namespace ClusterVR.CreatorKit.Editor.Window.VenueUpload
{
    public sealed class EditVenueViewModel : IDisposable
    {
        readonly Reactive<bool> isDirty = new();

        public IReadOnlyReactive<bool> IsDirty => isDirty;

        readonly UserInfo userInfo;

        internal readonly ImageViewModel Thumbnail = new();

        readonly Reactive<Venue> venue = new();
        readonly Reactive<string> venueId = new();
        readonly Reactive<string> newVenueName = new();
        readonly Reactive<string> newVenueDesc = new();
        readonly Reactive<string> errorMessage = new();
        internal IReadOnlyReactive<Venue> Venue => venue;
        internal IReadOnlyReactive<string> VenueId => venueId;
        internal IReadOnlyReactive<string> NewVenueName => newVenueName;
        internal IReadOnlyReactive<string> NewVenueDesc => newVenueDesc;
        internal IReadOnlyReactive<string> ErrorMessage => errorMessage;
        string newThumbnailPath;
        bool updatingVenue;

        readonly CancellationTokenSource cancellationTokenSource = new();

        public EditVenueViewModel(UserInfo userInfo, Venue venue)
        {
            this.userInfo = userInfo;
            SetVenue(venue);
        }

        void SetVenue(Venue venue)
        {
            Assert.IsNotNull(venue);
            this.venue.Val = venue;
            Thumbnail.SetImageUrl(venue.GetThumbnailUrlForDisplay());
            venueId.Val = venue.VenueId.Value;
            newVenueName.Val = venue.Name;
            newVenueDesc.Val = venue.Description;
        }

        public void SetVenueName(string venueName)
        {
            newVenueName.Val = venueName;
            isDirty.Val = true;
        }

        public void SetVenueDesc(string venueDesc)
        {
            newVenueDesc.Val = venueDesc;
            isDirty.Val = true;
        }

        public void RequestChangeImage()
        {
            if (!updatingVenue)
            {
                newThumbnailPath = EditorUtility.OpenFilePanelWithFilters(TranslationTable.cck_select_image, "", new[] { TranslationTable.cck_image_files, "png,jpg,jpeg", "All files", "*" });
                Thumbnail.SetImagePath(newThumbnailPath);
                UpdateVenueAsync().Forget();
            }
        }

        public void ApplyEdit()
        {
            if (!updatingVenue)
            {
                UpdateVenueAsync().Forget();
            }
        }

        public void CancelEdit()
        {
            newVenueName.Val = venue.Val.Name;
            newVenueDesc.Val = venue.Val.Description;
            isDirty.Val = false;
        }

        async Task UpdateVenueAsync()
        {
            updatingVenue = true;
            errorMessage.Val = null;

            try
            {
                var patchVenueService = new PatchVenueSettingService(
                    userInfo.VerifiedToken,
                    venue.Val,
                    newVenueName.Val,
                    newVenueDesc.Val,
                    venue.Val.ThumbnailUrls.ToList(),
                    newThumbnailPath);
                await patchVenueService.RunAsync(cancellationTokenSource.Token);
                ReloadGroupVenuesService.Instance.ReloadGroupVenuesAsync(cancellationTokenSource.Token).Forget();
            }
            catch (Exception exception) when (exception is not OperationCanceledException)
            {
                errorMessage.Val = TranslationUtility.GetMessage(TranslationTable.cck_world_info_save_failed, exception.Message);
            }
            finally
            {
                updatingVenue = false;
            }
        }

        public void Dispose()
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
        }
    }
}
