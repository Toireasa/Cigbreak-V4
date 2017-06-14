using UnityEngine;
using System.Collections;

public class BadgeData : ScriptableObject
{

    [SerializeField]
    private Sprite m_Icon;
    public Sprite Icon
    {
        get { return m_Icon; }
    }
    public enum BadgeType
    {
        QuitDate,
        Time,
        LastTrophy
    }
    [SerializeField]
    [HideInInspector]
    private BadgeType m_BadgeType;
    public BadgeType GetBadgeType
    {
        get { return m_BadgeType; }
    }


    [SerializeField]
    [HideInInspector]
    private int m_Hours;
    public int Hours
    {
        get { return m_Hours; }
        set { m_Hours = value; }
    }

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
}
