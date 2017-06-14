using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace CigBreak
{
    /// <summary>
    /// Works on data from InptController and triggers InteractableObjects with input from player
    /// </summary>
    [RequireComponent(typeof(CircleCollider2D), typeof(Rigidbody2D))]
    public class InputMessanger : MonoBehaviour
    {
        // Component and object references
        private CircleCollider2D cCollider = null;
        private InteractableObject touchedObject = null;

        // Reference to input qualifier function
        private System.Func<InputController.InputType> inputTypeQualifier = null;

        private void Start()
        {
            cCollider = GetComponent<CircleCollider2D>();
        }

        public void Initialise(System.Func<InputController.InputType> inputTypeQualifier)
        {
            this.inputTypeQualifier = inputTypeQualifier;
        }

        public void Enable()
        {
            cCollider.enabled = true;
        }

        public void Disable()
        {
            if (touchedObject != null)
            {
                ProcessInput(touchedObject);
            }
            touchedObject = null;
            cCollider.enabled = false;
        }

        public void SetPosition(Vector3 pos)
        {
            transform.position = new Vector3(pos.x, pos.y, CigBreakConstants.Values.SceneDistance);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.tag == CigBreakConstants.Tags.InteractableObject)
            {
                touchedObject = col.GetComponent<InteractableObject>();
            }
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            touchedObject = null;

            if (col.tag == CigBreakConstants.Tags.InteractableObject)
            {
                InteractableObject obj = col.GetComponent<InteractableObject>();
                ProcessInput(obj);
            }
        }

        private void ProcessInput(InteractableObject obj)
        {
            // Qualify input and call the corresponding event on the object involved
            InputController.InputType inputType = inputTypeQualifier();

            switch (inputType)
            {
                case InputController.InputType.Swipe:
                    obj.OnSwipe();
                    break;
                case InputController.InputType.Tap:
                    obj.OnTap();
                    break;
            }
        }
    }
}
