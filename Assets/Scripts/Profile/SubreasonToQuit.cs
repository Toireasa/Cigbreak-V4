using UnityEngine;
using System.Collections;

namespace CigBreak
{
    /// <summary>
    /// Data object representing a single reason for quiting under the cateogry represented by ReasonToQuit
    /// Contains reference to all health messages that are relevant to this subreason
    /// </summary>
    public class SubreasonToQuit : ScriptableObject
    {
        [SerializeField]
        private string subreasonID = "";
        public string SubreasonID { get { return subreasonID; } }

        [SerializeField]
        private string subreasonName = "";
        public string Subreason { get { return subreasonName; } }

        // Store reference to appropriate health messages here
    }
}