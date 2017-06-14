using UnityEngine;
using System.Collections;
using CigBreak;
using UnityEngine.UI;

namespace CigBreakGUI
{
    public class MapPopup : MonoBehaviour
    {
        private MapClickable eventOrigin = null;
        private bool visible = false;

        [SerializeField]
        private GameObject content = null;

        [SerializeField]
        private Text text = null;
        public Text PopupText 
        { 
            get { return text; } 
        }

        [SerializeField]
        private Button dismissButton = null;

       // private MapCameraController camControl = null;

        private void Awake()
        {
            //camControl = Camera.main.GetComponent<MapCameraController>();
            eventOrigin = GetComponentInParent<MapClickable>();

#if UNITY_EDITOR
            if (eventOrigin == null)
            {
                Debug.LogError("MapCliclable for a Map popup not found: " + name);
            }
#endif
            eventOrigin.OnButtonClicked += Toggle;
            dismissButton.onClick.AddListener(Toggle);

        }

        private void Toggle()
        {
            visible = !visible;
            content.SetActive(visible);

            //if(visible)
            //{
            //    camControl.SetViewTarget(gameObject);
            //}
            //else 
            //{
            //    camControl.SetInitViewTarget();
            //}
        }
    }
}
