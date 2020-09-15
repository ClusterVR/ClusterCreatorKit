using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClusterVR.CreatorKit.Editor.Core.Venue;
using ClusterVR.CreatorKit.Editor.Core.Venue.Json;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Venue
{
    public class SideMenuVenueList
    {
        public readonly Reactive<bool> reactiveForceLogout = new Reactive<bool>();
        public readonly Reactive<Core.Venue.Json.Venue> reactiveCurrentVenue = new Reactive<Core.Venue.Json.Venue>();

        readonly UserInfo userInfo;

        readonly Dictionary<GroupID, Venues> allVenues = new Dictionary<GroupID, Venues>();

        VisualElement selector;

        public SideMenuVenueList(UserInfo userInfo)
        {
            this.userInfo = userInfo;
        }

        public void AddView(VisualElement parent)
        {
            selector = new VisualElement() {style = {flexGrow = 1}};
            parent.Add(selector);
            _ = RefreshVenueSelector();
        }

        public void RefetchVenueWithoutChangingSelection()
        {
            var currentVenue = reactiveCurrentVenue.Val;
            if (currentVenue != null)
            {
                _ = RefreshVenueSelector(currentVenue.Group.Id, currentVenue.VenueId);
            }
            else
            {
                _ = RefreshVenueSelector();
            }
        }

        async Task RefreshVenueSelector(GroupID groupIdToSelect = null, VenueID venueIdToSelect = null)
        {
            selector.Clear();
            selector.Add(new Label() {text = "loading..."});

            var venuePickerHolder = new VisualElement(){style = {flexGrow = 1}};
            void RecreateVenuePicker(GroupID groupId)
            {
                venuePickerHolder.Clear();
                venuePickerHolder.Add(CreateVenuePicker(groupId, allVenues[groupId], venueIdToSelect));
            }

            try
            {
                // Fetch all data
                var groups = await APIServiceClient.GetGroups.Call(Empty.Value, userInfo.VerifiedToken, 3);
                foreach (var group in groups.List)
                {
                    allVenues[group.Id] = await APIServiceClient.GetGroupVenues.Call(group.Id, userInfo.VerifiedToken, 3);
                }

                // Construct menu
                selector.Clear();
                selector.Add(new Label("所属チーム"));
                if (groups.List.Count == 0)
                {
                    selector.Add(new Label(){text = "読み込みに失敗しました"});
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

                selector.Add(new Label(){text="ワールド"});
                selector.Add(venuePickerHolder);

                selector.Add(UiUtils.Separator());

                selector.Add(new Label("ユーザーID"));
                var userSelector = new VisualElement(){style = {flexShrink = 0}};
                userSelector.Add(new Label(userInfo.Username));
                userSelector.Add(new Button(() => reactiveForceLogout.Val = true) {text = "アカウント切替"});
                selector.Add(userSelector);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                selector.Clear();
                selector.Add(new IMGUIContainer(() => EditorGUILayout.HelpBox($"会場情報の取得に失敗しました", MessageType.Error)));
            }
        }

        VisualElement CreateVenuePicker(GroupID groupId, Venues venues, VenueID venueIdToSelect = null)
        {
            var venueList = new ScrollView(ScrollViewMode.Vertical)
            {
                style = {marginTop = 8, flexGrow = 1}
            };
            venueList.Add(new Button(() => CreateNewVenue(groupId)) {text = "新規作成"});

            venueList.Add(new Label(){text = "作成済みワールドから選ぶ", style = {marginTop = 12}});
            foreach (var venue in venues.List.OrderBy(venue => venue.Name))
            {
                var venueButton = new Button(() => { reactiveCurrentVenue.Val = venue; })
                {
                    text = venue.Name,
                    style = {unityTextAlign = TextAnchor.MiddleLeft},
                };
                venueList.Add(venueButton);
            }

            reactiveCurrentVenue.Val = venues.List.Find(venue => venue.VenueId == venueIdToSelect);

            return venueList;
        }

        void CreateNewVenue(GroupID groupId)
        {
            var newVenuePayload = new PostNewVenuePayload
            {
                name = "NewVenue",
                description = "説明未設定",
                groupId = groupId.Value,
            };

            var postVenueService =
                new PostRegisterNewVenueService(
                    userInfo.VerifiedToken,
                    newVenuePayload,
                    venue =>
                    {
                        _ = RefreshVenueSelector(groupId, venue.VenueId);
                        reactiveCurrentVenue.Val = venue;
                    },
                    exception =>
                    {
                        Debug.LogException(exception);
                        selector.Add(new IMGUIContainer(() => EditorGUILayout.HelpBox($"新規会場の登録ができませんでした。{exception.Message}", MessageType.Error)));
                    });
            postVenueService.Run();
        }
    }
}
