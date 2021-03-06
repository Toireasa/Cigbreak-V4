using UnityEngine;
using System.Collections;

public class JournalAnimationController : MonoBehaviour
{
    [SerializeField]
    private GameObject m_AnimatedJournal = null;

    [SerializeField]
    private GameObject m_JournalPages = null;

    private bool m_JournalStarted = false;

    private void Awake()
    {
        m_AnimatedJournal.GetComponent<Animation>().Rewind();
        m_AnimatedJournal.GetComponent<Animation>().Play();
    }

    public void Update()
    {
        if (!m_JournalStarted && !m_AnimatedJournal.GetComponent<Animation>().isPlaying)
        {
            m_JournalStarted = true;
            m_JournalPages.SetActive(true);
        }
    }
}
