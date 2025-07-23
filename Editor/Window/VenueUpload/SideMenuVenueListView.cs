using System;
using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.Editor.Api.Venue;
using ClusterVR.CreatorKit.Editor.Utils;
using ClusterVR.CreatorKit.Editor.Utils.Extensions;
using ClusterVR.CreatorKit.Editor.Window.View;
using ClusterVR.CreatorKit.Translation;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Window.VenueUpload
{
    public sealed class SideMenuVenueListView : VisualElement
    {
        readonly Label groupSelectorLoading;
        readonly Label groupSelectorAffiliatedTeam;
        readonly Label groupSelectorLoadFailed;
        readonly HelpBox venueSelectorVenueRegistrationFailed;
        readonly Label venueSelectorLoading;
        readonly HelpBox venueSelectorVenueInfoFetchFailed;
        readonly Button newVenueButton;
        readonly PopupField<Group> teamMenu;

        readonly ScrollView venueList;
        readonly TextField filterField;

        List<Group> groups;

        public event Action<GroupID> OnGroupSelected;
        public event Action OnNewVenueClicked;
        public event Action<string> OnFilterTextChanged;
        public event Action<Venue> OnVenueClicked;

        public SideMenuVenueListView()
        {
            var groupSelector = new VisualElement
            {
                style = { flexShrink = 0 }
            };
            var venueSelector = new VisualElement();
            hierarchy.Add(groupSelector);
            hierarchy.Add(venueSelector);

            groupSelectorLoading = new Label { text = TranslationTable.cck_loading };
            groupSelectorAffiliatedTeam = new Label { text = TranslationTable.cck_affiliated_team };
            groupSelectorLoadFailed = new Label { text = TranslationTable.cck_load_failed };
            groupSelector.Add(groupSelectorLoading);
            groupSelector.Add(groupSelectorAffiliatedTeam);
            groupSelector.Add(groupSelectorLoadFailed);

            teamMenu = new PopupField<Group>(new List<Group>(), 0,
                group => group?.Name ?? string.Empty,
                group => group?.Name ?? string.Empty);
            groupSelector.Add(teamMenu);

            groupSelector.Add(UiUtils.Separator());
            groupSelector.Add(new Label() { text = TranslationTable.cck_world });

            venueSelectorLoading = new Label { text = TranslationTable.cck_loading };
            venueSelectorVenueInfoFetchFailed = new HelpBox(TranslationTable.cck_venue_info_fetch_failed, HelpBoxMessageType.Error);
            venueSelector.Add(venueSelectorLoading);
            venueSelector.Add(venueSelectorVenueInfoFetchFailed);

            var venuePicker = new VisualElement();

            venueSelectorVenueRegistrationFailed = new HelpBox
            {
                messageType = HelpBoxMessageType.Error,
                style = { display = DisplayStyle.None }
            };
            venuePicker.Add(venueSelectorVenueRegistrationFailed);

            newVenueButton = new Button { text = TranslationTable.cck_create_new };
            venuePicker.Add(newVenueButton);
            venuePicker.Add(new Label() { text = TranslationTable.cck_select_from_existing_worlds, style = { marginTop = 12 } });

            filterField = new TextField()
            {
                style = { marginTop = 8 },
                label = TranslationTable.cck_search,
                isDelayed = true
            };
            filterField[0].style.minWidth = 50;
            venuePicker.Add(filterField);

            venueList = new ScrollView(ScrollViewMode.VerticalAndHorizontal)
            {
                style = { marginTop = 8, flexGrow = 1 }
            };
            venuePicker.Add(venueList);

            venueSelector.Add(venuePicker);

            RenderGroupSelectorEmpty();
            RenderVenueSelectorEmpty();

            teamMenu.RegisterValueChangedCallback(ev => OnGroupSelected?.Invoke(ev.newValue.Id));

            newVenueButton.clicked += () => OnNewVenueClicked?.Invoke();
            filterField.RegisterValueChangedCallback(ev => OnFilterTextChanged?.Invoke(ev.newValue));
        }

        public IDisposable Bind(SideMenuVenueList viewModel)
        {
            OnGroupSelected += viewModel.SelectGroup;
            OnNewVenueClicked += viewModel.OnNewVenueClicked;
            OnFilterTextChanged += viewModel.OnFilterTextChanged;
            OnVenueClicked += viewModel.OnVenueClicked;

            return Disposable.Create(() =>
                {
                    OnVenueClicked -= viewModel.OnVenueClicked;
                },
                ReactiveBinder.Bind(viewModel.FilterText, filterText => filterField.SetValueWithoutNotify(filterText)),
                ReactiveBinder.Bind(viewModel.CurrentGroupId, UpdateGroupSelection),
                ReactiveBinder.Bind(viewModel.GroupSelectorState, RenderGroupSelector),
                ReactiveBinder.Bind(viewModel.VenueSelectorState, RenderVenueSelector));
        }

        void UpdateGroupSelection(GroupID currentGroupId)
        {
            if (groups != null)
            {
                var groupToSelect = groups.FirstOrDefault(group => group.Id.Value == currentGroupId?.Value);
                teamMenu.SetValueWithoutNotify(groupToSelect);
            }
        }

        void RenderGroupSelector((SideMenuVenueList.GroupSelectorMode mode, Groups groups, GroupID currentGroupId) state)
        {
            switch (state.mode)
            {
                case SideMenuVenueList.GroupSelectorMode.Empty:
                    RenderGroupSelectorEmpty();
                    break;
                case SideMenuVenueList.GroupSelectorMode.Loading:
                    RenderGroupSelectorLoading();
                    break;
                case SideMenuVenueList.GroupSelectorMode.Loaded:
                    RenderGroupSelector(state.groups, state.currentGroupId);
                    break;
                case SideMenuVenueList.GroupSelectorMode.Failed:
                    RenderGroupSelectorLoadFailed();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        void RenderVenueSelector((SideMenuVenueList.VenueSelectorMode mode, List<Venue> venues) state)
        {
            switch (state.mode)
            {
                case SideMenuVenueList.VenueSelectorMode.Empty:
                    RenderVenueSelectorEmpty();
                    break;
                case SideMenuVenueList.VenueSelectorMode.Loading:
                    RenderVenueSelectorLoading();
                    break;
                case SideMenuVenueList.VenueSelectorMode.Loaded:
                    RenderVenueSelector(state.venues);
                    break;
                case SideMenuVenueList.VenueSelectorMode.Failed:
                    RenderVenueSelectorLoadFailed();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        void RenderGroupSelectorEmpty()
        {
            DisplayVenueRegistrationFailed(null);
            groupSelectorLoading.SetVisibility(false);
            groupSelectorAffiliatedTeam.SetVisibility(false);
            groupSelectorLoadFailed.SetVisibility(false);
            venueSelectorVenueRegistrationFailed.SetVisibility(false);
        }

        void RenderGroupSelectorLoading()
        {
            RenderGroupSelectorEmpty();
            groupSelectorLoading.SetVisibility(true);
        }

        void RenderGroupSelectorLoadFailed()
        {
            RenderGroupSelectorEmpty();
            groupSelectorAffiliatedTeam.SetVisibility(true);
            groupSelectorLoadFailed.SetVisibility(true);
        }

        void RenderGroupSelector(Groups groups, GroupID currentGroupId)
        {
            this.groups = groups.List;

            RenderGroupSelectorEmpty();
            groupSelectorAffiliatedTeam.SetVisibility(true);

            if (groups.List.Count == 0)
            {
                groupSelectorLoadFailed.SetVisibility(true);
                teamMenu.SetVisibility(false);
            }
            else
            {
                teamMenu.SetVisibility(true);
                teamMenu.choices = groups.List;
                UpdateGroupSelection(currentGroupId);
            }
        }

        void RenderVenueSelectorEmpty()
        {
            venueSelectorLoading.SetVisibility(false);
            venueSelectorVenueInfoFetchFailed.SetVisibility(false);
            newVenueButton.SetVisibility(false);
        }

        void RenderVenueSelectorLoading()
        {
            RenderVenueSelectorEmpty();
            venueSelectorLoading.SetVisibility(true);
        }

        void RenderVenueSelectorLoadFailed()
        {
            RenderVenueSelectorEmpty();
            venueSelectorVenueInfoFetchFailed.SetVisibility(true);
        }

        void RenderVenueSelector(List<Venue> venues)
        {
            RenderVenueSelectorEmpty();
            newVenueButton.SetVisibility(true);
            RenderVenueList(venues);
        }

        public void DisplayVenueRegistrationFailed(Exception exception)
        {
            if (exception != null)
            {
                venueSelectorVenueRegistrationFailed.SetVisibility(true);
                venueSelectorVenueRegistrationFailed.text = TranslationUtility.GetMessage(TranslationTable.cck_venue_registration_failed, exception.Message);
            }
            else
            {
                venueSelectorVenueRegistrationFailed.SetVisibility(false);
            }
        }

        void RenderVenueList(List<Venue> venues)
        {
            venueList.Clear();
            foreach (var venue in venues.OrderBy(venue => venue.Name))
            {
                var venueButton = new Button
                {
                    text = venue.IsBeta ? $"[beta] {venue.Name}" : venue.Name,
                    style = { unityTextAlign = TextAnchor.MiddleLeft }
                };
                venueButton.clicked += () => OnVenueClicked?.Invoke(venue);
                venueList.Add(venueButton);
            }
        }
    }
}
