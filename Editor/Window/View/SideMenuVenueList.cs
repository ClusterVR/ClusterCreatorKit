using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ClusterVR.CreatorKit.Editor.Api.RPC;
using ClusterVR.CreatorKit.Editor.Api.User;
using ClusterVR.CreatorKit.Editor.Api.Venue;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Window.View
{
    public sealed class SideMenuVenueList
    {
        public readonly Reactive<Venue> reactiveCurrentVenue = new Reactive<Venue>();

        readonly UserInfo userInfo;

        readonly Dictionary<GroupID, Venues> allVenues = new Dictionary<GroupID, Venues>();

        VisualElement selector;

        public SideMenuVenueList(UserInfo userInfo)
        {
            this.userInfo = userInfo;
        }

        public void AddView(VisualElement parent, CancellationToken cancellationToken)
        {
            selector = new VisualElement()
            {
                style = { flexGrow = 1 }
            };
            parent.Add(selector);
            _ = RefreshVenueSelector(cancellationToken);
        }

        public void RefetchVenueWithoutChangingSelection(CancellationToken cancellationToken)
        {
            var currentVenue = reactiveCurrentVenue.Val;
            if (currentVenue != null)
            {
                _ = RefreshVenueSelector(cancellationToken, currentVenue.Group.Id, currentVenue.VenueId);
            }
            else
            {
                _ = RefreshVenueSelector(cancellationToken);
            }
        }

        async Task RefreshVenueSelector(CancellationToken cancellationToken, GroupID groupIdToSelect = null,
            VenueID venueIdToSelect = null)
        {
            selector.Clear();
            selector.Add(new Label() { text = "loading..." });

            var venuePickerHolder = new VisualElement()
            {
                style = { flexGrow = 1 }
            };

            void RecreateVenuePicker(GroupID groupId)
            {
                venuePickerHolder.Clear();
                venuePickerHolder.Add(CreateVenuePicker(groupId, allVenues[groupId], cancellationToken, venueIdToSelect));
            }

            try
            {
                var groups = await APIServiceClient.GetGroups(userInfo.VerifiedToken, cancellationToken);
                foreach (var group in groups.List)
                {
                    allVenues[@group.Id] =
                        await APIServiceClient.GetGroupVenues(@group.Id, userInfo.VerifiedToken, cancellationToken);
                }

                selector.Clear();
                selector.Add(new Label("所属チーム"));
                if (groups.List.Count == 0)
                {
                    selector.Add(new Label() { text = "読み込みに失敗しました" });
                }
                else
                {
                    var teamMenu = new PopupField<Group>(groups.List, 0, group => group.Name, group => group.Name);
                    teamMenu.RegisterValueChangedCallback(ev => RecreateVenuePicker(ev.newValue.Id));
                    selector.Add(teamMenu);

                    var groupToSelect = groups.List.Find(group => group.Id == groupIdToSelect) ?? groups.List[0];
                    teamMenu.SetValueWithoutNotify(groupToSelect);

                    RecreateVenuePicker(groupToSelect.Id);
                }

                selector.Add(UiUtils.Separator());

                selector.Add(new Label() { text = "ワールド" });
                selector.Add(venuePickerHolder);

            }
            catch (Exception e)
            {
                Debug.LogException(e);
                selector.Clear();
                selector.Add(new IMGUIContainer(() => EditorGUILayout.HelpBox($"会場情報の取得に失敗しました", MessageType.Error)));
            }
        }

        VisualElement CreateVenuePicker(GroupID groupId, Venues venues, CancellationToken cancellationToken, VenueID venueIdToSelect)
        {
            var venuePicker = new VisualElement();
            venuePicker.Add(new Button(() => CreateNewVenue(groupId, cancellationToken)) { text = "新規作成" });
            venuePicker.Add(new Label() { text = "作成済みワールドから選ぶ", style = { marginTop = 12 } });
            venuePicker.Add(CreateVenueList(venues, venueIdToSelect));
            return venuePicker;
        }

        VisualElement CreateVenueList(Venues venues, VenueID venueIdToSelect)
        {
            var venueList = new ScrollView(ScrollViewMode.VerticalAndHorizontal)
            {
                style = { marginTop = 8, flexGrow = 1 }
            };

            foreach (var venue in venues.List.OrderBy(venue => venue.Name))
            {
                var venueButton = new Button(() => { reactiveCurrentVenue.Val = venue; })
                {
                    text = venue.Name,
                    style = { unityTextAlign = TextAnchor.MiddleLeft }
                };
                venueList.Add(venueButton);
            }

            reactiveCurrentVenue.Val = venues.List.Find(venue => venue.VenueId == venueIdToSelect);

            return venueList;
        }

        void CreateNewVenue(GroupID groupId, CancellationToken cancellationToken)
        {
            var postVenueService =
                new PostRegisterNewVenueService(
                    userInfo.VerifiedToken,
                    new PostNewVenuePayload("New World", "説明未設定", groupId.Value),
                    venue =>
                    {
                        _ = RefreshVenueSelector(cancellationToken, groupId, venue.VenueId);
                        reactiveCurrentVenue.Val = venue;
                    },
                    exception =>
                    {
                        Debug.LogException(exception);
                        selector.Add(new IMGUIContainer(() =>
                            EditorGUILayout.HelpBox($"新規会場の登録ができませんでした。{exception.Message}", MessageType.Error)));
                    });
            postVenueService.Run(cancellationToken);
        }
    }
}
