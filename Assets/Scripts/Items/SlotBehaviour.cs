using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DapperDino.GGJ2020.Items
{
    public abstract class SlotBehaviour : MonoBehaviour, IDropHandler
    {
        [SerializeField] protected InventoryBehaviour inventoryBehaviour = null;

        [Header("UI")]
        [SerializeField] private GameObject slotDisplay = null;
        [SerializeField] private Image image = null;

        public abstract void OnDrop(PointerEventData eventData);

        protected void UpdateSlot(Item item)
        {
            if (item == null)
            {
                slotDisplay.SetActive(false);

                return;
            }

            image.sprite = item.Icon;

            slotDisplay.SetActive(true);
        }
    }
}
