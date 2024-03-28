using System.Collections;
using UnityEngine;

namespace Behaviours
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class ProjectileDamageSource : DamageSource
    {
        [SerializeField] private float m_MaxLifetime = 10;
        [SerializeField] float m_Speed;
        private Rigidbody2D m_Body;

        protected override void Use()
        {
            m_Body = GetComponent<Rigidbody2D>();
            m_Body.gravityScale = 0;
            m_Body.velocity = transform.up * m_Speed;

            StartCoroutine(LifeTime());
        }

        private IEnumerator LifeTime()
        {
            yield return new WaitForSeconds(m_MaxLifetime);
            Destroy(gameObject);
        }
    }
}

