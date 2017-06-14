using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using CigBreakGUI;
using CigBreak;
using System.Linq;

public class ListOneTaskPage : TaskPage
{
    [SerializeField]
    protected InputField m_Answer;

    protected override void OnEnable()
    {
        base.OnEnable();

        //if (PlayerProfile.GetProfile().TaskStatus != null)
        {
            m_Status = PlayerProfile.GetProfile().TaskStatus.FirstOrDefault(r => r.ID == m_Task.ID);

            if (m_Status != null)
            {
                m_Answer.text = m_Status.Answer;

            }
        }
    }

    private void OnDisable()
    {
        m_Answer.text = "";
    }

    public override void OnOk()
    {
        if (m_Answer.text != string.Empty)
        {
            base.OnOk();
            PlayerProfile.GetProfile().RecordTaskStatus(m_Task.ID, m_Answer.text);
        }
    }
}
