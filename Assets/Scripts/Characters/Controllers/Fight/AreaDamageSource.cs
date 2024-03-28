using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Behaviours
{
    public class AreaDamageSource : DamageSource
    {
        protected override void Use()
        {
            StartCoroutine(DestroyOnNextFixedUpdate());
        }

        private IEnumerator DestroyOnNextFixedUpdate()
        {
            yield return new WaitForFixedUpdate();
            Destroy(gameObject);
        }
    }
}

