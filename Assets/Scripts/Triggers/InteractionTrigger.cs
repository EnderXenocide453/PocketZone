using UnityEngine;

namespace Triggers
{
    public abstract class InteractionTrigger : MonoBehaviour
    {
        [SerializeField] private string[] m_InteractionTags;

        protected abstract void OnEnter(Collider2D collision);
        protected abstract void OnExit(Collider2D collision);

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!CheckTag(collision.tag))
                return;

            OnEnter(collision);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!CheckTag(collision.tag))
                return;

            OnExit(collision);
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

