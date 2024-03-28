using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public abstract class NumValueDisplay : MonoBehaviour
    {
        public abstract void DisplayValue(float currentValue, float maxValue);
    }
}

