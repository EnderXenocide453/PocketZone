using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BarDisplay : NumValueDisplay
    {
        [SerializeField] private Image m_Bar;

        private void Start()
        {
            m_Bar ??= GetComponent<Image>();
            m_Bar.type = Image.Type.Filled;
        }

        public override void DisplayValue(float currentValue, float maxValue)
        {
            m_Bar.fillAmount = currentValue / maxValue;
        }
    }
}

