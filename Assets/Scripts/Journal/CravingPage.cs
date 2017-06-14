using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using CigBreakGUI;
using CigBreak;

public class CravingPage : MonoBehaviour
{
    [SerializeField]
    GameObject m_MainPage = null;

    [SerializeField]
    GameObject m_TipasPage = null;
    
    [SerializeField]
    RawImage m_PersonePhotoHolder = null;

    [SerializeField]
    RawImage m_RewardPhotoHolder = null;

    [SerializeField]
    Text m_TipsText = null;

    [SerializeField]
    private LevelPopup m_LevelPopUp = null;

    private Quaternion baseRotation;

    void OnEnable()
    {
        baseRotation = Quaternion.identity;
        m_PersonePhotoHolder.transform.rotation = Quaternion.identity;
        m_RewardPhotoHolder.transform.rotation = Quaternion.identity;

        if (File.Exists(Application.persistentDataPath + "/JournalLovePhoto.png"))
        {

            byte[] test = File.ReadAllBytes(Application.persistentDataPath + "/JournalLovePhoto.png");
            Texture2D myTexture = new Texture2D(2, 2);
            myTexture.LoadImage(test);

            baseRotation = m_PersonePhotoHolder.transform.rotation;


            m_PersonePhotoHolder.texture = myTexture;
            m_PersonePhotoHolder.transform.rotation = baseRotation * Quaternion.AngleAxis(90, Vector3.back);
        }
        if (File.Exists(CigBreakConstants.Paths.Photos + "/Task3.png"))
        {
            m_RewardPhotoHolder.transform.rotation = Quaternion.identity;

            byte[] test = File.ReadAllBytes(Application.persistentDataPath + "/Task3.png");
            Texture2D myTexture = new Texture2D(2, 2);
            myTexture.LoadImage(test);

            baseRotation = m_RewardPhotoHolder.transform.rotation;

            m_RewardPhotoHolder.texture = myTexture;
            m_RewardPhotoHolder.transform.rotation = baseRotation * Quaternion.AngleAxis(90, Vector3.back);
        }
    }

    public void TipsBT()
    {
        m_MainPage.SetActive(false);
        m_TipasPage.SetActive(true);
    }


    public void BackBT()
    {
        m_MainPage.SetActive(true);
        m_TipasPage.SetActive(false);
    }

    public void PlayBT()
    {
        int CurrentLevelIndex = PlayerProfile.GetProfile().GetLevelResults().Count;

        if (CurrentLevelIndex > 29)
        {
            CurrentLevelIndex = 29;
        }

        m_LevelPopUp.Initialise(Resources.Load<LevelSet>(CigBreakConstants.Paths.LevelSet).LevelData [CurrentLevelIndex], CurrentLevelIndex);
        m_LevelPopUp.gameObject.SetActive(true);
    }
}
