using CharacterStats;
using Loot;
using System.Collections;
using UnityEngine;

namespace Behaviours
{
    public abstract class DamageSource : MonoBehaviour
    {
        [SerializeField] private float m_Damage = 10f;
        [SerializeField] private string[] m_InteractionTags;
        [SerializeField] private bool m_DestroyAfterCollision = true;
        [SerializeField] private Item m_ConsumableItem;
        [SerializeField] private int m_ConsumptionCost = 1;

        public (int id, int cost) ConsumptionParameters => m_ConsumableItem ? (m_ConsumableItem.Index, m_ConsumptionCost) : (0, 0);

        public void Execute()
        {
            Use();
        }

        public void SetTags(string[] tags)
        {
            m_InteractionTags = tags;
        }

        protected abstract void Use();

        private void DealDamage(Health health)
        {
            health.TakeDamage(m_Damage);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!CheckTag(collision.tag) || !collision.TryGetComponent<Health>(out var health))
                return;

            DealDamage(health);
            if (m_DestroyAfterCollision)
                Destroy(gameObject);
        }

        private bool CheckTag(string targetTag)
        {
            foreach (var tag in m_InteractionTags) {
                if (tag.Equals(targetTag))
                    return true;
            }

            return false;
        }
    }
}

