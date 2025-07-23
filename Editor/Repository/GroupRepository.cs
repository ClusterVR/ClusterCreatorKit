using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ClusterVR.CreatorKit.Editor.Api.RPC;
using ClusterVR.CreatorKit.Editor.Api.Venue;
using ClusterVR.CreatorKit.Editor.Utils;

namespace ClusterVR.CreatorKit.Editor.Repository
{
    public sealed class GroupRepository
    {
        public static readonly GroupRepository Instance = new();

        readonly Reactive<Groups> groups = new();
        readonly Reactive<GroupID> currentGroupId = new();
        readonly Reactive<Group> currentGroup = new();

        public IReadOnlyReactive<Groups> Groups => groups;
        public IReadOnlyReactive<GroupID> CurrentGroupId => currentGroupId;
        public IReadOnlyReactive<Group> CurrentGroup => currentGroup;

        private GroupRepository() { }

        public void Clear()
        {
            groups.Val = null;
            currentGroupId.Val = null;
            UpdateCurrentGroupIfNeeded();
        }

        public void SetCurrentGroup(GroupID groupID)
        {
            currentGroupId.Val = groupID;
            UpdateCurrentGroupIfNeeded();
        }

        public async Task LoadGroupsAsync(string accessToken, CancellationToken cancellationToken)
        {
            try
            {
                var groups = await APIServiceClient.GetGroups(accessToken, cancellationToken);
                if (groups.List.All(group => group.Id.Value != currentGroupId.Val?.Value))
                {
                    currentGroupId.Val = groups.List.Select(group => group.Id).FirstOrDefault();
                }
                this.groups.Val = groups;
                UpdateCurrentGroupIfNeeded();
            }
            catch (Exception)
            {
                Clear();
                throw;
            }
        }

        void UpdateCurrentGroupIfNeeded()
        {
            var oldVal = currentGroup.Val;
            var newVal = groups.Val?.List.Find(group => group.Id.Value == currentGroupId.Val?.Value);
            if (oldVal != newVal)
            {
                currentGroup.Val = newVal;
            }
        }
    }
}
