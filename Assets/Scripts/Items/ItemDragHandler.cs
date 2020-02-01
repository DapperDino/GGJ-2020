using UnityEngine;
using UnityEngine.EventSystems;

namespace DapperDino.GGJ2020.Items
{
    public abstract class ItemDragHandler : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        [SerializeField] private Transform dragParent = null;
        [SerializeField] private CanvasGroup canvasGroup = null;

        private Transform originalParent;

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                originalParent = transform.parent;

                transform.SetParent(dragParent);

                canvasGroup.blocksRaycasts = false;
            }
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                transform.position = eventData.position;
            }
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                transform.SetParent(originalParent);

                transform.localPosition = Vector3.zero;

                canvasGroup.blocksRaycasts = true;
            }
        }
    }
}
