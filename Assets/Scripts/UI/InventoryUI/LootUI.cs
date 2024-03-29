using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Loot
{
    public class LootUI : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private Image m_LootIcon;
        [SerializeField] private TMP_Text m_LootName;
        [SerializeField] private TMP_Text m_LootCount;

        private int m_Index;

        public event Action<int, PointerEventData> onClick;

        public void SetData(LootInfo lootInfo)
        {
            m_LootIcon.sprite = lootInfo.Sprite;
            m_LootName.text = lootInfo.Name;
            m_LootCount.text = lootInfo.Count.ToString();

            m_Index = lootInfo.ID;
        }

        public void SetCount(int count)
        {
            if (count == 0) {
                m_LootCount.text = string.Empty;
                return;
            }

            m_LootCount.text = count.ToString();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            onClick?.Invoke(m_Index, eventData);
        }
    }
}
