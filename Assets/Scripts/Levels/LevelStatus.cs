using UnityEngine;
using System.Collections;

namespace CigBreak
{
    /// <summary>
    /// Record of the level status for the player profile
    /// </summary>
    [System.Serializable]
    public class LevelStatus
    {
        // LevelID identifies the level data for the level
        public string LevelID { get; set; }
        // Max stars earned in this level by the user
        public int StarsEarned { get; set; }
    } 
}
