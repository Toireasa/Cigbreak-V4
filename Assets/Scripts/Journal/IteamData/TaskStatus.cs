using UnityEngine;
using System.Collections;

public class TaskStatus
{
    private int m_ID;
    public int ID
    {
        get { return m_ID; }
        set { m_ID = value; }
    }

    private string m_Answer;
    public string Answer
    {
        get { return m_Answer; }
        set { m_Answer = value; }
    }

    private string m_PhotoName;
    public string PhotoName
    {
        get { return m_PhotoName; }
        set { m_PhotoName = value; }
    }

    private bool m_Completed = false;
    public bool Completed
    {
        get { return m_Completed; }
        set { m_Completed = true; }
    }
}

