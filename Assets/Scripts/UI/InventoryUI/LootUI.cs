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
        [SerializeField] private TMPro.TMP_Text m_LootName;
        [SerializeField] private TMPro.TMP_Text m_LootCount;

        public void SetData(LootInfo lootInfo)
        {
            m_LootIcon.sprite = lootInfo.Sprite;
            m_LootName.text = lootInfo.Name;
            m_LootCount.text = lootInfo.Count.ToString();
        }

        public void SetCount(int count)
        {
            m_LootCount.text = count.ToString();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log(eventData.button);
        }
    }
}
