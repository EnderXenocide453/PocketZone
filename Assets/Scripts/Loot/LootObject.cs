using Loot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Loot
{
    public class LootObject : MonoBehaviour
    {
        [SerializeField] SpriteRenderer m_SpriteRenderer;
        private LootInfo m_Info;
        private int m_Count;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<LootCollector>(out var collector)) {
                collector.AddLoot(m_Info.ID, m_Count);
                Destroy(gameObject);
            }
        }

        public void SetItem(LootInfo lootInfo, int count)
        {
            m_Info = lootInfo;
            m_SpriteRenderer.sprite = lootInfo.Sprite;
            m_Count = count;
        }
    }
}

