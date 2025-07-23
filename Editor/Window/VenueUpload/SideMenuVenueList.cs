using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ClusterVR.CreatorKit.Editor.Api.User;
using ClusterVR.CreatorKit.Editor.Api.Venue;
using ClusterVR.CreatorKit.Editor.ProjectSettings;
using ClusterVR.CreatorKit.Editor.Repository;
using ClusterVR.CreatorKit.Editor.Utils;
using ClusterVR.CreatorKit.Editor.Utils.Extensions;
using ClusterVR.CreatorKit.Editor.Window.View;
using ClusterVR.CreatorKit.Translation;

namespace ClusterVR.CreatorKit.Editor.Window.VenueUpload
{
    public sealed class SideMenuVenueList : IDisposable
    {
        readonly UserInfo userInfo;

        SideMenuVenueListView view;
        readonly Reactive<string> filterText = new("");
        public IReadOnlyReactive<string> FilterText => filterText;

        public enum GroupSelectorMode
        {
            Empty,
            Loading,
            Loaded,
            Failed
        };

        readonly Reactive<(GroupSelectorMode mode, Groups groups, GroupID currentGroupId)> groupSelectorState = new();
        public IReadOnlyReactive<(GroupSelectorMode mode, Groups groups, GroupID currentGroupId)> GroupSelectorState => groupSelectorState;

        public enum VenueSelectorMode
        {
            Empty,
            Loading,
            Loaded,
            Failed
        };

        readonly Reactive<(VenueSelectorMode mode, List<Venue> venues)> venueSelectorState = new();
        public IReadOnlyReactive<(VenueSelectorMode mode, List<Venue> venues)> VenueSelectorState => venueSelectorState;

        public IReadOnlyReactive<GroupID> CurrentGroupId => GroupRepository.CurrentGroupId;

        readonly IDisposable disposable;

        CancellationTokenSource groupSelectCancellationTokenSource;
        CancellationTokenSource venueSelectCancellationTokenSource;

        GroupRepository GroupRepository => GroupRepository.Instance;
        VenueRepository VenueRepository => VenueRepository.Instance;

        public SideMenuVenueList(UserInfo userInfo)
        {
            this.userInfo = userInfo;

            disposable = Disposable.Create(
                ReactiveBinder.Bind(GroupRepository.Groups, ReconstructGroupSelector),
                ReactiveBinder.Bind(GroupRepository.CurrentGroup, _ => RefreshVenueSelector()),
                ReactiveBinder.Bind(VenueRepository.CurrentVenues, ReconstructVenueSelector),
                ReactiveBinder.Bind(filterText, _ => ReconstructVenueSelector(VenueRepository.CurrentVenues.Val)));

            RefreshGroupSelector();
        }

        public void SelectGroup(GroupID groupId)
        {
            GroupRepository.SetCurrentGroup(groupId);
        }

        void RefreshVenueSelector()
        {
            CancelVenueSelector();
            venueSelectCancellationTokenSource = new();
            RefreshVenueSelectorAsync(venueSelectCancellationTokenSource.Token).Forget();
        }

        async Task RefreshVenueSelectorAsync(CancellationToken cancellationToken)
        {
            venueSelectorState.Val = (VenueSelectorMode.Loading, null);

            try
            {
                var groupId = GroupRepository.CurrentGroupId.Val;
                if (groupId != null)
                {
                    await VenueRepository.ReloadGroupVenuesAsync(groupId, userInfo.VerifiedToken, cancellationToken);
                }
            }
            catch (Exception e) when (e is not OperationCanceledException)
            {
                venueSelectorState.Val = (VenueSelectorMode.Failed, null);
                throw;
            }
        }

        void RefreshGroupSelector()
        {
            CancelGroupSelector();
            groupSelectCancellationTokenSource = new();
            RefreshGroupSelectorAsync(groupSelectCancellationTokenSource.Token).Forget();
        }

        async Task RefreshGroupSelectorAsync(CancellationToken cancellationToken)
        {
            CancelVenueSelector();

            groupSelectorState.Val = (GroupSelectorMode.Loading, null, null);

            try
            {
                await GroupRepository.LoadGroupsAsync(userInfo.VerifiedToken, cancellationToken);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception)
            {
                groupSelectorState.Val = (GroupSelectorMode.Failed, null, null);
                throw;
            }
        }

        void ReconstructGroupSelector(Groups groups)
        {
            if (groups == null)
            {
                groupSelectorState.Val = (GroupSelectorMode.Empty, null, null);
                return;
            }
            groupSelectorState.Val = (GroupSelectorMode.Loaded, groups, GroupRepository.CurrentGroupId.Val);
        }

        void ReconstructVenueSelector(Venues venues)
        {
            if (venues == null)
            {
                venueSelectorState.Val = (VenueSelectorMode.Empty, null);
                return;
            }

            var filteredVenues = venues.List;
            if (!string.IsNullOrEmpty(filterText.Val))
            {
                filteredVenues = venues.List.Where(venue => venue.Name.ToLower().Contains(filterText.Val.ToLower())).ToList();
            }
            venueSelectorState.Val = (VenueSelectorMode.Loaded, filteredVenues);
        }

        public void OnNewVenueClicked()
        {
            OnNewVenueClickedAsync().Forget();
        }

        async Task OnNewVenueClickedAsync()
        {
            var groupId = GroupRepository.CurrentGroup.Val.Id;
            var isBeta = ClusterCreatorKitSettings.instance.IsBeta;
            try
            {
                await VenueRepository.CreateNewVenueAsync(
                    name: TranslationTable.cck_new_world_default_name,
                    description: TranslationTable.cck_description_not_set,
                    groupId,
                    isBeta,
                    userInfo.VerifiedToken,
                    venueSelectCancellationTokenSource.Token);
            }
            catch (Exception exception)
            {
                view.DisplayVenueRegistrationFailed(exception);
            }
        }

        public void OnFilterTextChanged(string newValue)
        {
            filterText.Val = newValue;
        }

        public void OnVenueClicked(Venue venue)
        {
            VenueRepository.SetCurrentVenueId(venue.VenueId);
        }

        void CancelGroupSelector()
        {
            if (groupSelectCancellationTokenSource != null)
            {
                groupSelectCancellationTokenSource.Cancel();
                groupSelectCancellationTokenSource.Dispose();
                groupSelectCancellationTokenSource = null;
            }
        }

        void CancelVenueSelector()
        {
            if (venueSelectCancellationTokenSource != null)
            {
                venueSelectCancellationTokenSource.Cancel();
                venueSelectCancellationTokenSource.Dispose();
                venueSelectCancellationTokenSource = null;
            }
        }

        public void Dispose()
        {
            disposable.Dispose();
            CancelGroupSelector();
            CancelVenueSelector();
        }
    }
}
