using Installers;
using Loot;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Spawners {
    public class LootSpawner : MonoBehaviour
    {
        [SerializeField] private Item m_Item;
        [SerializeField] private int m_MinCount = 1, m_MaxCount = 10;
        [SerializeField] private float m_Delay = 5f;
        [SerializeField] private Vector2 m_MinAreaCorner, m_MaxAreaCorner;
        [SerializeField] private Color m_AreaColor = Color.red;

        private LootFabric m_LootFabric;

        [Inject]
        public void Construct(LootFabric lootFabric)
        {
            m_LootFabric = lootFabric;

            Spawn();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = m_AreaColor;
            Gizmos.DrawLine(m_MinAreaCorner, new Vector3(m_MinAreaCorner.x, m_MaxAreaCorner.y));
            Gizmos.DrawLine(new Vector3(m_MinAreaCorner.x, m_MaxAreaCorner.y), m_MaxAreaCorner);
            Gizmos.DrawLine(m_MaxAreaCorner, new Vector3(m_MaxAreaCorner.x, m_MinAreaCorner.y));
            Gizmos.DrawLine(new Vector3(m_MaxAreaCorner.x, m_MinAreaCorner.y), m_MinAreaCorner);
        }

        public void Spawn()
        {
            StartCoroutine(SpawnWithDelay());
        }

        public void SpawnAtRandomPoint()
        {
            Vector2 point = GetRandomPosition();

            m_LootFabric.InstantiateLootObject(m_Item.Index, point, Random.Range(m_MinCount, m_MaxCount + 1));
        }

        private Vector2 GetRandomPosition()
        {
            return new Vector2(
                Random.Range(m_MinAreaCorner.x, m_MaxAreaCorner.x),
                Random.Range(m_MinAreaCorner.y, m_MaxAreaCorner.y));
        }

        private IEnumerator SpawnWithDelay()
        {
            while (true) {
                yield return new WaitForSeconds(m_Delay);
                SpawnAtRandomPoint();
            }
        }
    }
}

