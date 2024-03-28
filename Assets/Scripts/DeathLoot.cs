using CharacterStats;
using System;
using UnityEngine;

namespace Loot
{
    [RequireComponent(typeof(Health))]
    public class DeathLoot : MonoBehaviour
    {
        private Health m_Health;

        private void Start()
        {
            m_Health = GetComponent<Health>();
            m_Health.onDeath += DropLoot;
        }

        private void DropLoot()
        {
            Debug.Log("LOOT");
        }
    }
}

