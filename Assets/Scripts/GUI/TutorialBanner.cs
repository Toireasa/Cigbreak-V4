using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using CigBreak;

namespace CigBreakGUI
{
    public class TutorialBanner : MonoBehaviour
    {
        [SerializeField]
        private Image image = null;
        [SerializeField]
        private Image background = null;
        //[SerializeField]
        //private Text text = null;
        [SerializeField]
        private AnimationCurve scaleAnimation = null;

        [SerializeField]
        private float animationTime = 2f;

        private GameObject spawnedElement = null;

        private UnityAction onDismissedElement { get; set; }

        private Gameplay gameplay = null;

        private void Start()
        {
            gameplay = Gameplay.gameplay;
            gameplay.OnTutorialInstruction += Display;
            gameplay.OnEndTutorialStep += Hide;
            gameplay.OnEndStep += Hide;


            gameplay.OnFailTutorialInstruction += SpawnElement;
            gameplay.OnHideFailTutorial += ClearElement;
        }

        public void Display(Sprite sprite)
        {
            if (sprite!=null)
            {
                image.gameObject.SetActive(true);
                image.sprite = sprite;
                StartCoroutine(Animate(0f, 0.5f));
            }
        }

        public void Hide(UnityAction callback)
        {
            StartCoroutine(HideAnim(callback));
        }

        private IEnumerator HideAnim(UnityAction callback)
        {
            yield return StartCoroutine(Animate(0.5f, 1f));
            image.gameObject.SetActive(false);

            if (callback != null)
            {
                callback();
            }
        }

        private IEnumerator Animate(float start, float end)
        {
            float step = 1f / animationTime;
            float t = start;
            while (t <= end)
            {
                image.transform.localScale = new Vector3(1f, 1f) * scaleAnimation.Evaluate(t);
                t += step * Time.deltaTime;
                yield return null;
            }
        }

        private void SpawnElement(GameObject obj, UnityAction callback)
        {
            if(spawnedElement != null)
            {
                ClearElement();
            }

            background.enabled = true;

            spawnedElement = Instantiate(obj) as GameObject;
            spawnedElement.transform.SetParent(gameObject.transform, false);
            onDismissedElement += callback;
            spawnedElement.GetComponentInChildren<Button>().onClick.AddListener(DismissElement);
        }

        private void ClearElement()
        {
            Destroy(spawnedElement);
            spawnedElement = null;
            onDismissedElement = null;
            background.enabled = false;
        }

        private void DismissElement()
        {
            if(onDismissedElement != null)
            {
                onDismissedElement();
            }

            ClearElement();
        }
    }
}
