using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ClusterVR.CreatorKit.Preview.PlayerController
{
    // 画面上のクリックやドラッグを受け取る用のComponentです。PreviewOnly内CanvasのPanelにアタッチされています。
    public class DesktopPointerEventListener : MonoBehaviour, IDragHandler, IPointerClickHandler
    {
        public event Action<Vector2> OnDragged;
        public event Action<Vector2> OnClicked;
        
        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            OnDragged?.Invoke(eventData.delta / Screen.height);
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            if (!eventData.dragging) OnClicked?.Invoke(eventData.position);
        }
    }
}
