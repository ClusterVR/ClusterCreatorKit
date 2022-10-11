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
        [SerializeField, HideInInspector] RectTransform rectTransform;
        [SerializeField, HideInInspector] Canvas canvas;

        RectTransform IPlayerLocalUI.RectTransform
        {
            get
            {
                if (rectTransform != null)
                {
                    return rectTransform;
                }
                if (this == null)
                {
                    return null;
                }
                return rectTransform = GetComponent<RectTransform>();
            }
        }

        Canvas Canvas
        {
            get
            {
                if (canvas != null)
                {
                    return canvas;
                }
                if (this == null)
                {
                    return null;
                }
                return canvas = GetComponent<Canvas>();
            }
        }

        public void SetEnabled(bool enabled)
        {
            var canvas = Canvas;
            if (canvas != null)
            {
                canvas.enabled = enabled;
            }
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
            if (rectTransform == null || rectTransform.gameObject != gameObject)
            {
                rectTransform = GetComponent<RectTransform>();
            }
            if (canvas == null || canvas.gameObject != gameObject)
            {
                canvas = GetComponent<Canvas>();
            }

            LimitSortingOrders();
        }

        void LimitSortingOrders()
        {
            if (canvas == null)
            {
                return;
            }
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            LimitSortingOrder(canvas);
            foreach (var childCanvas in gameObject.GetComponentsInChildren<Canvas>(true))
            {
                if (childCanvas.overrideSorting)
                {
                    LimitSortingOrder(childCanvas);
                }
            }
        }

        void LimitSortingOrder(Canvas canvas)
        {
            if (canvas.sortingOrder > 100)
            {
                canvas.sortingOrder = 100;
            }
            else if (canvas.sortingOrder < -100)
            {
                canvas.sortingOrder = -100;
            }
        }

        void Reset()
        {
            rectTransform = GetComponent<RectTransform>();
            canvas = GetComponent<Canvas>();

#if UNITY_EDITOR
            void CreateSafeAreaIfNot()
            {
                foreach (Transform child in transform)
                {
                    if (child.GetComponent<SafeArea>() != null)
                    {
                        return;
                    }
                }

                var safeArea = ObjectFactory.CreateGameObject("SafeArea", typeof(SafeArea));
                safeArea.transform.SetParent(transform);
            }

            CreateSafeAreaIfNot();
#endif
        }
    }
}
