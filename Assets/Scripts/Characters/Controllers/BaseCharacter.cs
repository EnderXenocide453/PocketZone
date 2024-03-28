using CharacterStats;
using Movement;
using UnityEngine;

namespace Behaviours
{
    public class BaseCharacter : MonoBehaviour
    {
        [SerializeField] private Health m_Health;
        [SerializeField] private TargetDetector m_TargetDetector;
        [SerializeField] private MovementBehaviour m_MovementBehaviour;
        [SerializeField] private BaseAttackBehaviour m_BaseAttackBehaviour;

        private void Awake()
        {
            if (m_Health != null)
                m_Health.onDeath += OnDeath;

            m_TargetDetector.onCurrentTargetChanged += OnTargetChanged;
        }

        protected void OnTargetChanged(Transform target)
        {
            m_MovementBehaviour.SetTarget(target);
            m_BaseAttackBehaviour.SetTarget(target);
        }

        protected void OnDeath()
        {
            //m_MovementBehaviour.enabled = false;
            //m_TargetDetector.enabled = false;
            //m_BaseAttackBehaviour.enabled = false;

            //foreach (var collider in GetComponents<Collider>())
            //    collider.enabled = false;

            Destroy(gameObject);
        }
    }
}

