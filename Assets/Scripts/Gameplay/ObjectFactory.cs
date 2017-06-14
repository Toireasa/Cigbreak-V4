using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CigBreak
{
    /// <summary>
    /// Object pool and manager for in game InteractableObject instances
    /// </summary>
    public class ObjectFactory : MonoBehaviour
    {
        // Amout of objects to be spawned for each type
        [SerializeField]
        private int unhealthyObjectCount = 10;
        [SerializeField]
        private int healthyObjectCount = 10;
        [SerializeField]
        private int powerupObjectCount = 2;

        // Prefabs of each object type
        [SerializeField]
        private UnhealthyObject unhealthyPrefab = null;
        [SerializeField]
        private HealthyObject healthyPrefab = null;
        [SerializeField]
        private PowerUp powerupPrefab = null;

        // Pools for each type
        private UnhealthyObject[] unhealthyObjects;
        private HealthyObject[] healthyObjects;
        private PowerUp[] powerupObjects;

        // Static reference to current instance
        public static ObjectFactory objectFactory = null;

        private void Awake()
        {
            // Don't allow duplicate factories
            if (objectFactory == null)
            {
                objectFactory = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            // Initalise all pools and spawn objects
            unhealthyObjects = new UnhealthyObject[unhealthyObjectCount];
            for (int i = 0; i < unhealthyObjectCount; i++)
            {
                unhealthyObjects[i] = Instantiate(unhealthyPrefab, transform.position, Quaternion.identity) as UnhealthyObject;
                unhealthyObjects[i].gameObject.SetActive(false);
            }

            healthyObjects = new HealthyObject[healthyObjectCount];
            for (int i = 0; i < healthyObjectCount; i++)
            {
                healthyObjects[i] = Instantiate(healthyPrefab, transform.position, Quaternion.identity) as HealthyObject;
                healthyObjects[i].gameObject.SetActive(false);
            }

            powerupObjects = new PowerUp[powerupObjectCount];
            for (int i = 0; i < powerupObjectCount; i++)
            {
                powerupObjects[i] = Instantiate(powerupPrefab, transform.position, Quaternion.identity) as PowerUp;
                powerupObjects[i].gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// Returns next available object of specified type form the pool
        /// </summary>
        /// <param name="objectType">Type of object</param>
        /// <returns>Next available object in pool</returns>
        public GameObject GetNextObject(GameItemData.ItemType objectType)
        {
            GameObject interObj = null;

            switch (objectType)
            {
                case GameItemData.ItemType.Unhealthy:
                    interObj = unhealthyObjects.Select(o => o.gameObject).FirstOrDefault(g => !g.activeSelf);
                    break;

                case GameItemData.ItemType.Healthy:
                    interObj = healthyObjects.Select(o => o.gameObject).FirstOrDefault(g => !g.activeSelf);
                    break;

                case GameItemData.ItemType.Powerup:
                    interObj = powerupObjects.Select(o => o.gameObject).FirstOrDefault(g => !g.activeSelf);
                    break;
            }

#if UNITY_EDITOR
            if (interObj == null)
            {
                Debug.LogError("Not enough objects in the pool!");
            }
#endif
            if (interObj != null)
            {
                interObj.SetActive(true);
            }
            return interObj;

        }

        /// <summary>
        /// Returns object to the pool
        /// </summary>
        /// <param name="obj">object to return</param>
        public void ReturnObject(GameObject obj)
        {
            InteractableObject io = obj.GetComponent<InteractableObject>();
            io.Reset();
            obj.SetActive(false);
            obj.transform.position = transform.position;
        }

        /// <summary>
        /// Returns all used (active) objects of specified type
        /// </summary>
        /// <param name="type">type of object - must be InteractableObject or subclass</param>
        /// <returns>List of interactable objects matching the specified type</returns>
        public List<InteractableObject> GetActiveObjectsOfType(System.Type type)
        {
            if (type == typeof(UnhealthyObject))
            {
                return unhealthyObjects.Where(o => o.gameObject.activeSelf).Cast<InteractableObject>().ToList();
            }
            else if (type == typeof(HealthyObject))
            {
                return healthyObjects.Where(o => o.gameObject.activeSelf).Cast<InteractableObject>().ToList();
            }
            else if (type == typeof(PowerUp))
            {
                return powerupObjects.Where(o => o.gameObject.activeSelf).Cast<InteractableObject>().ToList();
            }

            return null;
        }

        #region Debug
        public void UpdateForceScale(float newScale)
        {
            foreach (UnhealthyObject uo in unhealthyObjects)
            {
                Rigidbody2D rb = uo.GetComponent<Rigidbody2D>();
                rb.gravityScale = newScale;
            }

            foreach (HealthyObject ho in healthyObjects)
            {
                Rigidbody2D rb = ho.GetComponent<Rigidbody2D>();
                rb.gravityScale = newScale;
            }

            foreach (PowerUp po in powerupObjects)
            {
                Rigidbody2D rb = po.GetComponent<Rigidbody2D>();
                rb.gravityScale = newScale;
            }
        }

        public void UpdateObjectScale(float newScale)
        {
            foreach (UnhealthyObject o in unhealthyObjects)
            {
                o.transform.localScale = new Vector3(newScale, newScale, 1f);
            }

            foreach (HealthyObject o in healthyObjects)
            {
                o.transform.localScale = new Vector3(newScale, newScale, 1f);
            }

            foreach (PowerUp o in powerupObjects)
            {
                o.transform.localScale = new Vector3(newScale, newScale, 1f);
            }
        }
        #endregion
    }
}
