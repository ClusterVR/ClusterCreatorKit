using System;
using System.IO;
using System.Linq;
using ClusterVR.CreatorKit.Editor.Preview.EditorUI;
using ClusterVR.CreatorKit.Editor.Preview.RoomState;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Preview.WebTrigger
{
    public class WebTriggerWindow : EditorWindow
    {
        const string messageWhenNotPlayMode = "ウェブトリガーは実行中のみ使用可能です";

        [MenuItem("Cluster/Preview/WebTriggerWindow")]
        public static void ShowWindow()
        {
            var window = GetWindow<WebTriggerWindow>();
            window.titleContent = new GUIContent("WebTriggerWindow");
        }

        [SerializeField] WebTrigger trigger;
        VisualElement triggersRoot;

        public void OnEnable()
        {
            var root = rootVisualElement;
            root.Add(GenerateControlSection());
            root.Add(UiUtils.Separator());
            root.Add(GenerateTriggersSection());
            root.Add(UiUtils.Separator());

            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        void OnDisable()
        {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
        }

        void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            triggersRoot.SetEnabled(state == PlayModeStateChange.EnteredPlayMode);
        }

        VisualElement GenerateControlSection()
        {
            var controlSection = EditorUIGenerator.GenerateSection();
            var loadJsonButton = new Button(LoadJsonTrigger) {text = "JSONを読み込む"};
            controlSection.Add(loadJsonButton);

            return controlSection;
        }

        void LoadJsonTrigger()
        {
            var filePath = EditorUtility.OpenFilePanel("JSONを読み込む", "", "json");
            if (string.IsNullOrEmpty(filePath)) return;
            if (TryLoadTriggerJson(filePath, out var trigger))
            {
                this.trigger = trigger;
                UpdateTriggerButtons(trigger);
            }
        }

        static bool TryLoadTriggerJson(string filePath, out WebTrigger trigger)
        {
            try
            {
                trigger = JsonUtility.FromJson<WebTrigger>(File.ReadAllText(filePath));
                return Validate(trigger);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                trigger = default;
                return false;
            }
        }

        void UpdateTriggerButtons(WebTrigger webTrigger)
        {
            triggersRoot.Clear();
            if (trigger == null) return;
            foreach (var trigger in webTrigger.triggers)
            {
                triggersRoot.Add(GenerateTriggerButton(trigger));
            }
        }

        static bool Validate(WebTrigger webTrigger)
        {
            bool valid = true;
            foreach (var trigger in webTrigger.triggers)
            {
                foreach (var state in trigger.state)
                {
                    if (!TryGetStateValue(state.type, state.value, default, out _))
                    {
                        Debug.LogError($"{trigger.displayName}の{state.key}のValueのパースに失敗しました");
                        valid = false;
                    }
                }
            }

            return valid;
        }

        VisualElement GenerateTriggersSection()
        {
            triggersRoot = EditorUIGenerator.GenerateSection();
            UpdateTriggerButtons(trigger);
            triggersRoot.SetEnabled(Application.isPlaying);
            return triggersRoot;
        }

        VisualElement GenerateTriggerButton(Trigger trigger)
        {
            var button = new Button(() => OnTriggerPressed(trigger))
            {
                text = trigger.displayName,
            };
            if (trigger.color != null && trigger.color.Length >= 3)
            {
                var color = new Color(trigger.color[0], trigger.color[1], trigger.color[2]);
                button.style.backgroundColor = color;
            }
            return button;
        }

        static void OnTriggerPressed(Trigger trigger)
        {
            if (!Bootstrap.IsInPlayMode)
            {
                Debug.LogWarning(messageWhenNotPlayMode);
                return;
            }

            if (trigger.showConfirmDialog)
            {
                var ok = EditorUtility.DisplayDialog($"{trigger.displayName}を実行します。よろしいですか？",
                    $"Category: {trigger.category}",
                    "トリガーを実行する",
                    "キャンセル");
                if (!ok) return;
            }

            if(!Bootstrap.SignalGenerator.TryGet(out var signal)) return;
            foreach (var state in trigger.state)
            {
                var hasValue = TryGetStateValue(state.type, state.value, signal, out var stateValue);
                Assert.IsTrue(hasValue);
                var key = GetStateKey(state.key);
                Bootstrap.RoomStateRepository.Update(key, stateValue);
                Debug.Log($"{state.key}を更新しました");
            }

            Bootstrap.GimmickManager.OnStateUpdated(trigger.state.Select(s => s.key).Select(GetStateKey));
        }

        static string GetStateKey(string key) => RoomStateKey.GetGlobalKey(key);

        static bool TryGetStateValue(string type, string value, StateValue signal, out StateValue stateValue)
        {
            stateValue = default;
            switch (type)
            {
                case "signal":
                    stateValue = signal;
                    return true;
                case "bool":
                    if (bool.TryParse(value, out var boolValue))
                    {
                        stateValue = new StateValue(boolValue);
                        return true;
                    }
                    else return false;
                case "integer":
                    if (int.TryParse(value, out var integerValue))
                    {
                        stateValue = new StateValue(integerValue);
                        return true;
                    }
                    else return false;
                case "float":
                    if (float.TryParse(value, out var floatValue))
                    {
                        stateValue = new StateValue(floatValue);
                        return true;
                    }
                    else return false;
                default:
                    return false;
            }
        }
    }
}