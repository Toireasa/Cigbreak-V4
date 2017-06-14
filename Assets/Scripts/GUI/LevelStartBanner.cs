using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using CigBreak;

namespace CigBreakGUI
{
    public class LevelStartBanner : MonoBehaviour
    {
        [SerializeField]
        private GameObject image = null;

        [SerializeField]
        private Text levelNumber = null;

        [SerializeField]
        private AnimationCurve scaleAnimation = null;

        [SerializeField]
        private float animationTime = 2f;

        private Gameplay gameplay = null;

        private void Start()
        {
            gameplay = Gameplay.gameplay;
            gameplay.OnPrepareLevel += Display;
        }

        public void Display(int level, UnityAction callback)
        {
            image.SetActive(true);
            levelNumber.text = level.ToString();
            StartCoroutine(AnimateBanner(callback));
        }

        private IEnumerator AnimateBanner(UnityAction callback)
        {
            float step = 1f / animationTime;
            float t = 0;
            while(t <= 1f)
            {
                image.transform.localScale = new Vector3(1f, 1f) * scaleAnimation.Evaluate(t);
                t += step * Time.deltaTime;
                yield return null;
            }

            image.SetActive(false);

            if(callback != null)
            {
                callback();
            }
        }
    } 
}
