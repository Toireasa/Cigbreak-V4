using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CigBreak
{
    /// <summary>
    /// Represents all badges to be included in the game
    /// </summary>
    public class BadgeSet : ScriptableObject
    {
        [SerializeField]
        private Badge[] badges = null;
        public IEnumerable<Badge> Badges { get { return badges; } }
    } 
}
