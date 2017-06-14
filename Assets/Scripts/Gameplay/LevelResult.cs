using UnityEngine;
using System.Collections;

namespace CigBreak
{
    /// <summary>
    /// Accumulates values related to completion of a single level
    /// Used to pass this data across from game scene to tally screen
    /// </summary>
    [System.Serializable]
    public class LevelResults
    {
        public bool Completed { get; set; }
        public float MoneySaved { get; set; }
        public float MaxMoney { get; set; }
        public int VegBroken { get; set; }
        //public float BrokenVegValue { get; set; }
        public float RemainingHealthPercentage { get; set; }

        /// <summary>
        /// Serialises the object in its current state and stores the result using PlayerPrefs
        /// </summary>
        public void Save()
        {
            string serialised = Utility.JsonUtility.SerializeToJson<LevelResults>(this);
            PlayerPrefs.SetString(CigBreakConstants.PlayerPrefKeys.LastLevelResult, serialised);
        }
    }

}
