using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ClusterVR.CreatorKit.Preview.PlayerController
{
    // 画面上のクリックやドラッグを受け取る用のComponentです。PreviewOnly内CanvasのPanelにアタッチされています。
    public class DesktopPointerEventListener : MonoBehaviour, IDragHandler, IPointerClickHandler
    {
        public event Action<Vector2> OnMoved;
        public event Action<Vector2> OnClicked;

        void Update()
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                OnMoved?.Invoke(new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) / Screen.height);
            }
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            if (Cursor.lockState != CursorLockMode.Locked)
            {
                OnMoved?.Invoke(eventData.delta / Screen.height);
            }
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            if (!eventData.dragging) OnClicked?.Invoke(eventData.position);
        }
    }
}
