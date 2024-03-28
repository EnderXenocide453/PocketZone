using System;
using UnityEngine;

namespace Behaviours
{
    public class CharacterAttack : MonoBehaviour
    {
        [SerializeField] private DamageDealer m_DamageDealer;
        private bool m_CanAttack;
        private Transform m_Target;

        public Action onReadyToAttack;

        private void Start()
        {
            m_DamageDealer.onReadyToAttack += TryAttack;
        }

        public void SetTarget(Transform target)
        {
            m_Target = target;
        }

        public void SetAttackPossibility(bool canAttack)
        {
            m_CanAttack = canAttack;
        }

        public void TryAttack()
        {
            if (!m_CanAttack || m_Target == null)
                return;

            m_DamageDealer.Attack((m_Target.position - transform.position).normalized);
        }
    }
}

