using CharacterStats;
using System;
using UnityEngine;
using Zenject;

namespace Loot
{
    [RequireComponent(typeof(Health))]
    public class DeathLoot : MonoBehaviour
    {
        private Health m_Health;
        private LootFabric m_Fabric;

        [Inject]
        public void Construct(LootFabric lootFabric)
        {
            m_Fabric = lootFabric;
        }

        private void Start()
        {
            m_Health = GetComponent<Health>();
            m_Health.onDeath += DropLoot;
        }

        private void DropLoot()
        {
            Debug.Log("LOOT");
            m_Fabric.InstantiateRandomLootObject(transform.position);
        }
    }
}

