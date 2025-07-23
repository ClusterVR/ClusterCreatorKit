using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ClusterVR.CreatorKit.Editor.Api.RPC;
using ClusterVR.CreatorKit.Editor.Api.Venue;
using ClusterVR.CreatorKit.Editor.Utils;

namespace ClusterVR.CreatorKit.Editor.Repository
{
    public sealed class VenueRepository
    {
        public static readonly VenueRepository Instance = new();

        readonly Reactive<Venues> currentVenues = new();
        readonly Reactive<VenueID> currentVenueId = new();
        readonly Reactive<Venue> currentVenue = new();

        public IReadOnlyReactive<Venues> CurrentVenues => currentVenues;
        public IReadOnlyReactive<Venue> CurrentVenue => currentVenue;

        private VenueRepository() { }

        public void Clear()
        {
            currentVenues.Val = null;
            currentVenueId.Val = null;
            UpdateCurrentVenueIfNeeded();
        }

        public void ClearLoadedVenues()
        {
            currentVenues.Val = null;
            UpdateCurrentVenueIfNeeded();
        }

        public async Task LoadGroupVenuesAsync(GroupID groupId, string accessToken, CancellationToken cancellationToken)
        {
            if (currentVenues.Val == null)
            {
                await GetGroupVenuesAsync(groupId, accessToken, cancellationToken);
            }
        }

        public async Task ReloadGroupVenuesAsync(GroupID groupId, string accessToken, CancellationToken cancellationToken)
        {
            await GetGroupVenuesAsync(groupId, accessToken, cancellationToken);
        }

        async Task GetGroupVenuesAsync(GroupID groupId, string accessToken, CancellationToken cancellationToken)
        {
            var venues = await APIServiceClient.GetGroupVenues(groupId, accessToken, cancellationToken);
            currentVenues.Val = venues;
            UpdateCurrentVenueIfNeeded();
        }

        public async Task CreateNewVenueAsync(string name, string description, GroupID groupId, bool isBeta, string accessToken, CancellationToken cancellationToken)
        {
            var venue = await APIServiceClient.PostRegisterNewVenue(
                new PostNewVenuePayload(name, description, groupId.Value, isBeta),
                accessToken, cancellationToken);
            await GetGroupVenuesAsync(groupId, accessToken, cancellationToken);
            SetCurrentVenueId(venue.VenueId);
        }

        public void SetCurrentVenueId(VenueID venueId)
        {
            currentVenueId.Val = venueId;
            UpdateCurrentVenueIfNeeded();
        }

        void UpdateCurrentVenueIfNeeded()
        {
            var oldVal = currentVenue.Val;
            var newVal = currentVenues.Val?.List
                .FirstOrDefault(venue => venue.VenueId?.Value == currentVenueId.Val?.Value);
            if (oldVal != newVal)
            {
                currentVenue.Val = newVal;
            }
        }
    }
}
