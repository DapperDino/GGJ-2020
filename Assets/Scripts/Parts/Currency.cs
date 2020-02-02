using DapperDino.GGJ2020.Items;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DapperDino.GGJ2020.Parts
{
    [Serializable]
    public class UnityStringEvent : UnityEvent<string> { }

    public class Currency : MonoBehaviour
    {
        [SerializeField] private InventoryBehaviour inventoryBehaviour = null;
        [SerializeField] private GameObject[] repairIcons = new GameObject[0];
        [SerializeField] private GameObject[] salvageIcons = new GameObject[0];
        [SerializeField] private UnityStringEvent OnScrapUpdated = new UnityStringEvent();

        private int scrap;
        private bool iconsEnabled;

        private void Start()
        {
            foreach (var icon in repairIcons)
            {
                var button = icon.GetComponent<Button>();
                button.onClick.AddListener(() =>
                {
                    var item = button.GetComponentInParent<SlotBehaviour>().Item;

                    if (item != null)
                    {
                        if (HasEnoughScrap(item.RepairCost))
                        {
                            RemoveScrap(item.RepairCost);
                            item.CurrentHealth = item.MaxHealth;
                        }
                    }
                });
            }

            foreach (var icon in salvageIcons)
            {
                var button = icon.GetComponent<Button>();
                button.onClick.AddListener(() =>
                {
                    var item = button.GetComponentInParent<SlotBehaviour>().Item;

                    if (item != null)
                    {
                        scrap += item.CurrentHealth;

                        OnScrapUpdated.Invoke($"Scraps: {scrap}");

                        inventoryBehaviour.Inventory.RemoveItem(item);
                    }
                });
            }
        }

        public void AddScrap(int scrapToAdd)
        {
            scrap += scrapToAdd;

            OnScrapUpdated.Invoke($"Scraps: {scrap}");
        }

        public void RemoveScrap(int scrapToRemove)
        {
            scrap = Mathf.Max(scrap - scrapToRemove, 0);

            OnScrapUpdated.Invoke($"Scraps: {scrap}");
        }

        public bool HasEnoughScrap(int scrapRequired) => scrap >= scrapRequired;

        private void Update()
        {
            if (inventoryBehaviour == null) { return; }

            if (Vector3.Distance(Vector3.zero, inventoryBehaviour.transform.position) < 30f)
            {
                if (iconsEnabled) { return; }

                foreach (var icon in repairIcons)
                {
                    icon.SetActive(true);
                }

                foreach (var icon in salvageIcons)
                {
                    icon.SetActive(true);
                }

                iconsEnabled = true;
            }
            else
            {
                if (!iconsEnabled) { return; }

                foreach (var icon in repairIcons)
                {
                    icon.SetActive(false);
                }

                foreach (var icon in salvageIcons)
                {
                    icon.SetActive(false);
                }

                iconsEnabled = false;
            }
        }
    }
}
