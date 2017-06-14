using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ErrorPage : MonoBehaviour
{
    [SerializeField]
    private Text m_ErrorTextField = null;

    private GameObject m_PreviousPage = null;
    public GameObject PreviousPage
    {
        set { m_PreviousPage = value; }
    }

    private string m_ErrorText = null;
    public string ErrorText
    {
        set { m_ErrorText = value; }
    }

	// Use this for initialization
	void OnEnable ()
    {
        m_ErrorTextField.text = m_ErrorText;

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnBack()
    {
        m_PreviousPage.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
