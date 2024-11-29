using UnityEngine;

namespace ClusterVR.CreatorKit.World.Implements.PlayerLocalUI
{
    [DisallowMultipleComponent, RequireComponent(typeof(RectTransform)), ExecuteAlways]
    public sealed class SafeArea : MonoBehaviour, ISafeArea
    {
        [SerializeField] SafeAreaScope scope = SafeAreaScope.Legacy;
        RectTransform rectTransform;

        public RectTransform RectTransform =>
            rectTransform == null ? rectTransform = GetComponent<RectTransform>() : rectTransform;

        public SafeAreaScope Scope => scope;

        DrivenRectTransformTracker tracker = new DrivenRectTransformTracker();

#if !CLUSTER_CREATOR_KIT_DISABLE_PREVIEW
        void Update()
        {
            var safeArea = Screen.safeArea;
            var (offsetMin, offsetMax) = scope switch
            {
                SafeAreaScope.Legacy => (new Vector2(56f, 88f), -new Vector2(56f, 0f)),
                SafeAreaScope.System => (new Vector2(12f, 0f), -new Vector2(12f, 0)),
                SafeAreaScope.Device => (Vector2.zero, Vector2.zero),
                _ => (Vector2.zero, Vector2.zero)
            };
            SetSafeArea(
                new Vector2(safeArea.xMin / Screen.width, safeArea.yMin / Screen.height),
                new Vector2(safeArea.xMax / Screen.width, safeArea.yMax / Screen.height),
                offsetMin,
                offsetMax
            );
        }
#endif

        void OnValidate()
        {
            tracker.Add(this, RectTransform, DrivenTransformProperties.All);
        }

        void Reset()
        {
            scope = SafeAreaScope.System;
        }

        public void SetSafeArea(Vector2 anchorMin, Vector2 anchorMax, Vector2 offsetMin, Vector2 offsetMax)
        {
            var rectTransform = RectTransform;
            rectTransform.localRotation = Quaternion.identity;
            rectTransform.localScale = Vector3.one;
            rectTransform.anchorMin = anchorMin;
            rectTransform.anchorMax = anchorMax;
            rectTransform.localPosition = Vector3.zero;
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
            rectTransform.offsetMin = offsetMin;
            rectTransform.offsetMax = offsetMax;
        }
    }
}
