using CharacterStats;
using Installers;
using UnityEngine;
using Zenject;

namespace Spawners
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject m_Enemy;
        [SerializeField] private int m_CountPerWave = 3;
        [SerializeField] private Vector2 m_MinAreaCorner, m_MaxAreaCorner;
        [SerializeField] private Color m_AreaColor = Color.red;

        private GameplaySceneInstaller m_Installer;
        private int m_CurrentCount;

        [Inject]
        public void Construct(GameplaySceneInstaller installer)
        {
            m_Installer = installer;

            SpawnEnemies();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = m_AreaColor;
            Gizmos.DrawLine(m_MinAreaCorner, new Vector3(m_MinAreaCorner.x, m_MaxAreaCorner.y));
            Gizmos.DrawLine(new Vector3(m_MinAreaCorner.x, m_MaxAreaCorner.y), m_MaxAreaCorner);
            Gizmos.DrawLine(m_MaxAreaCorner, new Vector3(m_MaxAreaCorner.x, m_MinAreaCorner.y));
            Gizmos.DrawLine(new Vector3(m_MaxAreaCorner.x, m_MinAreaCorner.y), m_MinAreaCorner);
        }

        private void SpawnEnemies()
        {
            for (int i = 0; i < m_CountPerWave; i++)
                SpawnEnemyAtRandomPoint();
        }

        private void SpawnEnemyAtRandomPoint()
        {
            Vector2 point = GetRandomPosition();

            m_Installer.Instantiate<Health>(m_Enemy, point, Quaternion.identity, null).onDeath += OnEnemyIsDead;
            m_CurrentCount++;
        }

        private Vector2 GetRandomPosition()
        {
            return new Vector2(
                Random.Range(m_MinAreaCorner.x, m_MaxAreaCorner.x), 
                Random.Range(m_MinAreaCorner.y, m_MaxAreaCorner.y));
        }

        private void OnEnemyIsDead()
        {
            if (--m_CurrentCount == 0)
                SpawnEnemies();
        }
    }
}

