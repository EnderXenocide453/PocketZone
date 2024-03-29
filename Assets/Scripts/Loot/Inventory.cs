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
        public Action onLoaded;

        public Inventory(params LootInfo[] loot)
        {
            m_StoredItems = new Dictionary<int, LootInfo>();
            foreach (var item in loot) {
                AddLoot(item);
            }
        }

        public LootInfo GetLootInfo(int id)
        {
            if (!m_StoredItems.ContainsKey(id)) { return null; }

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

        public bool TakeLootByID(int id, int count)
        {
            if (!m_StoredItems.ContainsKey(id))
                return false;

            LootInfo storedLoot = m_StoredItems[id];
            if (storedLoot.Count < count) {
                return false;
            }

            storedLoot.SetCount(storedLoot.Count - count);
            Debug.Log(storedLoot.Count);

            if (storedLoot.Count == 0) {
                m_StoredItems.Remove(storedLoot.ID);
                onInventoryChanges?.Invoke(storedLoot, InventoryActionType.Removed);
                return true;
            } 

            onInventoryChanges?.Invoke(storedLoot, InventoryActionType.Changed);
            return true;
        }

        public void SetData(LootInfo[] data)
        {
            m_StoredItems = new Dictionary<int, LootInfo>();
            foreach (LootInfo item in data) {
                AddLoot(item);
            }

            onLoaded?.Invoke();
        }
    }

    public enum InventoryActionType
    {
        Changed,
        Added,
        Removed
    }
}

