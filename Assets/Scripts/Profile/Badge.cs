using UnityEngine;
using System.Collections;

namespace CigBreak
{
    /// <summary>
    /// Data object representing a single badge that can be awarded in the game
    /// </summary>
    public class Badge : ScriptableObject
    {
        [SerializeField]
        private string badgeID = "";
        public string BadgeID { get { return badgeID; } }

        [SerializeField]
        private string title = "";
        public string Title { get { return title; } }

        [SerializeField]
        private Sprite icon = null;
        public Sprite Icon { get { return icon; } }

        [SerializeField]
        private int coinsRequired = 0;
        public int CoinsRequired { get { return coinsRequired; } }

        [SerializeField]
        [Multiline]
        private string description = "";
        public string Description { get { return description; } }
    } 
}
