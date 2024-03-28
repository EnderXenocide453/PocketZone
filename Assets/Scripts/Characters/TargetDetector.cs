using System;
using System.Collections;
using System.Collections.Generic;
using Triggers;
using UnityEngine;

namespace Behaviours
{
    public class TargetDetector : MonoBehaviour
    {
        [SerializeField] TargetTrigger m_Trigger;
        [SerializeField] float m_NearTargetCheckDelay = 1f;

        private Dictionary<int, Transform> m_Targets;
        private Transform m_CurrentTarget;

        public Dictionary<int, Transform> Targets => m_Targets;
        public Transform CurrentTarget
        {
            get => m_CurrentTarget;
            set
            {
                onCurrentTargetChanged?.Invoke(value);
                m_CurrentTarget = value;
            }
        }

        public event Action<Transform> onCurrentTargetChanged;

        private void Start()
        {
            m_Targets = new Dictionary<int, Transform>();

            if (m_Trigger == null)
                Debug.LogError("Не назначены органы чувств!");
            else {
                m_Trigger.onTargetEnter += OnTargetSpoted;
                m_Trigger.onTargetExit += OnTargetLost;
            }
        }

        private void OnTargetSpoted(Collider2D collider)
        {
            bool isFirst = m_Targets.Count == 0;
            m_Targets.TryAdd(collider.gameObject.GetInstanceID(), collider.transform);

            if (isFirst) {
                StartCoroutine(CheckNearestTarget());
            }
        }

        private void OnTargetLost(Collider2D collider)
        {
            int targetID = collider.gameObject.GetInstanceID();
            m_Targets.Remove(targetID);

            if (m_Targets.Count == 0) {
                StopAllCoroutines();
                CurrentTarget = null;
                return;
            }

            if (ReferenceEquals(collider.transform, CurrentTarget))
                SelectNearestTarget();
        }

        private void SelectNearestTarget()
        {
            Transform nearestTarget = CurrentTarget;
            float minDistance = float.MaxValue;

            if (nearestTarget != null)
                minDistance = Vector2.Distance(transform.position, nearestTarget.position);

            foreach (Transform target in m_Targets.Values) {
                float distance = Vector2.Distance(transform.position, target.position);

                if (distance < minDistance) {
                    minDistance = distance;
                    nearestTarget = target;
                }
            }

            if (!ReferenceEquals(nearestTarget, CurrentTarget)) {
                CurrentTarget = nearestTarget;
            }
        }

        private IEnumerator CheckNearestTarget()
        {
            while (true) {
                SelectNearestTarget();
                yield return new WaitForSeconds(m_NearTargetCheckDelay);
            }
        }
    }
}

