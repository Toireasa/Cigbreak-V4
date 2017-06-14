using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine.Serialization;

namespace CigBreak
{
    /// <summary>
    /// Data object representing a single level and its details
    /// </summary>
    public class LevelData : ScriptableObject
    {
        [SerializeField]
        private string levelID = "";
        public string LevelID { get { return levelID; } }

        [SerializeField]
        private bool m_FreeToPlay = false;
        public bool FreeToPlay { get { return m_FreeToPlay; } }

        [SerializeField]
        private AudioClip music = null;
        public AudioClip Music { get { return music; } }

        [FormerlySerializedAs("requiredCoins")]
        [SerializeField]
        private int cigaretteTarget = 0;
        public int CigaretteTarget { get { return cigaretteTarget; } }

        [SerializeField]
        private TutorialData tutorial = null;
        public TutorialData Tutorial { get { return tutorial; } }

        [SerializeField]
        private float minTorque = 1f;
        public float MinTorque { get { return minTorque; } }
        [SerializeField]
        private float maxTorque = 3f;
        public float MaxTorque { get { return maxTorque; } }

        [SerializeField]
        private LevelRewards levelRewards = null;
        public LevelRewards LevelRewards { get { return levelRewards; } }

        [SerializeField]
        private bool noMapExit = false;
        public bool NoMapExit { get { return noMapExit; } }

        [SerializeField]
        private bool forceMapExit = false;
        public bool ForceMapExit { get { return forceMapExit; } }

        [SerializeField]
        private List<LevelInteractableObjects> levelObjects = null;
        public ReadOnlyCollection<LevelInteractableObjects> LevelObjects { get { return levelObjects.AsReadOnly(); } }

        [SerializeField]
        private LevelSpawnTimers spawnTimers = null;
        public LevelSpawnTimers SpawnTimers { get { return spawnTimers; } }

        [System.Serializable]
        public class LevelInteractableObjects
        {
            [SerializeField]
            private GameItemData itemData = null;
            public GameItemData ItemData { get { return itemData; } }
            [SerializeField]
            [Range(0f, 1f)]
            private float spawnChance = 1f;
            public float SpawnChance { get { return spawnChance; } }
        }

        [System.Serializable]
        public class LevelSpawnTimers
        {
            [SerializeField]
            private List<SpawnTimer> spawnTimers = null;
            public ReadOnlyCollection<SpawnTimer> SpawnTimers { get { return spawnTimers.AsReadOnly(); } }

            [System.Serializable]
            public class SpawnTimer
            {
                [SerializeField]
                private float firstSpawnDelay = 0f;
                public float FirstSpawnDelay { get { return firstSpawnDelay; } }
                [SerializeField]
                private float timeBeforeRepeating = 0f;
                public float TimeBeforeRepeating { get { return timeBeforeRepeating; } }
            }
        }
    }
}
