using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace CigBreak
{
    /// <summary>
    /// Area under the game screen that detects falling objects
    /// </summary>
    public class ClearZone : MonoBehaviour
    {
        private ObjectFactory factory = null;

        private void Start()
        {
            factory = ObjectFactory.objectFactory;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {   
            // If interectable object collides with the area emit the OutOfScreen event and return object to object pool
            if (col.tag == CigBreakConstants.Tags.InteractableObject)
            {
                    InteractableObject io = col.GetComponent<InteractableObject>();
                    io.OutOfScreen();

                factory.ReturnObject(col.gameObject);
            }
        }
    }
}
