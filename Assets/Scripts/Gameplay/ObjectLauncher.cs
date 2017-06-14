using UnityEngine;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;

namespace CigBreak
{
    /// <summary>
    /// Launches new objects onto the screen during the game
    /// </summary>
    public class ObjectLauncher : MonoBehaviour
    {
        // Customisable values
        [SerializeField]
        private float spread = 2f;

        [SerializeField]
        [Range(1, 10)]
        private int maxChain = 3;

        // Object references
        Gameplay gameplay = null;
        ObjectFactory factory = null;

        // Objects that are inserted into the queue by additonal events
        private List<QueuedObject> addedObjects = null;

        public bool Paused { get; set; }

        private void Start()
        {
            // Cache references
            gameplay = Gameplay.gameplay;
            factory = ObjectFactory.objectFactory;

            addedObjects = new List<QueuedObject>();
        }

        public void StartLevel()
        {
            // Start launch loop based on the level data
            foreach (var occurence in gameplay.CurrentLevel.SpawnTimers.SpawnTimers)
            {
                StartCoroutine(LaunchObjects(occurence.FirstSpawnDelay, occurence.TimeBeforeRepeating, gameplay.CurrentLevel.LevelObjects));
            }
        }

        public void EndLevel()
        {
            StopAllCoroutines();
        }

        /// <summary>
        /// Launches a single object immidiately using the supplied data item
        /// </summary>
        /// <param name="itemData">Data object to be used</param>
        public void LaunchSingleObject(GameItemData itemData)
        {
            transform.rotation = Quaternion.Euler(Vector3.zero);
            LaunchObject(itemData);
        }

        /// <summary>
        /// Launches multiple objects of specified types
        /// </summary>
        /// <param name="count">total number of itmes to spawn</param>
        /// <param name="items">list of typed to use</param>
        public void LaunchMultiple(int count, List<LevelData.LevelInteractableObjects> items)
        {
            transform.rotation = Quaternion.Euler(Vector3.zero);
            float chancesum = items.Sum(i => i.SpawnChance);
            for(int i = 0; i < count; i++)
            {
                LaunchBaseObject(items.AsReadOnly(), chancesum);
            }
        }

        /// <summary>
        /// Inset an additional object into the queue, which will be spawned based on the chance supplies
        /// </summary>
        /// <param name="itemData">Data for the object to spawn</param>
        /// <param name="repeat">Should the object remain in the queue</param>
        /// <param name="chance">Chance the item will appear on each launch event</param>
        public void InsertObjectIntoQueue(GameItemData itemData, bool repeat, float chance)
        {
            addedObjects.Add(new QueuedObject() { ItemData = itemData, Repeat = repeat, Chance = chance });
        }

        public List<InteractableObject> GetActiveObjectsOfType(System.Type type)
        {
            return factory.GetActiveObjectsOfType(type);
        }

        /// <summary>
        /// Repetatly launch object based on the supplied data list, delay and interval
        /// </summary>
        /// <param name="delay"></param>
        /// <param name="interval"></param>
        /// <param name="itemData"></param>
        /// <returns></returns>
        private IEnumerator LaunchObjects(float delay, float interval, ReadOnlyCollection<LevelData.LevelInteractableObjects> itemData)
        {
            float baseChanceSum = itemData.Sum(i => i.SpawnChance);

            int chainLength = 0;
            GameItemData lastLaunched = null;

            yield return new WaitForSeconds(delay);
            while (true)
            {
                if (!gameplay.Paused && !Paused)
                {
                    // If there are any added objects, check if one should be launched
                    bool addedLauched = CheckAddedObjects(baseChanceSum);

                    if (!addedLauched)
                    {
                        GameItemData launched = null;

                        // Make adjustments if max chain has been reached. But only if there is more than one item in the list                  
                        if (itemData.Count > 1 && chainLength >= maxChain)
                        {
                            List<LevelData.LevelInteractableObjects> itemList = new List<LevelData.LevelInteractableObjects>(itemData);
                            itemList = itemList.Where(i => i.ItemData != lastLaunched).ToList();
                            launched = LaunchBaseObject(itemList.AsReadOnly(), itemList.Sum(i => i.SpawnChance));
                        }
                        else
                        {
                            launched = LaunchBaseObject(itemData, baseChanceSum);
                        }

                        // Do chain calculations
                        if(lastLaunched == null || launched == lastLaunched)
                        {
                            chainLength += 1;
                        }
                        else
                        {
                            chainLength = 1;
                        }

                        lastLaunched = launched;
                    }

                    // Select a new angle for the launcher
                    float newAngle = Random.Range(-spread, spread);
                    transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, newAngle));

                    yield return new WaitForSeconds(interval);
                }
                else
                {
                    yield return null;
                }
            }
        }

        private GameItemData LaunchBaseObject(ReadOnlyCollection<LevelData.LevelInteractableObjects> itemData, float baseChanceSum)
        {
            // Select random object from set and launch it
            float chance = Random.Range(0f, 1f);
            float checkProgress = 0f;

            foreach (var item in itemData)
            {
                if (checkProgress <= chance && chance < checkProgress + item.SpawnChance / baseChanceSum)
                {
                    LaunchObject(item.ItemData);
                    return item.ItemData;
                }
                else
                {
                    checkProgress += item.SpawnChance;
                }
            }

            return null;
        }

        private bool CheckAddedObjects(float baseChanceSum)
        {
            if (addedObjects.Count > 0)
            {
                float addedObjectsChance = addedObjects.Sum(i => i.Chance);
                float totalChance = baseChanceSum + addedObjectsChance;
                float normalisedBaseChance = baseChanceSum / totalChance;

                float typeChance = Random.Range(0f, 1f);

                if (typeChance > normalisedBaseChance)
                {
                    LaunchAddedObject(addedObjectsChance);
                    return true;
                }
            }

            return false;
        }

        private void LaunchAddedObject(float totalChance)
        {
            if (addedObjects.Count == 1)
            {
                QueuedObject obj = addedObjects[0];
                LaunchObject(obj.ItemData);
                if (!obj.Repeat)
                {
                    addedObjects.Remove(obj);
                }
            }
            else
            {
                float chance = Random.Range(0f, 1f);
                float checkProgress = 0f;
                QueuedObject launchedObj = null;

                foreach (var item in addedObjects)
                {
                    if (checkProgress <= chance && chance < checkProgress + item.Chance / totalChance)
                    {
                        LaunchObject(item.ItemData);
                        launchedObj = item;
                        break;
                    }
                    else
                    {
                        checkProgress += item.Chance;
                    }
                }

                if (launchedObj != null && !launchedObj.Repeat)
                {
                    addedObjects.Remove(launchedObj);
                }
            }
        }

        private void LaunchObject(GameItemData itemData)
        {
            GameObject obj = factory.GetNextObject(itemData.TypeOfItem);
            if (obj != null)
            {
                obj.transform.position = transform.position;
                obj.transform.rotation = transform.rotation;

                InteractableObject io = obj.GetComponent<InteractableObject>();
                io.Initialise(itemData);

                Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
                rb.AddForce(obj.transform.up * gameplay.ObjectForce);
                rb.AddTorque(gameplay.ObjectTorque, ForceMode2D.Impulse);

                gameplay.ObjectSpawned(io);
            }
        }

        private class QueuedObject
        {
            public GameItemData ItemData { get; set; }
            public float Chance { get; set; }
            public bool Repeat { get; set; }
        }
    }
}
