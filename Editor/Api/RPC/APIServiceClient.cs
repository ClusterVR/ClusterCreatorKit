using System;
using System.Threading;
using System.Threading.Tasks;
using ClusterVR.CreatorKit.Editor.Api.AccessoryTemplate;
using ClusterVR.CreatorKit.Editor.Api.Analytics;
using ClusterVR.CreatorKit.Editor.Api.ItemTemplate;
using ClusterVR.CreatorKit.Editor.Api.Venue;

namespace ClusterVR.CreatorKit.Editor.Api.RPC
{
    public sealed class Empty
    {
        public static readonly Empty Value = new Empty();
    }

    public static partial class APIServiceClient
    {
        public static Task<User.User> GetMyUser(string accessToken, CancellationToken cancellationToken)
        {
            return ApiClient.Get<Empty, User.User>(Empty.Value, accessToken,
                $"{Constants.ApiBaseUrl}/v1/account", cancellationToken);
        }

        public static Task<Groups> GetGroups(string accessToken, CancellationToken cancellationToken)
        {
            return ApiClient.Get<Empty, Groups>(Empty.Value, accessToken,
                $"{Constants.ApiBaseUrl}/v1/user/groups", cancellationToken);
        }

        public static Task<Venues> GetGroupVenues(GroupID groupId, string accessToken,
            CancellationToken cancellationToken)
        {
            return ApiClient.Get<Empty, Venues>(Empty.Value, accessToken,
                $"{Constants.ApiBaseUrl}/v1/groups/{groupId.Value}/venues", cancellationToken);
        }

        public static Task<Venue.Venue> PostRegisterNewVenue(PostNewVenuePayload payload, string accessToken,
            CancellationToken cancellationToken)
        {
            return ApiClient.Post<PostNewVenuePayload, Venue.Venue>(payload, accessToken,
                $"{Constants.ApiBaseUrl}/v1/venues", cancellationToken);
        }

        public static Task<Venue.Venue> PatchVenue(VenueID venueId, PatchVenuePayload payload, string accessToken,
            CancellationToken cancellationToken)
        {
            return ApiClient.Patch<PatchVenuePayload, Venue.Venue>(payload, accessToken,
                $"{Constants.ApiBaseUrl}/v1/venues/{venueId.Value}", cancellationToken);
        }

        public static Task<UploadRequest> PostUploadRequest(VenueID venueId, string accessToken,
            CancellationToken cancellationToken)
        {
            return ApiClient.Post<Empty, UploadRequest>(Empty.Value, accessToken,
                $"{Constants.ApiBaseUrl}/v1/venues/{venueId.Value}/upload/new", cancellationToken);
        }

        public static Task<VenueUploadRequestCompletionResponse> PostNotifyFinishedUpload(VenueID venueId,
            UploadRequestID uploadRequestId, PostNotifyFinishedUploadPayload payload, string accessToken,
            CancellationToken cancellationToken)
        {
            return ApiClient.Post<PostNotifyFinishedUploadPayload, VenueUploadRequestCompletionResponse>(payload,
                accessToken,
                $"{Constants.ApiBaseUrl}/v1/venues/{venueId.Value}/upload/{uploadRequestId.Value}/done?isPublish={payload.IsPublish}",
                cancellationToken);
        }

        public static Task<ThumbnailUploadPolicy> PostUploadThumbnailPolicy(PostUploadThumbnailPolicyPayload payload,
            string accessToken, Func<string, ThumbnailUploadPolicy> jsonDeserializer,
            CancellationToken cancellationToken)
        {
            return ApiClient.Post(payload, accessToken,
                $"{Constants.ApiBaseUrl}/v1/upload/venue/thumbnail/policies", jsonDeserializer, cancellationToken);
        }

        public static Task<AssetUploadPolicy> PostUploadAssetPolicy(UploadRequestID uploadRequestId,
            PostUploadAssetPolicyPayload payload, string accessToken, Func<string, AssetUploadPolicy> jsonDeserializer,
            CancellationToken cancellationToken)
        {
            return ApiClient.Post(payload, accessToken,
                $"{Constants.ApiBaseUrl}/v1/upload/venue/{uploadRequestId.Value}/policies", jsonDeserializer,
                cancellationToken);
        }

        public static Task PostAnalyticsEvent(EventPayload payload, string accessToken,
            CancellationToken cancellationToken)
        {
            return ApiClient.PostAnalyticsEvent(payload, accessToken, $"{Constants.ApiBaseUrl}/v1/analytics/event",
                cancellationToken);
        }

        public static Task<UploadItemTemplatePoliciesResponse> PostItemTemplatePolicies(
            UploadItemTemplatePoliciesPayload payload,
            string accessToken, Func<string, UploadItemTemplatePoliciesResponse> jsonDeserializer,
            CancellationToken cancellationToken)
        {
            return ApiClient.Post(payload, accessToken,
                $"{Constants.ApiBaseUrl}/v1/upload/item_template/policies", jsonDeserializer,
                cancellationToken);
        }

        public static Task<UploadAccessoryTemplatePoliciesResponse> PostAccessoryTemplatePolicies(
            UploadAccessoryTemplatePoliciesPayload payload,
            string accessToken, Func<string, UploadAccessoryTemplatePoliciesResponse> jsonDeserializer,
            CancellationToken cancellationToken)
        {
            return ApiClient.Post(payload, accessToken,
                $"{Constants.ApiBaseUrl}/v1/upload/accessory_template/policies", jsonDeserializer,
                cancellationToken);
        }
    }
}
