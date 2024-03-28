using CharacterStats;
using System.Collections;
using UnityEngine;

namespace Behaviours
{
    public abstract class DamageSource : MonoBehaviour
    {
        [SerializeField] float m_Damage = 10f;
        [SerializeField] private string[] m_InteractionTags;
        [SerializeField] bool m_DestroyAfterCollision = true;
        [SerializeField] bool m_DestroyOnNextFrame = false;

        public void Execute()
        {
            if (m_DestroyOnNextFrame)
                StartCoroutine(DestroyOnNextFrame());

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

        private IEnumerator DestroyOnNextFrame()
        {
            yield return null;
            Destroy(gameObject);
        }
    }
}

