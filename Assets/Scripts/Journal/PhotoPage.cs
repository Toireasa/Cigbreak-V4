using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;

public class PhotoPage : MonoBehaviour 
{
    [SerializeField]
    private Text m_Text = null;

    [SerializeField]
    private GameObject m_DetailsPage = null;

    [SerializeField]
    RawImage m_PhotoHolder = null;

    [SerializeField]
    GameObject m_CaptureBT = null;

    [SerializeField]
    GameObject m_EditBT = null;

    private WebCamTexture webCamTexture;

    private Quaternion baseRotation;
	private Quaternion m_InitRotation;
	#if UNITY_IOS
	private Vector3 m_InitScale ;
	#endif

    // Use this for initialization
    void Start () 
    {
        baseRotation = Quaternion.identity;
        m_PhotoHolder.transform.rotation = Quaternion.identity;
		m_InitRotation = m_PhotoHolder.transform.rotation;
        if (!File.Exists(Application.persistentDataPath + "/JournalLovePhoto.png"))
        {
            EditBT();
            m_Text.text = "Take a photo of someone who is motivating you to quit";
            m_CaptureBT.SetActive(true);
        }
        else
        {
            DisplayPhoto();
            m_Text.text = "This person is motivating me to quit";
            m_EditBT.SetActive(true);
        }
	}

    public void DisplayPhoto()
    {
        baseRotation = Quaternion.identity;
        m_PhotoHolder.transform.rotation = Quaternion.identity;
        m_Text.text = "This person is motivating me to quit";

        byte[] test = File.ReadAllBytes(Application.persistentDataPath + "/JournalLovePhoto.png");
        Texture2D myTexture = new Texture2D(2, 2);
        myTexture.LoadImage(test);

        baseRotation = m_PhotoHolder.transform.rotation;

        m_PhotoHolder.texture = myTexture;

        Vector3 v = m_PhotoHolder.transform.rotation.eulerAngles;
        m_PhotoHolder.transform.rotation = Quaternion.Euler(9 , 2, -90);


    }

    public void EditBT()
    {
        m_EditBT.SetActive(false);
        m_CaptureBT.SetActive(true);
        m_Text.text = "Take a photo of someone who is motivating you to quit";

        baseRotation = Quaternion.identity;
        m_PhotoHolder.transform.rotation = Quaternion.identity;


#if UNITY_IOS
		m_InitScale = m_PhotoHolder.transform.localScale;
#endif

        webCamTexture = new WebCamTexture();

        if (webCamTexture != null)
        {
#if UNITY_IOS
			m_PhotoHolder.transform.localScale = new Vector3 (-1,1, 1);
            m_PhotoHolder.transform.rotation = m_PhotoHolder.transform.rotation * Quaternion.AngleAxis(-90, Vector3.back);
# elif UNITY_ANDROID
            m_PhotoHolder.transform.rotation = m_PhotoHolder.transform.rotation * Quaternion.AngleAxis(90, Vector3.back);
#endif

            m_PhotoHolder.texture = webCamTexture;
            webCamTexture.Play();
        }
    }

    // ** Buttons Methodes ** //
    public void TakePhoto()
    {
        baseRotation = Quaternion.identity;
        m_PhotoHolder.transform.rotation = Quaternion.identity;

        if (webCamTexture != null)
        {
            Texture2D photo = new Texture2D(webCamTexture.width, webCamTexture.height);
            photo.SetPixels(webCamTexture.GetPixels());
            photo.Apply();

            //Encode to a PNG
            byte[] bytes = photo.EncodeToPNG();
            //Write out the PNG. Of course you have to substitute your_path for something sensible
            File.WriteAllBytes(Application.persistentDataPath + "/JournalLovePhoto.png", bytes);

            webCamTexture.Stop();
        }

        m_EditBT.SetActive(true);
        m_CaptureBT.SetActive(false);
    }
    
    public void OnDisable()
    {
        if (webCamTexture != null)
        {
            webCamTexture.Stop();
            Destroy(webCamTexture);
            webCamTexture = null;
        }
		#if UNITY_IOS
		m_PhotoHolder.transform.localScale = m_InitScale;
		#endif
		m_PhotoHolder.transform.rotation = m_InitRotation;
        m_DetailsPage.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
