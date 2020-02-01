using UnityEngine;

namespace DapperDino.GGJ2020.Parts
{
    [CreateAssetMenu(fileName = "New Part Type", menuName = "Parts/Part Type")]
    public class PartType : ScriptableObject
    {
        [SerializeField] private new string name = "New Part Name";

        public string Name => name;
    }
}
