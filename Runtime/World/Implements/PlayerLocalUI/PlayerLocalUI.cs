#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.UI;

namespace ClusterVR.CreatorKit.World.Implements.PlayerLocalUI
{
    [RequireComponent(typeof(Canvas), typeof(CanvasScaler)), DisallowMultipleComponent, ExecuteAlways]
    public sealed class PlayerLocalUI : MonoBehaviour, IPlayerLocalUI
    {
        RectTransform IPlayerLocalUI.RectTransform => GetComponent<RectTransform>();

        public void SetEnabled(bool enabled)
        {
            GetComponent<Canvas>().enabled = enabled;
        }

        void Start()
        {
            SetupCanvasScaler();
        }

        void SetupCanvasScaler()
        {
            var canvasScaler = GetComponent<CanvasScaler>();
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasScaler.referenceResolution = new Vector2(1024, 768);
            canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            canvasScaler.matchWidthOrHeight = 0.5f;
            canvasScaler.referencePixelsPerUnit = 100f;
        }
        
        void OnValidate()
        {
            var canvas = GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            LimitSortingOrder(canvas);
            foreach (var childCanvas in gameObject.GetComponentsInChildren<Canvas>(true))
            {
                if (childCanvas.overrideSorting) LimitSortingOrder(childCanvas);
            }
        }

        void LimitSortingOrder(Canvas canvas)
        {
            if (canvas.sortingOrder > 100) canvas.sortingOrder = 100;
            else if (canvas.sortingOrder < -100) canvas.sortingOrder = -100;
        }

        void Reset()
        {
#if UNITY_EDITOR
            void CreateSafeAreaIfNot()
            {
                foreach (Transform child in transform)
                {
                    if (child.GetComponent<SafeArea>() != null) return;
                }
                var safeArea = ObjectFactory.CreateGameObject("SafeArea", typeof(SafeArea));
                safeArea.transform.SetParent(transform);
            }
            CreateSafeAreaIfNot();
#endif
        }
    }
}