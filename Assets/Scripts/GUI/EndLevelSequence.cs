using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

namespace CigBreak
{
    public class EndLevelSequence : MonoBehaviour
    {
        [SerializeField]
        private Image background = null;
        [SerializeField]
        private float animationTime = 1f;

        private Gameplay gameplay = null;

        void Start()
        {
            gameplay = Gameplay.gameplay;
            gameplay.OnFinishLevel += Play;
        }

        private void Play(UnityAction callback)
        {
            StartCoroutine(AnimateBackground(callback));
        }

        private IEnumerator AnimateBackground(UnityAction callback)
        {
            Color bgCol = background.color;
            float speed = 1f / animationTime;
            float t = 0f;
            while (t < 1f)
            {
                background.color = Color.Lerp(bgCol, Color.black, t);
                t += speed * Time.fixedDeltaTime;
                yield return null;
            }

            if(callback != null)
            {
                callback();
            }
        }
    }
}
