using UnityEngine;

namespace Loot
{
    [CreateAssetMenu(fileName = "Item", menuName = "Loot/Item")]
    public class Item : ScriptableObject
    {
        [SerializeField] private string m_ItemName;
        [SerializeField] private Sprite m_Sprite;
        [Space]
        [SerializeField] private int m_Index;

        public string GetName() => m_ItemName;
        public Sprite GetSprite() => m_Sprite;
        public int Index
        {
            get => m_Index;
            set => m_Index = value;
        }
    }
}

