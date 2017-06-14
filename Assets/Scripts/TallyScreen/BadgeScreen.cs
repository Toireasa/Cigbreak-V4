using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using CigBreak;
using UnityEngine.Events;

namespace CigBreakGUI
{
    public class BadgeScreen : MonoBehaviour
    {
        [SerializeField]
        private Image iconOverlay = null;
        [SerializeField]
        private Text badgeDescription = null;

        [SerializeField]
        private float animationTime = 2f;
        
        public void Display(Badge badge, bool forceMapExit)
        {
            gameObject.SetActive(true);

            StartCoroutine(LowerOverlay());

            badgeDescription.text = badge.Description;           
        }

        private IEnumerator FadeOutOverlay()
        {
            float speed = 1f / animationTime;
            Color current = iconOverlay.color;
            while(current.a > 0)
            {
                current.a -= speed * Time.deltaTime;
                iconOverlay.color = current;
                yield return null;
            }
        }

        private IEnumerator LowerOverlay()
        {
            float speed = 1f / animationTime;
            while(iconOverlay.fillAmount > 0)
            {
                iconOverlay.fillAmount -= speed * Time.deltaTime;
                yield return null;
            }
            iconOverlay.fillAmount = 1;
            StartCoroutine(LowerOverlay());
        }

    } 
}
