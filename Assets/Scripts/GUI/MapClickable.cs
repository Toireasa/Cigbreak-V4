using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using CigBreak;
using UnityEngine.Events;

namespace CigBreak
{
    public class MapClickable : MonoBehaviour
    {
        private MapCameraController camControl = null;

        public UnityAction OnButtonClicked { get; set; }

        private void Awake()
        {
            camControl = Camera.main.GetComponent<MapCameraController>();
        }

        private void OnMouseUpAsButton()
        {
            // Only accept event if they are not being captured by the gui
            if (EventSystem.current.currentSelectedGameObject == null &&
                !camControl.Scrolling &&
                OnButtonClicked != null)
            {
                OnButtonClicked();
            }
        }
    } 
}
