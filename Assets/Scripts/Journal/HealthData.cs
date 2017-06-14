using UnityEngine;
using System.Collections;

namespace CigBreak
{
    public class HealthData : ScriptableObject
    {
        [SerializeField]
        private string m_Title = "";
        public string Title
        {
            get { return m_Title; }
        }

        [SerializeField]
        [Multiline]
        private string m_Statement = "";
        public string Statement
        {
            get { return m_Statement; }
        }

        [SerializeField]
        private float m_DaysToAchive = 0;
        public float DaysToAchive
        {
            get { return m_DaysToAchive; }
        }
    }
}
