using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using CigBreak;

namespace CigBreakGUI
{
    public class HealthDisplay : MonoBehaviour
    {
        [SerializeField]
        private Image lungsSmoke = null;
        [SerializeField]
        private Image screenSmoke = null;
        [SerializeField]
        private float fillStep = 0.1f;

        private Gameplay gameplay = null;
        private int maxHealth;

        private float currentSmokeFill = 0f;
        private float targetSmokeFill = 0f;

        void Start()
        {
            gameplay = Gameplay.gameplay;
            maxHealth = gameplay.PlayerMaxHealth;
            gameplay.OnHealthChanged += UpdateHealth;
        }

        private void UpdateHealth(int currentHealth)
        {
            targetSmokeFill =  (float)(maxHealth - currentHealth) / maxHealth;
        }

        private void Update()
        {
            if(Mathf.Abs(currentSmokeFill - targetSmokeFill) > fillStep)
            {
                // Decide if smoke level should fall or rise
                float sign = 0;
                if(currentSmokeFill > targetSmokeFill)
                {
                    sign = -1;
                }
                else
                {
                    sign = 1;
                }

                currentSmokeFill += fillStep * sign;
                lungsSmoke.fillAmount = currentSmokeFill;
                screenSmoke.color = new Color(1f, 1f, 1f, currentSmokeFill);
            }
        }
    } 
}
