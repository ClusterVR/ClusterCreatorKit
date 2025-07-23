using ClusterVR.CreatorKit.Editor.Repository;
using ClusterVR.CreatorKit.Editor.Utils;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Observer
{
    public static class GroupChangedObserver
    {
        static GroupRepository GroupRepository => GroupRepository.Instance;
        static VenueRepository VenueRepository => VenueRepository.Instance;

        [InitializeOnLoadMethod]
        static void ObserveGroupChanged()
        {
            ReactiveBinder.Bind(GroupRepository.CurrentGroup, _ =>
            {
                VenueRepository.ClearLoadedVenues();
            });
        }
    }
}
