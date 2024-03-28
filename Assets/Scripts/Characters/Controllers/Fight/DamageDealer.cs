using System;
using System.Collections;
using UnityEngine;

namespace Behaviours
{
    public class DamageDealer : MonoBehaviour
    {
        [SerializeField] float m_AttackDelay = 0.5f;
        [SerializeField] GameObject m_DamageSourcePrefab;
        [SerializeField] private string[] m_InteractionTags;

        private bool m_IsReadyToAttack = true;

        public Action onReadyToAttack;

        public void Attack(Vector2 direction)
        {
            if (!m_IsReadyToAttack) return;

            DamageSource damageSource = Instantiate(
                    m_DamageSourcePrefab, 
                    transform.position, 
                    Quaternion.FromToRotation(Vector2.up, direction)
                ).GetComponent<DamageSource>();

            damageSource.SetTags(m_InteractionTags);
            damageSource.Execute();

            StartCoroutine(CoolDown());
        }

        private IEnumerator CoolDown()
        {
            m_IsReadyToAttack = false;
            yield return new WaitForSeconds(m_AttackDelay);
            m_IsReadyToAttack = true;
            onReadyToAttack?.Invoke();
        }
    }
}

