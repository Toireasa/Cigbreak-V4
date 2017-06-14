using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace CigBreak
{
    /// <summary>
    /// Represents all levels in the game and provides access to their data objects
    /// </summary>
    public class LevelSet : ScriptableObject
    {
        [SerializeField]
        private LevelData[] levelData = null;
        public LevelData[] LevelData { get { return levelData; } }

        // Supply a separate IndexOf function, since it's not accessible via IEnumerable<>
        public int IndexOf(LevelData data)
        {
            return Array.IndexOf(levelData, data);
        }
    } 
}
