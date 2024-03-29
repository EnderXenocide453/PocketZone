using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Loot
{
    public class Inventory
    {
        private Dictionary<int, LootInfo> m_StoredItems;

        public LootInfo[] StoredItems => m_StoredItems.Values.ToArray();

        public Action<LootInfo, InventoryActionType> onInventoryChanges;

        public Inventory()
        {
            m_StoredItems = new Dictionary<int, LootInfo>();
        }

        public LootInfo GetLootInfo(int id)
        {
            if (id < 0 || id >= m_StoredItems.Count) { return null; }

            return m_StoredItems[id];
        }

        public void AddLoot(LootInfo loot)
        {
            if (!m_StoredItems.ContainsKey(loot.ID)) {
                m_StoredItems.Add(loot.ID, loot);
                onInventoryChanges?.Invoke(loot, InventoryActionType.Added);
                return;
            }

            LootInfo storedLoot = m_StoredItems[loot.ID];
            storedLoot.SetCount(loot.Count + storedLoot.Count);
            onInventoryChanges?.Invoke(storedLoot, InventoryActionType.Changed);
        }

        public int TakeLoot(LootInfo loot)
        {
            if (!m_StoredItems.ContainsKey(loot.ID))
                return 0;

            LootInfo storedLoot = m_StoredItems[loot.ID];
            int count = Mathf.Min(loot.Count, storedLoot.Count);
            storedLoot.SetCount(storedLoot.Count - count);

            if (storedLoot.Count == 0) {
                m_StoredItems.Remove(storedLoot.ID);
                onInventoryChanges?.Invoke(storedLoot, InventoryActionType.Removed);
                return count;
            }

            onInventoryChanges?.Invoke(storedLoot, InventoryActionType.Changed);
            return count;
        }
    }

    public enum InventoryActionType
    {
        Changed,
        Added,
        Removed
    }
}

