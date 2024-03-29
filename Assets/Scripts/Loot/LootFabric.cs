using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Loot
{
    public class LootFabric : MonoBehaviour
    {
        [SerializeField] private int minRandomCount = 1, maxRandomCount = 5;
        [SerializeField] private Item[] m_ItemsCollection;
        [SerializeField] private GameObject m_LootObjectPrefab;

        [ContextMenu("Generate IDs")]
        public void GenerateIDs()
        {
            for (int i = 0; i < m_ItemsCollection.Length; i++) {
                m_ItemsCollection[i].Index = i;
            }
        }

        public LootInfo GetItem(int id, int count)
        {
            return new LootInfo(m_ItemsCollection[id], count);
        }

        public LootObject InstantiateLootObject(int id, Vector2 position, int count = 1)
        {
            LootObject loot = Instantiate(m_LootObjectPrefab, position, Quaternion.identity).GetComponent<LootObject>();
            loot.SetItem(GetItem(id, count));

            return loot;
        }

        public LootObject InstantiateRandomLootObject(Vector2 position)
        {
            int id = Random.Range(0, m_ItemsCollection.Length);
            int count = Random.Range(minRandomCount, maxRandomCount);

            return InstantiateLootObject(id, position, count);
        }
    }

    public class LootInfo
    {
        public int ID {  get; private set; }
        public string Name { get; private set; }
        public Sprite Sprite { get; private set; }
        public int Count { get; private set; }

        public LootInfo(Item item, int count)
        {
            ID = item.Index;
            Name = item.GetName();
            Sprite = item.GetSprite();
            Count = count;
        }

        public void SetCount(int value)
        {
            Count = value;
        }
    }
}

