using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Xml;
using CigBreak;

public class ConfirmEmailPage : MonoBehaviour
{
    [SerializeField]
    private Text m_ConfirmEmailText = null;

    [SerializeField]
    private GameObject m_NextPage = null;

    [SerializeField]
    private GameObject m_PreviousPage = null;


    [SerializeField]
    private ErrorPage m_ErrorPage = null;


    // Use this for initialization
    void OnEnable ()
    {
        m_ConfirmEmailText.text = "Please confirm \n your email address. \n" + PlayerProfile.GetProfile().Email;

    }

    public void OnOk()
    {

        string url = "https://www.healthygames.uk/index.php?Request=api";

        WWWForm form = new WWWForm();

        form.AddField("Action", "RegisterFreeAccount");
        form.AddField("Token", PlayerProfile.GetProfile().Token);
        form.AddField("Email", PlayerProfile.GetProfile().Email);

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
            XmlNode isEliglible = xml.GetElementsByTagName("UserID")[0];

            if (isEliglible != null)
            {
                m_NextPage.SetActive(true);
                gameObject.SetActive(false);
                PlayerProfile.GetProfile().UnlockFullGame = true;
            }
            else
            {
                xml = new XmlDocument();
                xml.LoadXml(www.text);
                XmlNode error = xml.GetElementsByTagName("result")[0];

                switch (error.Attributes.GetNamedItem("result_code").Value)
                {
                    case "100":
                        m_ErrorPage.ErrorText = error.InnerText;
                        break;
                    case "5":
                        m_ErrorPage.ErrorText = error.InnerText;
                        break;

                    default:
                        break;
                }

                m_ErrorPage.PreviousPage = this.gameObject;
                m_ErrorPage.gameObject.SetActive(true);
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


    public void OnBack()
    {
        m_PreviousPage.SetActive(true);
        gameObject.SetActive(false);
    }
}
