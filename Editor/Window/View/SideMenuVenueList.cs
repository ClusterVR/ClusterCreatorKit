using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ClusterVR.CreatorKit.Editor.Api.RPC;
using ClusterVR.CreatorKit.Editor.Api.User;
using ClusterVR.CreatorKit.Editor.Api.Venue;
using ClusterVR.CreatorKit.Editor.ProjectSettings;
using ClusterVR.CreatorKit.Translation;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Window.View
{
    public sealed class SideMenuVenueList : IDisposable
    {
        public readonly Reactive<Venue> reactiveCurrentVenue = new Reactive<Venue>();

        readonly UserInfo userInfo;

        readonly Dictionary<GroupID, Venues> loadedVenues = new Dictionary<GroupID, Venues>();

        VisualElement groupSelector;
        VisualElement venueSelector;

        CancellationTokenSource groupSelectCancellationTokenSource;
        CancellationTokenSource venueSelectCancellationTokenSource;

        public SideMenuVenueList(UserInfo userInfo)
        {
            this.userInfo = userInfo;
        }

        public void AddView(VisualElement parent)
        {
            groupSelector = new VisualElement()
            {
                style = { flexGrow = 1 }
            };
            venueSelector = new VisualElement()
            {
                style = { flexGrow = 1 }
            };
            parent.Add(groupSelector);
            RefreshGroupSelector();
        }

        public void RefetchVenueWithoutChangingSelection()
        {
            var currentVenue = reactiveCurrentVenue.Val;
            if (currentVenue != null)
            {
                RefreshGroupSelector(currentVenue.Group.Id, currentVenue.VenueId);
            }
            else
            {
                RefreshGroupSelector();
            }
        }

        void RefreshVenueSelector(GroupID groupId, VenueID venueIdToSelect)
        {
            CancelVenueSelector();
            venueSelectCancellationTokenSource = new();
            _ = RefreshVenueSelectorAsync(groupId, venueIdToSelect, venueSelectCancellationTokenSource.Token);
        }

        async Task RefreshVenueSelectorAsync(GroupID groupId, VenueID venueIdToSelect, CancellationToken cancellationToken)
        {
            venueSelector.Clear();
            venueSelector.Add(new Label() { text = TranslationTable.cck_loading });

            try
            {
                if (!loadedVenues.TryGetValue(groupId, out var venues))
                {
                    venues = await APIServiceClient.GetGroupVenues(groupId, userInfo.VerifiedToken, cancellationToken);
                    loadedVenues.Add(groupId, venues);
                }
                venueSelector.Clear();
                venueSelector.Add(CreateVenuePicker(groupId, venues, cancellationToken, venueIdToSelect));
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception e)
            {
                venueSelector.Clear();
                venueSelector.Add(new IMGUIContainer(() => EditorGUILayout.HelpBox(TranslationTable.cck_venue_info_fetch_failed, MessageType.Error)));
            }
        }

        void RefreshGroupSelector(GroupID groupIdToSelect = null, VenueID venueIdToSelect = null)
        {
            CancelGroupSelector();
            groupSelectCancellationTokenSource = new();
            _ = RefreshGroupSelectorAsync(groupSelectCancellationTokenSource.Token, groupIdToSelect, venueIdToSelect);
        }

        async Task RefreshGroupSelectorAsync(CancellationToken cancellationToken, GroupID groupIdToSelect = null,
            VenueID venueIdToSelect = null)
        {
            CancelVenueSelector();

            venueSelector.Clear();
            loadedVenues.Clear();

            groupSelector.Clear();
            groupSelector.Add(new Label() { text = TranslationTable.cck_loading });

            try
            {
                var groups = await APIServiceClient.GetGroups(userInfo.VerifiedToken, cancellationToken);
                groupSelector.Clear();
                groupSelector.Add(new Label(TranslationTable.cck_affiliated_team));
                if (groups.List.Count == 0)
                {
                    groupSelector.Add(new Label() { text = TranslationTable.cck_load_failed });
                }
                else
                {
                    var teamMenu = new PopupField<Group>(groups.List, 0, group => group.Name, group => group.Name);
                    teamMenu.RegisterValueChangedCallback(ev => RefreshVenueSelector(ev.newValue.Id, venueIdToSelect));
                    groupSelector.Add(teamMenu);

                    var groupToSelect = groups.List.Find(group => group.Id == groupIdToSelect) ?? groups.List[0];
                    teamMenu.SetValueWithoutNotify(groupToSelect);

                    RefreshVenueSelector(groupToSelect.Id, venueIdToSelect);
                }

                groupSelector.Add(UiUtils.Separator());

                groupSelector.Add(new Label() { text = TranslationTable.cck_world });
                groupSelector.Add(venueSelector);
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                groupSelector.Clear();
                groupSelector.Add(new Label(TranslationTable.cck_affiliated_team));
                groupSelector.Add(new Label() { text = TranslationTable.cck_load_failed });
            }
        }

        VisualElement CreateVenuePicker(GroupID groupId, Venues venues, CancellationToken cancellationToken, VenueID venueIdToSelect)
        {
            var venuePicker = new VisualElement();

            void OnNewVenueClicked()
            {
                var isBeta = ClusterCreatorKitSettings.instance.IsBeta;
                CreateNewVenue(groupId, isBeta, cancellationToken);
            }

            venuePicker.Add(new Button(OnNewVenueClicked) { text = TranslationTable.cck_create_new });
            venuePicker.Add(new Label() { text = TranslationTable.cck_select_from_existing_worlds, style = { marginTop = 12 } });
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
                    text = venue.IsBeta ? $"[beta] {venue.Name}" : venue.Name,
                    style = { unityTextAlign = TextAnchor.MiddleLeft }
                };
                venueList.Add(venueButton);
            }

            reactiveCurrentVenue.Val = venues.List.Find(venue => venue.VenueId == venueIdToSelect);

            return venueList;
        }

        void CreateNewVenue(GroupID groupId, bool isBeta, CancellationToken cancellationToken)
        {
            var postVenueService =
                new PostRegisterNewVenueService(
                    userInfo.VerifiedToken,
                    new PostNewVenuePayload(TranslationTable.cck_new_world_default_name, TranslationTable.cck_description_not_set, groupId.Value, isBeta),
                    venue =>
                    {
                        RefreshGroupSelector(groupId, venue.VenueId);
                        reactiveCurrentVenue.Val = venue;
                    },
                    exception =>
                    {
                        Debug.LogException(exception);
                        groupSelector.Add(new IMGUIContainer(() =>
                            EditorGUILayout.HelpBox(TranslationUtility.GetMessage(TranslationTable.cck_venue_registration_failed, exception.Message), MessageType.Error)));
                    });
            postVenueService.Run(cancellationToken);
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

        void IDisposable.Dispose()
        {
            CancelGroupSelector();
            CancelVenueSelector();
        }
    }
}
