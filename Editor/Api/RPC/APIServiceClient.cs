using System;
using System.Threading.Tasks;
using ClusterVR.CreatorKit.Editor.Api.Analytics;
using ClusterVR.CreatorKit.Editor.Api.Venue;

namespace ClusterVR.CreatorKit.Editor.Api.RPC
{
    public sealed class Empty
    {
        public static readonly Empty Value = new Empty();
    }

    public static class APIServiceClient
    {
        public static Task<User.User> GetMyUser(string accessToken)
        {
            return ApiClient.Get<Empty, User.User>(Empty.Value, accessToken,
                $"{Constants.ApiBaseUrl}/v1/account");
        }

        public static Task<Groups> GetGroups(string accessToken)
        {
            return ApiClient.Get<Empty, Groups>(Empty.Value, accessToken,
                $"{Constants.ApiBaseUrl}/v1/user/groups");
        }

        public static Task<Venues> GetGroupVenues(GroupID groupId, string accessToken)
        {
            return ApiClient.Get<Empty, Venues>(Empty.Value, accessToken,
                $"{Constants.ApiBaseUrl}/v1/groups/{groupId.Value}/venues");
        }

        public static Task<Venue.Venue> PostRegisterNewVenue(PostNewVenuePayload payload, string accessToken)
        {
            return ApiClient.Post<PostNewVenuePayload, Venue.Venue>(payload, accessToken,
                $"{Constants.ApiBaseUrl}/v1/venues");
        }

        public static Task<Venue.Venue> PatchVenue(VenueID venueId, PatchVenuePayload payload, string accessToken)
        {
            return ApiClient.Patch<PatchVenuePayload, Venue.Venue>(payload, accessToken,
                $"{Constants.ApiBaseUrl}/v1/venues/{venueId.Value}");
        }

        public static Task<UploadRequest> PostUploadRequest(VenueID venueId, string accessToken)
        {
            return ApiClient.Post<Empty, UploadRequest>(Empty.Value, accessToken,
                $"{Constants.ApiBaseUrl}/v1/venues/{venueId.Value}/upload/new");
        }

        public static Task<VenueUploadRequestCompletionResponse> PostNotifyFinishedUpload(VenueID venueId,
            UploadRequestID uploadRequestId, PostNotifyFinishedUploadPayload payload, string accessToken)
        {
            return ApiClient.Post<PostNotifyFinishedUploadPayload, VenueUploadRequestCompletionResponse>(payload,
                accessToken,
                $"{Constants.ApiBaseUrl}/v1/venues/{venueId.Value}/upload/{uploadRequestId.Value}/done?isPublish={payload.IsPublish}");
        }

        public static Task<ThumbnailUploadPolicy> PostUploadThumbnailPolicy(PostUploadThumbnailPolicyPayload payload,
            string accessToken, Func<string, ThumbnailUploadPolicy> jsonDeserializer)
        {
            return ApiClient.Post(payload, accessToken,
                $"{Constants.ApiBaseUrl}/v1/upload/venue/thumbnail/policies", jsonDeserializer);
        }

        public static Task<AssetUploadPolicy> PostUploadAssetPolicy(UploadRequestID uploadRequestId,
            PostUploadAssetPolicyPayload payload, string accessToken, Func<string, AssetUploadPolicy> jsonDeserializer)
        {
            return ApiClient.Post(payload, accessToken,
                $"{Constants.ApiBaseUrl}/v1/upload/venue/{uploadRequestId.Value}/policies", jsonDeserializer);
        }

        public static Task PostAnalyticsEvent(EventPayload payload, string accessToken)
        {
            return ApiClient.PostAnalyticsEvent(payload, accessToken,
                $"{Constants.ApiBaseUrl}/v1/analytics/event");
        }
    }
}
