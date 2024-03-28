using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Loot
{
    public class LootFabric : MonoBehaviour
    {
        [SerializeField] private Item[] m_ItemsCollection;
        [SerializeField] private GameObject m_LootObjectPrefab;

        [ContextMenu("Generate IDs")]
        public void GenerateIDs()
        {
            for (int i = 0; i < m_ItemsCollection.Length; i++) {
                m_ItemsCollection[i].Index = i;
            }
        }

        public LootInfo GetItem(int id)
        {
            return new LootInfo(m_ItemsCollection[id]);
        }

        public LootObject InstantiateLootObject(int id, Vector2 position)
        {
            LootObject loot = Instantiate(m_LootObjectPrefab, position, Quaternion.identity).GetComponent<LootObject>();
            loot.SetItem(GetItem(id));

            return loot;
        }
    }

    public struct LootInfo
    {
        public int ID {  get; private set; }
        public string Name { get; private set; }
        public Sprite Sprite { get; private set; }

        public LootInfo(Item item)
        {
            ID = item.Index;
            Name = item.GetName();
            Sprite = item.GetSprite();
        }
    }
}

