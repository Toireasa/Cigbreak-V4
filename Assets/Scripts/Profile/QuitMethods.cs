using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CigBreak
{
    /// <summary>
    /// Collections of all quit methods to be included in game
    /// </summary>
    public class QuitMethods : ScriptableObject
    {
        [SerializeField]
        private QuitMethod[] quitMethods = null;
        public IEnumerable<QuitMethod> QuitMethodsList { get { return quitMethods; } }
    }

    /// <summary>
    /// Data object representing details about a single quit method
    /// </summary>
    [System.Serializable]
    public class QuitMethod
    {
        [SerializeField]
        private string methodID = "";
        public string MethodID { get { return methodID; } }

        [SerializeField]
        private string methodName = "";
        public string MethodName { get { return methodName; } }

        [SerializeField]
        private string description = "";
        public string Description { get { return description; } }
    }
}
