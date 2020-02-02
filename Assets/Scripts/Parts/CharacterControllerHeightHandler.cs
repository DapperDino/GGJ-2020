using DapperDino.GGJ2020.Items;
using UnityEngine;

namespace DapperDino.GGJ2020.Parts
{
    public class CharacterControllerHeightHandler : MonoBehaviour
    {
        [SerializeField] private InventoryBehaviour inventoryBehaviour = null;
        [SerializeField] private CharacterController characterController = null;

        [SerializeField] private PartType headPartType = null;
        [SerializeField] private PartType legsPartType = null;

        private Item head;
        private Item legs;

        private void OnEnable()
        {
            inventoryBehaviour.Inventory.OnEquipmentSlotUpdate += HandleEquipmentSlotUpdate;
        }

        private void OnDisable()
        {
            inventoryBehaviour.Inventory.OnEquipmentSlotUpdate -= HandleEquipmentSlotUpdate;
        }

        private void HandleEquipmentSlotUpdate(PartType partType, Item item)
        {
            if (partType == headPartType)
            {
                head = item;
            }
            else if (partType == legsPartType)
            {
                //if (legs != null)
                //{
                //    transform.position -= new Vector3(0f, legs.Height, 0f);
                //}

                legs = item;

                if (legs != null)
                {
                    transform.position += new Vector3(0f, legs.Height, 0f);
                }
            }
            else
            {
                return;
            }

            UpdateHeight();
        }

        public void UpdateHeight()
        {
            float height = 1f;

            if (head != null)
            {
                height += head.Height;
            }

            if (legs != null)
            {
                height += legs.Height;
            }

            characterController.height = height;

            if (head != null && legs != null)
            {
                float center = (head.Height - legs.Height) / 2f;

                characterController.center = new Vector3(0f, center, 0f);

                return;
            }

            if (legs != null)
            {
                characterController.center = new Vector3(0f, -((height - 1f) / 2f), 0f);

                return;
            }

            characterController.center = new Vector3(0f, (height - 1f) / 2f, 0f);
        }
    }
}
