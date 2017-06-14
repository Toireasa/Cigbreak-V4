﻿using UnityEngine;
using System.Collections;

namespace CigBreak
{
    /// <summary>
    /// Healthy objects (vegetables)
    /// </summary>
    public class HealthyObject : InteractableObject
    {

        public override void OnSwipe()
        {
            // If the object is not yet destroyed continue to reduce its health
            if (currentHealth > 0)
            {
                //Play sound
                if (itemData.SwipeSounds.Length > 0)
                {
                    int audioIndex = Mathf.Min(itemData.Health - currentHealth, itemData.SwipeSounds.Length - 1);
                    audioSource.PlayOneShot(itemData.SwipeSounds[audioIndex]);
                }
                else
                {
                    Debug.LogErrorFormat("Object {0} has no swipe audio assigned", itemData.name);
                }

                // Reduce health and update visuals
                currentHealth = Mathf.Max(currentHealth - 1, 0);
                UpdateMaterial();

                // If the object just changed to 0 health emit destroyed event
                if (currentHealth == 0 && OnObjectDestroyed != null)
                {
                    OnObjectDestroyed(this);
                }
            }
        }

        public override void OnTap()
        {
            // No action taken on tap
        }
    }

}