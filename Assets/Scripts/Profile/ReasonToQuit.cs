using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CigBreak
{
    /// <summary>
    /// Data object representing the main category of reason to quit
    /// Contains reference to all subreasons to be represented under this reason
    /// </summary>
    public class ReasonToQuit : ScriptableObject
    {
        [SerializeField]
        private string mainReasonID = "";
        public string MainReasonID { get { return mainReasonID; } }

        [SerializeField]
        private string mainReasonName = "";
        public string MainReasonName { get { return mainReasonName; } }

        [SerializeField]
        private string mainResonDescription = "";
        public string MainReasonDescription { get { return mainResonDescription; } }

        [SerializeField]
        private SubreasonToQuit[] subReasons = null;
        public IEnumerable<SubreasonToQuit> SubReasons { get { return subReasons; } }
    }
}
