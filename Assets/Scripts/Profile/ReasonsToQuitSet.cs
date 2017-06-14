using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CigBreak
{
    /// <summary>
    /// All ReasonToQuit objects to be used in the game
    /// </summary>
    public class ReasonsToQuitSet : ScriptableObject
    {
        [SerializeField]
        private ReasonToQuit[] reasonsToQuit = null;
        public IEnumerable<ReasonToQuit> ReasonsToQuit { get { return reasonsToQuit; } }
    }
}