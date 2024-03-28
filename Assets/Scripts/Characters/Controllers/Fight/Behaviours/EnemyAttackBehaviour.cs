using UnityEngine;

namespace Behaviours
{
    public class EnemyAttackBehaviour : BaseAttackBehaviour
    {
        [SerializeField] private float m_AttackDistance = 1.5f;

        bool m_AlreadyAttack = false;

        private void FixedUpdate()
        {
            if (!CurrentTarget) return;

            if (Vector2.Distance(CurrentTarget.position, transform.position) < m_AttackDistance) {
                if (m_AlreadyAttack) return;

                CharacterAttack.SetAttackPossibility(true);
                CharacterAttack.TryAttack();
            }
            else if (m_AlreadyAttack) {
                CharacterAttack.SetAttackPossibility(false);
            }
        }
    }
}

