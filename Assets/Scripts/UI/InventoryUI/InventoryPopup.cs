using Loot;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace UI
{
    public class InventoryPopup : MonoBehaviour
    {
        [SerializeField] GameObject m_PopupItemPrefab;

        public Action<InteractType> onSelected;

        public void SetData(params LootInteractionData[] data)
        {
            Clear();
            foreach (var item in data)
                AddItem(item);
        }

        public void Show(Vector2 position)
        {
            transform.position = position;
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void AddItem(LootInteractionData data)
        {
            PopupItem item = Instantiate(m_PopupItemPrefab, transform).GetComponent<PopupItem>();
            item.SetData(data.Title, (int)data.Type);

            item.onSelected += OnSelected;
        }

        private void OnSelected(int id)
        {
            InteractType type = (InteractType)id;
            onSelected?.Invoke(type);
            Hide();
        }

        private void Clear()
        {
            for (int i = 0; i < transform.childCount; i++) {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
    }
}

