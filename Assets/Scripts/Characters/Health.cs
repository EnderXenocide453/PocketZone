using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace CharacterStats
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float m_MaxHealth = 100;
        [SerializeField] NumValueDisplay m_HealthDisplay;
        private float m_CurrentHealth;

        public Action onDeath;

        private void Start()
        {
            m_CurrentHealth = m_MaxHealth;
        }

        public void TakeDamage(float damage)
        {
            SetHP(m_CurrentHealth -  damage);
        }

        private void SetHP(float health)
        {
            if (health <= 0) {
                health = 0;
                Death();
            }

            m_CurrentHealth = health;
            m_HealthDisplay?.DisplayValue(m_CurrentHealth, m_MaxHealth);
        }

        private void Death()
        {
            onDeath?.Invoke();
        }
    }
}

