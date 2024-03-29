using System;
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

        public LootInteractionData[] Interactions => new LootInteractionData[]
        {
            new LootInteractionData("Бросить", InteractType.Drop)
        };
    }

    public struct LootInteractionData
    {
        private string m_Title;
        private InteractType m_Type;

        public string Title => m_Title;
        public InteractType Type => m_Type;

        public LootInteractionData(string title, InteractType type)
        {
            m_Title = title;
            m_Type = type;
        }
    }

    public enum InteractType
    {
        Drop,
        Use
    }
}

