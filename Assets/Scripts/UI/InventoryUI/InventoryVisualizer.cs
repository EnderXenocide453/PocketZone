using Input;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Loot
{
    public class InventoryVisualizer : MonoBehaviour
    {
        [SerializeField] private RectTransform m_ItemContainer;
        [SerializeField] private GameObject m_ItemUIPrefab;

        private Inventory m_Inventory;
        private Dictionary<int, LootUI> m_LootUIElements;

        [Inject]
        public void Construct(Inventory inventory, InputPresenter input)
        {
            m_Inventory = inventory;
            inventory.onInventoryChanges += OnInventoryChanges;

            input.OnInventoryAction += ToggleInventory;

            Draw();
        }

        private void ToggleInventory()
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }

        private void OnInventoryChanges(LootInfo loot, InventoryActionType type)
        {
            if (loot == null) return;

            switch (type) {
                case InventoryActionType.Added:
                    AddItem(loot);
                    break;
                case InventoryActionType.Changed:
                    RedrawItem(loot);
                    break;
                case InventoryActionType.Removed:
                    RemoveItem(loot);
                    break;
            }
        }

        private void AddItem(LootInfo loot)
        {
            if (m_LootUIElements.ContainsKey(loot.ID))
                return;

            LootUI ui = Instantiate(m_ItemUIPrefab, m_ItemContainer).GetComponent<LootUI>();
            ui.SetData(loot);

            m_LootUIElements.Add(loot.ID, ui);
        }

        private void RedrawItem(LootInfo loot)
        {
            if (!m_LootUIElements.ContainsKey(loot.ID)) {
                AddItem(loot);
                return;
            }

            m_LootUIElements[loot.ID].SetCount(loot.Count);
        }

        private void RemoveItem(LootInfo loot)
        {
            if (!m_LootUIElements.ContainsKey(loot.ID)) {
                return;
            }

            Destroy(m_LootUIElements[loot.ID].gameObject);
            m_LootUIElements.Remove(loot.ID);
        }

        private void Draw()
        {
            Clear();

            foreach (var item in m_Inventory.StoredItems)
                AddItem(item);
        }

        private void Clear()
        {
            if (m_LootUIElements != null)
                foreach (var item in m_LootUIElements.Values) {
                    Destroy(item.gameObject);
                }

            m_LootUIElements = new Dictionary<int, LootUI>();
        }


    }
}

