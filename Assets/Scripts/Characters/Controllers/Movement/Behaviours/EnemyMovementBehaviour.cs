using UnityEngine;

namespace Movement
{
    public class EnemyMovementBehaviour : MovementBehaviour
    {
        [SerializeField] float m_StopDistance = 0.5f;
        private bool m_IsMove;

        private Vector2 m_Direction
        {
            get
            {
                if (CurrentTarget == null || Vector2.Distance(CurrentTarget.position, transform.position) <= m_StopDistance)
                    return Vector2.zero;

                return Vector2.ClampMagnitude(CurrentTarget.position - transform.position, 1);
            }
        }

        private void FixedUpdate()
        {
            if (m_IsMove) {
                Move(m_Direction);
            }
        }

        public override void SetTarget(Transform target)
        {
            base.SetTarget(target);

            m_IsMove = target;
        }

        protected override void Move(Vector2 direction)
        {
            if (!enabled) return;

            CharacterMovement.MoveByDirection(direction);
        }
    }
}

