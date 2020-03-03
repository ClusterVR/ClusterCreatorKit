using ClusterVR.CreatorKit.Editor.Core.Venue.Json;
using UnityEngine.Networking;

namespace ClusterVR.CreatorKit.Editor.Core.Venue
{
    public class Empty
    {
        public static readonly Empty Value = new Empty();
    }

    public static class APIServiceClient
    {
        public static readonly RPCClient<Empty, User.User> GetMyUser =
            new RPCClient<Empty, User.User>(
                _ => $"{Constants.UserApiBaseUrl}/v1/users/me",
                UnityWebRequest.kHttpVerbGET);

        public static readonly RPCClient<Empty, Groups> GetGroups =
            new RPCClient<Empty, Groups>(
                _ => $"{Constants.VenueApiBaseUrl}/v1/user/groups",
                UnityWebRequest.kHttpVerbGET);

        public static readonly RPCClient<GroupID, Venues> GetGroupVenues =
            new RPCClient<GroupID, Venues>(
                groupId => $"{Constants.VenueApiBaseUrl}/v1/groups/{groupId.Value}/venues",
                UnityWebRequest.kHttpVerbGET);

        public static readonly RPCClient<VenueID, UploadRequest> PostUploadRequest =
            new RPCClient<VenueID, UploadRequest>(
                venueId => $"{Constants.VenueApiBaseUrl}/v1/venues/{venueId.Value}/upload/new",
                UnityWebRequest.kHttpVerbPOST);

        public static readonly RPCClient<(VenueID, UploadRequestID), VenueUploadRequestCompletionResponse> PostNotifyFinishedUpload =
            new RPCClient<(VenueID venueId, UploadRequestID uploadRequestId), VenueUploadRequestCompletionResponse>(
                request => $"{Constants.VenueApiBaseUrl}/v1/venues/{request.venueId.Value}/upload/{request.uploadRequestId.Value}/done",
                UnityWebRequest.kHttpVerbPOST);
    }
}
