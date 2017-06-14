using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;

public class CameraCapture : MonoBehaviour 
{
    [SerializeField]
    RawImage m_PhotoHolder = null;

    private WebCamTexture webCamTexture;
    private Quaternion m_InitRotation;

#if UNITY_IOS
	private Vector3 m_InitScale ;
#endif


    void OnEnable()
    {
		m_InitRotation = m_PhotoHolder.transform.rotation;
#if UNITY_IOS
			m_InitScale = m_PhotoHolder.transform.localScale;
#endif

        webCamTexture = new WebCamTexture();

		if(webCamTexture!=null)
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

	public void OnDisable()
	{
#if UNITY_IOS
		m_PhotoHolder.transform.localScale = m_InitScale;
#endif
		m_PhotoHolder.transform.rotation = m_InitRotation;

        if (webCamTexture != null)
        {
            webCamTexture.Stop();
            Destroy(webCamTexture);
            webCamTexture = null;
        }
    }

	//public void OnExitJournal()
	//{

 //   }

    // ** Buttons Methodes ** //
    public void TakePhoto()
    {
		if (webCamTexture != null) {
			Texture2D photo = new Texture2D (webCamTexture.width, webCamTexture.height);
			photo.SetPixels (webCamTexture.GetPixels ());
			photo.Apply ();        

			//Encode to a PNG
			byte[] bytes = photo.EncodeToPNG ();
			//Write out the PNG. Of course you have to substitute your_path for something sensible
			File.WriteAllBytes (Application.persistentDataPath + "/JournalLovePhoto.png", bytes);
        
			webCamTexture.Stop ();
		}
    }
}
