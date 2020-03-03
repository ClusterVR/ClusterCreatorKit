using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ClusterVR.CreatorKit.Preview.PlayerController
{
    //マウスドラッグによってカメラの向きを調整するためのComponentです。PreviewOnly内CanvasのPanelにアタッチされています。
    public class DesktopMouseDragListener : MonoBehaviour, IDragHandler
    {
        public Action<Vector2> OnMouseDrag;

        public void OnDrag(PointerEventData eventData)
        {
            OnMouseDrag.Invoke(eventData.delta / Screen.height);
        }
    }
}
