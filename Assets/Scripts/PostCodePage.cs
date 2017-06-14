using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using CigBreak;
using System.Xml;

public class PostCodePage : MonoBehaviour
{
    [SerializeField]
    private InputField m_InputFilead01;

    [SerializeField]
    private GameObject m_ValidePostCode = null;

    [SerializeField]
    private GameObject m_InvalidePostCode = null;

    [SerializeField]
    private ErrorPage m_ErrorPage = null;

    [SerializeField]
    private GameObject m_MainPage = null;

    public void OnOk()
    {
     
        string url = "https://www.healthygames.uk/index.php?Request=api";

        WWWForm form = new WWWForm();

        form.AddField("Action", "ValidatePostcode");
        form.AddField("Postcode", m_InputFilead01.text);

        WWW www = new WWW(url, form);

        StartCoroutine(WaitForRequest(www));
    }

    XmlDocument xml;

    IEnumerator WaitForRequest(WWW www)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
            Debug.Log("WWW Ok!: " + www.text);

            xml = new XmlDocument();
            xml.LoadXml(www.text);
            XmlNode isEliglible = xml.GetElementsByTagName("IsEligible")[0];

            if (isEliglible.InnerText == "1")
            {
                PlayerProfile.GetProfile().Postcode = m_InputFilead01.text;
                PlayerProfile.GetProfile().Token = xml.GetElementsByTagName("Token")[0].InnerText;
                m_ValidePostCode.SetActive(true);
            }
            else
            {
                m_InvalidePostCode.SetActive(true);
            }
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
            m_ErrorPage.ErrorText = "Ooops! Something whent wrong. Please check your internet connection and try again.";
            m_ErrorPage.PreviousPage = this.gameObject;
            m_ErrorPage.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    public void BackBT()
    {
        gameObject.SetActive(false);
        m_MainPage.SetActive(true);
    }
}
