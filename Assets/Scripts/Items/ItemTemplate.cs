using DapperDino.GGJ2020.Parts;
using UnityEngine;

namespace DapperDino.GGJ2020.Items
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Items/Item")]
    public class ItemTemplate : ScriptableObject
    {
        [SerializeField] private int id = -1;
        [SerializeField] private new string name = "New Item Name";
        [SerializeField] private PartType partType = null;

        [Header("UI")]
        [SerializeField] private Sprite icon = null;

        public int Id => id;
        public string Name => name;
        public PartType PartType => partType;
        public Sprite Icon => icon;
    }
}
