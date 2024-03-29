using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class PopupItem : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private TMP_Text m_Title;
        private int m_EnumID;

        public event Action<int> onSelected;

        public void OnPointerDown(PointerEventData eventData)
        {
            onSelected?.Invoke(m_EnumID);
        }

        public void SetData(string title, int enumID)
        {
            m_Title.text = title;
            m_EnumID = enumID;
        }
    }
}

