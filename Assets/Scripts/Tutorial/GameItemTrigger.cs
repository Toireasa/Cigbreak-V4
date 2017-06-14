using UnityEngine;
using System.Collections;
using System.Linq;
using CigBreakGUI;

namespace CigBreak
{
    public class GameItemTrigger : MonoBehaviour
    {
        [SerializeField]
        private GameObject finger = null;

        [SerializeField]
        private GameItemData.ItemType[] stoppedObjects = null;

        [SerializeField]
        private bool multipleUse = false;
        [SerializeField]
        private bool showFinger = true;

        [SerializeField]
        private PriceTags pricetag= null;

        private void Awake()
        {
            finger.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.tag == CigBreakConstants.Tags.InteractableObject)
            {
                InteractableObject obj = col.GetComponent<InteractableObject>();
                if (stoppedObjects.Contains(obj.ItemData.TypeOfItem))
                {
                    switch(obj.ItemData.TypeOfItem)
                    {
                        case GameItemData.ItemType.Unhealthy:
                            if(obj.CurrentHealth > 0)
                            {
                                OnUnhealthy(obj);
                            }
                            break;

                        case GameItemData.ItemType.Powerup:
                            OnPowerup(obj);
                            break;
                    }
                }
            }
        }

        private void OnPowerup(InteractableObject obj)
        {
            obj.TogglePause(true);
            (obj as PowerUp).setpricetag(pricetag);
            (obj as PowerUp).OnTap();
                       
            if (!multipleUse)
            {
                obj.OnObjectDestroyed += Deactivate;
                obj.OnObjectCollected += Deactivate;
            }
        }

        public void powerUpDone()
        {
            pricetag.unblock();
        }

        private void OnUnhealthy(InteractableObject obj)
        {
            if (showFinger)
            {
                finger.SetActive(true);
            }
            obj.TogglePause(true);
            if (!multipleUse)
            {
                obj.OnObjectDestroyed += Deactivate;
                obj.OnObjectCollected += Deactivate;
            }
        }

        private void Deactivate(InteractableObject io)
        {
            io.OnObjectDestroyed -= Deactivate;
            io.OnObjectCollected -= Deactivate;
            io.TogglePause(false);
            finger.SetActive(false);
            gameObject.SetActive(false);
        }

        public void Deactivate()
        {
            finger.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
