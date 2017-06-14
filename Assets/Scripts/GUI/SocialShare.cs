using UnityEngine;
using System.Collections;
using Soomla.Profile;
using UnityEngine.UI;
using CigBreak;

public class SocialShare : MonoBehaviour
{ 
    private Provider m_Provider = null;

    [SerializeField]
    private Text m_BebugMessageText = null;

    [SerializeField]
    private GameObject m_RewardPanel = null;

    [SerializeField]
    private GameObject m_SharePanel = null;
    
    public string MessageText
    {
        set { m_BebugMessageText.text = value; }
    }

    private string m_NameText = null;
    public string SetNameMessage
    {
        set { m_NameText = value; }
    }

    private string m_DescriptionText = null;
    public string SetDescriptionMessage
    {
        set { m_DescriptionText = value; }
    }

    private string m_ImageURL = "https://static.wixstatic.com/media/ac9214_8a68e0cc7a2d4bb697960d55ba8353c8.jpeg/v1/fill/w_240,h_240,al_c,q_90/ac9214_8a68e0cc7a2d4bb697960d55ba8353c8.jpeg";
    public string SetImageURL
    {
        set { m_ImageURL = value; }
    }

    private void OnEnable()
    {
        m_RewardPanel.SetActive(false);
        m_SharePanel.SetActive(true);
        ProfileEvents.OnLoginFinished += onLoginFinished;
        ProfileEvents.OnLoginFailed += onLoginFailed;
        ProfileEvents.OnSocialActionFailed += onSocialActionFailed;
        ProfileEvents.OnSocialActionFinished += onSocialActionFinished;
    }

    private void OnDisable()
    {
        ProfileEvents.OnLoginFinished -= onLoginFinished;
        ProfileEvents.OnLoginFailed -= onLoginFailed;
        ProfileEvents.OnSocialActionFailed -= onSocialActionFailed;
        ProfileEvents.OnSocialActionFinished -= onSocialActionFinished;
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
                m_NameText,                          // Name
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
            m_NameText,                          // Name
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
        m_BebugMessageText.text = message;
#endif
    }

    public void onSocialActionFailed(Provider provider, SocialActionType action, string message, string payload)
    {
#if DEBUG
        m_BebugMessageText.text = message;
#endif
    }

    public void onSocialActionFinished(Provider provider, SocialActionType action, string payload)
    {
        PlayerProfile.GetProfile().AddCoins(1);
        m_RewardPanel.SetActive(true);
        m_SharePanel.SetActive(false);
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
