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

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<LootCollector>(out var collector)) {
                collector.AddLoot(m_Info);
                Destroy(gameObject);
            }
        }

        public void SetItem(LootInfo lootInfo)
        {
            m_Info = lootInfo;
            m_SpriteRenderer.sprite = lootInfo.Sprite;
        }
    }
}

