using UnityEngine;
using System.Collections;
using Soomla.Profile;
using UnityEngine.UI;
using CigBreak;

public class JournalSharePage : MonoBehaviour
{
    [SerializeField]
    private GameObject m_RewardPanel = null;

    [SerializeField]
    private GameObject m_SharePanel = null;

    [SerializeField]
    private Text m_MessageText = null;

    [SerializeField]
    private Text m_TitleText = null;

    [SerializeField]
    private Image m_BadgeMessageIcon = null;
    public Sprite SetBadgeIcon
    {
        set { m_BadgeMessageIcon.sprite = value; }
    }
    
    private string m_NameText = null;
    public string SetNameMessage
    {
        set {
            m_TitleText.text = value;
            m_NameText = value;
        }
    }

    private string m_ShareTitleText = null;
    public string ShareTitleText
    {
        set
        {
            m_ShareTitleText = value;
        }
    }

    private string m_DescriptionText = null;
    public string SetDescriptionMessage
    {
        set {
            m_DescriptionText = value;
        }
    }

    private string m_ImageURL = "https://static.wixstatic.com/media/ac9214_8a68e0cc7a2d4bb697960d55ba8353c8.jpeg/v1/fill/w_240,h_240,al_c,q_90/ac9214_8a68e0cc7a2d4bb697960d55ba8353c8.jpeg";
    public string SetImageURL
    {
        set { m_ImageURL = value; }
    }

    public void SetMessageText()
    {
        m_TitleText.transform.rotation = Quaternion.Euler(0, 0, 11.73f);
    }

    private Provider m_Provider = null;

    private void OnEnable()
    {
        if (m_RewardPanel != null)
        {
            m_RewardPanel.SetActive(false);
        }
        m_SharePanel.SetActive(true);
        ProfileEvents.OnLoginFinished += onLoginFinished;
        ProfileEvents.OnLoginFailed += onLoginFailed;
        ProfileEvents.OnSocialActionFailed += onSocialActionFailed;
        ProfileEvents.OnSocialActionFinished += onSocialActionFinished;
        ProfileEvents.OnSocialActionCancelled += onSocialActionCancelled;
    }

    private void OnDisable()
    {
        ProfileEvents.OnLoginFinished -= onLoginFinished;
        ProfileEvents.OnLoginFailed -= onLoginFailed;
        ProfileEvents.OnSocialActionFailed -= onSocialActionFailed;
        ProfileEvents.OnSocialActionFinished -= onSocialActionFinished;
        ProfileEvents.OnSocialActionCancelled -= onSocialActionCancelled;

        if (m_TitleText !=null)
        {
            m_TitleText.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void Share(Provider _provider)
    {
        if (!SoomlaProfile.IsLoggedIn(_provider))
        {
            SoomlaProfile.Login(_provider);
            m_Provider = _provider;
        }
        else
        {
            SoomlaProfile.UpdateStoryDialog(
                _provider,                             // Provider
                m_ShareTitleText,                          // Name
                "Cigbreak Story",                             // Caption
                m_DescriptionText,                   // Description
                "http://www.healthygames.co.uk/",               // Link to post
                m_ImageURL,  // Image URL
                "",                                            // Payload
                null                                           // Reward
                );
        }
    }


    public void onLoginFinished(UserProfile userProfileJson, bool autoLogin, string payload)
    {

        SoomlaProfile.UpdateStoryDialog(
            m_Provider,                             // Provider
            m_ShareTitleText,                          // Name
            "Cigbreak Story",                             // Caption
            m_DescriptionText,                     // Description
            "http://www.healthygames.co.uk/",               // Link to post
            m_ImageURL,  // Image URL
            "",                                            // Payload
            null                                           // Reward
            );
    }


    public void onLoginFailed(Provider provider, string message, bool autoLogin, string payload)
    {
#if DEBUG
        m_MessageText.text = message;
#endif
    }

    public void onSocialActionFailed(Provider provider, SocialActionType action, string message, string payload)
    {
#if DEBUG
        m_MessageText.text = message;
#endif
    }

    public void onSocialActionFinished(Provider provider, SocialActionType action, string payload)
    {
        PlayerProfile.GetProfile().AddCoins(1);
        m_RewardPanel.SetActive(true);
        m_SharePanel.SetActive(false);
    }

    public void onSocialActionCancelled(Provider provider, SocialActionType action, string payload)
    {
        m_RewardPanel.SetActive(false);
        m_SharePanel.SetActive(true);
    }

    // Buttons Methodes
    public void ShareOnFacebook()
    {
        Share(Provider.FACEBOOK);
    }
    public void ShareOnGoogle()
    {
        Share(Provider.GOOGLE);
    }

    public void ShareOnTwitter()
    {
        Share(Provider.TWITTER);
    }

    public void BackBT()
    {
        gameObject.SetActive(false);
    }

}
