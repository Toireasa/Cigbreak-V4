using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialPanel : MonoBehaviour
{
    [SerializeField]
    Camera m_MainCamera = null;

    [SerializeField]
    Text m_TutorialText = null;

    [SerializeField]
    GameObject m_Panel = null;

    int m_Step;
    // Use this for initialization
    void Start()
    {
        m_Step = 1;

        m_TutorialText.text = "You're taking the first step to being free of cigarettes forever";


    }

    // Update is called once per frame
    void Update()
    {
        //if (m_Step==2)
        //{

        //}
    }

    public void NextStep()
    {
        m_Step++;
        if (m_Step == 2)
        {
            m_Panel.SetActive(false);
            StartCoroutine(FirstStep());
        }
        else if (m_Step == 3)
        {
            StartCoroutine(SecondStep());
        }
        else
        {
            PlayerPrefs.SetString("Tutorial", "OFF");
            LoadingScreen.LoadScene(CigBreakConstants.SceneNames.Journal);
        }
    }

    private IEnumerator FirstStep()
    {
        Vector3 startPosition = m_MainCamera.transform.position;
        Vector3 positionChange = new Vector3(-1.87f, 9.24f, 18.24f);
        float speed = positionChange.magnitude / 10;
        float t = 0f;

        while (m_MainCamera.transform.position != positionChange)
        {
            m_MainCamera.transform.position = Vector3.Lerp(startPosition, positionChange, t);
            t += Time.deltaTime * speed;
            yield return null;
        }
        m_Panel.SetActive(true);
        m_TutorialText.text = "Your goal is to reach the finish line";
    }

    private IEnumerator SecondStep()
    {
        Vector3 startPosition = m_MainCamera.transform.position;
        Vector3 positionChange = new Vector3(3.4f, 9.24f, -24.0f);
        float speed = positionChange.magnitude / 10;
        float t = 0f;

        while (m_MainCamera.transform.position != positionChange)
        {
            m_MainCamera.transform.position = Vector3.Lerp(startPosition, positionChange, t);
            t += Time.deltaTime * speed;
            yield return null;
        }
        m_Panel.SetActive(true);
        m_TutorialText.text = "Your first step to quitting is to begin your quit journal";
    }
}
