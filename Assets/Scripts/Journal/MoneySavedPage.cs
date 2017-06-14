using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using CigBreak;

public class MoneySavedPage : MonoBehaviour
{
    [SerializeField]
    RawImage m_PhotoHolder = null;

    [SerializeField]
    Text m_MoneySaveTxt = null;


    [SerializeField]
    private JournalSharePage m_SharePanel = null;

    [SerializeField]
    private GameObject m_Main = null;


    private Quaternion baseRotation;

    string m_currencySymbol;
    // Use this for initialization
    void OnEnable()
    {
        float _moneyPerDay = ((float)PlayerProfile.GetProfile().CostOfPack / (float)PlayerProfile.GetProfile().NumOfCigPerPack) * (float)PlayerProfile.GetProfile().NumOfCigPerDay;
        System.TimeSpan ts = System.DateTime.Now - PlayerProfile.GetProfile().QuitDate.Value;


        if (PlayerProfile.GetProfile().Contry == Country.US)
        {
            m_currencySymbol = "$";
        }
        else
        {
            m_currencySymbol = "£";
        }

        m_MoneySaveTxt.text = "You have saved " + m_currencySymbol+(_moneyPerDay * ts.Days).ToString() ;

        if (File.Exists(CigBreakConstants.Paths.Photos + "/Task3.png"))
        {
            m_PhotoHolder.transform.rotation = Quaternion.identity;

            byte[] test = File.ReadAllBytes(Application.persistentDataPath + "/Task3.png");
            Texture2D myTexture = new Texture2D(2, 2);
            myTexture.LoadImage(test);

            baseRotation = m_PhotoHolder.transform.rotation;

            m_PhotoHolder.texture = myTexture;
            m_PhotoHolder.transform.rotation = baseRotation * Quaternion.AngleAxis(90, Vector3.back);
        }
    }

    public void Share()
    {

        m_SharePanel.SetDescriptionMessage = "Quitting the habit is saving me lots of money. Cigbreak is keeping me on track and I’m looking forward to buying myself a treat";
        float _moneyPerDay = ((float)PlayerProfile.GetProfile().CostOfPack / (float)PlayerProfile.GetProfile().NumOfCigPerPack) * (float)PlayerProfile.GetProfile().NumOfCigPerDay;
        System.TimeSpan ts = System.DateTime.Now - PlayerProfile.GetProfile().QuitDate.Value;


        m_SharePanel.SetImageURL = "http://static.wixstatic.com/media/ac9214_9b134fa3262a401db0d0c2ed479294c7.png/v1/fill/w_251,h_250,al_c/ac9214_9b134fa3262a401db0d0c2ed479294c7.png";
        m_SharePanel.ShareTitleText = "I HAVE SAVED " + (_moneyPerDay * ts.Days).ToString() + m_currencySymbol;

        m_SharePanel.gameObject.SetActive(true);
        m_Main.SetActive(false);
    }

    public void Back()
    {
        m_SharePanel.gameObject.SetActive(false);
        m_Main.SetActive(true);
    }
}
