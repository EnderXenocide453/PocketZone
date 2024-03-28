using System.Collections.Generic;
using Triggers;
using UnityEngine;

namespace Movement
{
    public class EnemyMovementBehaviour : MovementBehaviour
    {
        [SerializeField] TargetTrigger m_SenseTrigger;

        private bool m_IsMove;

        private Dictionary<int, Transform> m_Targets;
        private Transform m_CurrentTarget;

        private Vector2 m_Direction => 
            m_CurrentTarget == null ? 
            Vector2.zero : 
            Vector2.ClampMagnitude(m_CurrentTarget.position - transform.position, 1);

        private void Start()
        {
            m_Targets = new Dictionary<int, Transform>();

            if (m_SenseTrigger == null)
                Debug.LogError("Не назначены органы чувств!");
            else {
                m_SenseTrigger.onTargetEnter += OnTargetSpoted;
                m_SenseTrigger.onTargetExit += OnTargetLost;
            }
        }

        private void FixedUpdate()
        {
            if (m_IsMove) {
                Move(m_Direction);
            }
        }

        protected override void Move(Vector2 direction)
        {
            if (!enabled) return;

            CharacterMovement.MoveByDirection(direction);
        }

        private void OnTargetSpoted(Collider2D collider)
        {
            m_Targets.TryAdd(collider.gameObject.GetInstanceID(), collider.transform);

            if (!m_IsMove)
                m_IsMove = true;

            m_CurrentTarget = collider.transform;
        }

        private void OnTargetLost(Collider2D collider)
        {
            int targetID = collider.gameObject.GetInstanceID();
            m_Targets.Remove(targetID);

            if (m_Targets.Count == 0) {
                m_IsMove = false;
                return;
            }

            if (targetID == m_CurrentTarget.gameObject.GetInstanceID())
                SelectNearestTarget();
        }

        private void SelectNearestTarget()
        {
            float minDistance = float.MaxValue;

            foreach (Transform target in m_Targets.Values) {
                float distance = Vector2.Distance(transform.position, target.position);

                if (distance < minDistance) {
                    minDistance = distance;
                    m_CurrentTarget = target;
                }
            }
        }
    }
}

