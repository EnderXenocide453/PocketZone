using Loot;
using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Behaviours
{
    public class DamageDealer : MonoBehaviour
    {
        [SerializeField] float m_AttackDelay = 0.5f;
        [SerializeField] GameObject m_DamageSourcePrefab;
        [SerializeField] private string[] m_InteractionTags;
        [SerializeField] private bool m_enableConsumption = false;

        private bool m_IsReadyToAttack = true;
        private Inventory m_Inventory;

        private (int id, int count) m_ConsumptionParameters;

        public Action onReadyToAttack;

        [Inject]
        public void Construct(Inventory inventory)
        {
            m_Inventory = inventory;

            var source = m_DamageSourcePrefab.GetComponent<DamageSource>();
            m_ConsumptionParameters = source.ConsumptionParameters;
        }

        public void Attack(Vector2 direction)
        {
            if (!m_IsReadyToAttack) return;

            if (m_enableConsumption && m_ConsumptionParameters.count > 0)
                if (!m_Inventory.TakeLootByID(m_ConsumptionParameters.id, m_ConsumptionParameters.count))
                    return;

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

