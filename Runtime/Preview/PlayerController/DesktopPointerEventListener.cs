using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ClusterVR.CreatorKit.Preview.PlayerController
{
    // 画面上のクリックやドラッグを受け取る用のComponentです。PreviewOnly内CanvasのPanelにアタッチされています。
    public class DesktopPointerEventListener : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        public event Action<Vector2> OnMoved;
        public event Action<Vector2> OnClicked;

        void Update()
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    // カーソルロックが解除されなくなった時の念の為
                    Cursor.lockState = CursorLockMode.None;
                    return;
                }

                var delta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
                delta *= 0.002f * Mathf.Pow(4f, CameraControlSettings.Sensitivity);
                if (CameraControlSettings.InvertHorizontal) delta.x = -delta.x;
                if (CameraControlSettings.InvertVertical) delta.y = -delta.y;
                OnMoved?.Invoke(delta);
            }
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            if (!eventData.dragging) OnClicked?.Invoke(eventData.position);
        }

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            Cursor.lockState = CursorLockMode.None;
        }

        void IDragHandler.OnDrag(PointerEventData eventData) { }

        void OnApplicationFocus(bool hasFocus)
        {
            if (!hasFocus) EventSystem.current.currentInputModule.DeactivateModule();
        }
    }
}
