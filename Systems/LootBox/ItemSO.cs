using UnityEngine;

namespace SombraStudios.Shared.Systems.LootBox
{
    [CreateAssetMenu(fileName = "New Item Data", menuName = "Item Data", order = 51)]
    public class ItemSO : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField, Multiline] private string _description;
        [SerializeField] private float _rarety = 0f;

        public float Rarety { get { return _rarety; } }
        public string Name { get { return _name; } }
        public string Description { get { return _description; } }
    }
}
