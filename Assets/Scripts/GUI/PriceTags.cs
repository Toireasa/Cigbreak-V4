using UnityEngine;
using System.Collections;
using CigBreak;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

namespace CigBreakGUI
{
    public class PriceTags : MonoBehaviour
    {

        [SerializeField]
        private GameObject m_PriceTag = null;

        [SerializeField]
        private int factoryInstances = 20;
        [SerializeField]
        private float maxScale = 0.5f;
        [SerializeField]
        private float animationTime = 1f;
        [SerializeField]
        private AnimationCurve animationCurve = null;

        private List<GameObject> m_PriceTagInstances = null;

        private Gameplay gameplay = null;

        void Start()
        {
            gameplay = Gameplay.gameplay;
            gameplay.OnInteractableObjectDestroyed += OnObjectDestroyed;
            InitialiseInstances();
        }

        private void InitialiseInstances()
        {
            m_PriceTagInstances = new List<GameObject>(factoryInstances);

            for (int i = 0; i < factoryInstances; i++)
            {
                GameObject positive1 = Instantiate(m_PriceTag) as GameObject;
                positive1.transform.SetParent(transform, false);
                positive1.transform.localScale = Vector3.zero;
                positive1.SetActive(false);

                m_PriceTagInstances.Add(positive1);
            }
        }

        private void OnObjectDestroyed(InteractableObject obj)
        {
            if (!blocktags)
            {

                if (obj.ItemData.TypeOfItem == GameItemData.ItemType.Unhealthy)
                {
                    GameObject label = null;

                    string currency;

                    if (PlayerProfile.GetProfile().Contry == Country.UK)
                    {
                        currency = "£";
                    }
                    else
                    {
                        currency = "$";
                    }                    

                    label = GetNextPositive();
                    label.GetComponentInChildren<TextMesh>().text = currency + obj.ItemData.Price.ToString("F2");
					/*
					label.GetComponentInChildren<TextMesh> ().offsetZ = 500;
					label.GetComponentInChildren<MeshRenderer> ().sortingLayerName = "Default";
					label.GetComponentInChildren<MeshRenderer> ().sortingOrder = 0;
					*/
					//Debug.Log(label.GetComponent<Renderer>().sortingLayerName);
					//Debug.Log(label.GetComponentInChildren<TextMesh>().text);
                    ShowLabel(obj, label);
                }

            }
        }

        private void ShowLabel(InteractableObject obj, GameObject label)
        {
            if (label != null)
            {
                label.transform.position = obj.transform.position;
                StartCoroutine(AnimateLabel(label));
            }
        }

        private IEnumerator AnimateLabel(GameObject label)
        {
            Vector3 startScale = label.transform.localScale;
            Vector3 endScale = new Vector3(maxScale, maxScale);
            float speed = maxScale / animationTime;
            float t = 0f;

            while(t <= 1f)
            {
                label.transform.localScale = Vector3.Lerp(startScale, endScale, animationCurve.Evaluate(t));
                t += speed * Time.deltaTime;
                yield return null;
            }
            label.transform.localScale = startScale;
            label.SetActive(false);
        }

        private GameObject GetNextPositive()
        {
            GameObject label = m_PriceTagInstances.FirstOrDefault(g => !g.activeSelf);

            if (label != null)
            {
                label.SetActive(true);
            }

            return label;
        }

        private bool blocktags = false;

        public void test(InteractableObject obj,float money)
        {
            GameObject label = null;
            label = GetNextPositive();

            string currency;

            if (PlayerProfile.GetProfile().Contry == Country.UK)
            {
                currency = "£";
            }
            else
            {
                currency = "$";
            }

            label.GetComponentInChildren<TextMesh>().text = currency + money.ToString("F2");
            
            ShowLabel(obj, label);
            blocktags = true;

         }

        public void unblock()
        {
            blocktags = false;
        }
    } 
}
