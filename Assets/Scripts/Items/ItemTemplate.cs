using DapperDino.GGJ2020.Parts;
using UnityEngine;

namespace DapperDino.GGJ2020.Items
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Items/Item")]
    public class ItemTemplate : ScriptableObject
    {
        [SerializeField] private int id = -1;
        [SerializeField] private new string name = "New Item Name";
        [SerializeField] private float height = 0f;
        [SerializeField] private int maxHealth = 100;
        [SerializeField] private PartType partType = null;
        [SerializeField] private GameObject prefab = null;
        [SerializeField] private GameObject pickupPrefab = null;

        [Header("UI")]
        [SerializeField] private Sprite icon = null;

        public int Id => id;
        public string Name => name;
        public float Height => height;
        public int MaxHealth => maxHealth;
        public PartType PartType => partType;
        public GameObject Prefab => prefab;
        public GameObject PickupPrefab => pickupPrefab;
        public Sprite Icon => icon;
    }
}
