using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CigBreak
{
    /// <summary>
    /// All smoking habit options to be included in the game
    /// </summary>
    public class SmokingHabits : ScriptableObject
    {
        [SerializeField]
        private SmokingHabit[] smokingHabits = null;
        public IEnumerable<SmokingHabit> SmokingHabitsList { get { return smokingHabits; } }
    } 

    /// <summary>
    /// A single smoking habit data object
    /// </summary>
    [System.Serializable]
    public class SmokingHabit
    {
        [SerializeField]
        private string smokingHabitID = "";
        public string SmokingHabitID { get { return smokingHabitID; } }

        [SerializeField]
        private string smokingHabitName = "";
        public string SmokingHabitName { get { return smokingHabitName; } }
    }
}
