using CharacterStats;
using Movement;
using UnityEngine;

namespace Characters
{
    public class BaseCharacter : MonoBehaviour
    {
        [SerializeField] private Health m_Health;
        [SerializeField] private MovementBehaviour m_MovementBehaviour;

        private void Awake()
        {
            if (m_Health != null)
                m_Health.onDeath += OnDeath;
        }

        private void OnDeath()
        {
            m_MovementBehaviour.enabled = false;
        }
    }
}

