using System;
using System.Collections.Generic;
using UnityEngine;

namespace Loot
{
    public class Inventory
    {
        private Dictionary<int, int> m_StoredItems;

        public Action<int, InventoryActionType> onInventoryChanges;

        public Inventory()
        {
            m_StoredItems = new Dictionary<int, int>();
            onInventoryChanges += (int id, InventoryActionType type) => Debug.Log($"{id}\n{type}");
        }

        public void AddLoot(int lootId, int count = 1)
        {
            if (!m_StoredItems.ContainsKey(lootId)) {
                m_StoredItems.Add(lootId, count);
                onInventoryChanges?.Invoke(count, InventoryActionType.Added);
                return;
            }

            m_StoredItems[lootId] += count;
            onInventoryChanges?.Invoke(m_StoredItems[lootId], InventoryActionType.Changed);
        }

        public int TakeLoot(int lootId, int count = 1)
        {
            if (!m_StoredItems.ContainsKey(lootId))
                return 0;

            count = Mathf.Min(count, m_StoredItems[lootId]);
            m_StoredItems[lootId] -= count;

            if (m_StoredItems[lootId] == 0) {
                m_StoredItems.Remove(lootId);
                onInventoryChanges?.Invoke(0, InventoryActionType.Removed);
                return count;
            }

            onInventoryChanges?.Invoke(count, InventoryActionType.Changed);
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

