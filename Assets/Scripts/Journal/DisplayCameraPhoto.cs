using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;

public class DisplayCameraPhoto : MonoBehaviour 
{
    [SerializeField]
    RawImage m_PhotoHolder = null;

   // private WebCamTexture webCamTexture;
    private Quaternion baseRotation;

	// Use this for initialization
	void OnEnable () 
    {
        baseRotation = Quaternion.identity;
        m_PhotoHolder.transform.rotation = Quaternion.identity;

       // webCamTexture = new WebCamTexture();
       // webCamTexture.Play();
        byte[] test = File.ReadAllBytes(Application.persistentDataPath + "/JournalLovePhoto.png");
        Texture2D myTexture = new Texture2D(2, 2);
        myTexture.LoadImage(test);

        baseRotation = m_PhotoHolder.transform.rotation;

       // webCamTexture.Stop();
        m_PhotoHolder.texture = myTexture;
        m_PhotoHolder.transform.rotation = baseRotation * Quaternion.AngleAxis(90, Vector3.back);
	}

}
