using UnityEngine;
using System.Collections;

namespace CigBreak
{
    public class TasksData : ScriptableObject
    {
        public enum TaskType
        {
            ListThree,
            ListOne,
            Photo,
            Simple,
            CalculateMoney
        }

        [SerializeField]
        private int m_ID = 0;
        public int ID
        {
            get {return m_ID;}
        }

        [SerializeField]
        private string m_Title = "";
        public string Title
        {
            get { return m_Title; }
        }

        [SerializeField]
        private int m_Coins = 0;
        public int Coins
        {
            get { return m_Coins; }
        }

        [SerializeField]
        [Multiline]
        private string m_Statement = "";
        public string Statement
        {
            get { return m_Statement; }
        }

        [SerializeField]
        [Multiline]
        private string m_Comment = "";
        public string Comment
        {
            get { return m_Comment; }
        }

        [SerializeField]
        private TaskType m_TaskType;
        public TaskType Type
        {
            get { return m_TaskType; }
        }

        [SerializeField]
        private int m_Requirement = 0;
        public int Requirement
        {
            get { return m_Requirement; }
        }

        [SerializeField]
        private bool m_FreeToPlay = true;
        public bool FreeToPlay
        {
            get { return m_FreeToPlay; }
        }
    }
}
