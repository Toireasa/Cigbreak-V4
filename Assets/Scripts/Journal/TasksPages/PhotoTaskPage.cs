using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Linq;
using CigBreak;

namespace CigBreakGUI
{
    public class PhotoTaskPage : TaskPage
    {
        [SerializeField]
        RawImage m_PhotoHolder = null;

        [SerializeField]
        protected GameObject m_CaptureButton = null;

        [SerializeField]
        protected GameObject m_EditButton = null;

        private WebCamTexture webCamTexture;

		private Quaternion m_InitRotation;

#if UNITY_IOS
		private Vector3 m_InitScale ;
#endif

        protected override void OnEnable()
        {
            base.OnEnable();

			m_InitRotation = m_PhotoHolder.transform.rotation;
#if UNITY_IOS
			m_InitScale = m_PhotoHolder.transform.localScale;
#endif

            if (PlayerProfile.GetProfile().TaskStatus != null)
            {
                m_Status = PlayerProfile.GetProfile().TaskStatus.FirstOrDefault(r => r.ID == m_Task.ID);
            }
            m_Title.text = m_Task.Title;
            m_Statement.text = m_Task.Statement;

            if (webCamTexture==null)
            {
                webCamTexture = new WebCamTexture();
               
            }
            
			if (!File.Exists(Application.persistentDataPath + "/Task3.png"))
            {
#if UNITY_IOS
			m_PhotoHolder.transform.localScale = new Vector3 (-1,1, 1);
            m_PhotoHolder.transform.rotation = m_PhotoHolder.transform.rotation * Quaternion.AngleAxis(-90, Vector3.back);
#elif UNITY_ANDROID
                m_PhotoHolder.transform.rotation = m_PhotoHolder.transform.rotation * Quaternion.AngleAxis(90, Vector3.back);
#endif

                if (webCamTexture!=null)
				{
					m_PhotoHolder.texture = webCamTexture;
					webCamTexture.Play();
				}

                m_CaptureButton.SetActive(true);
				m_EditButton.SetActive(false);
            }
            else
            {
				byte[] test = File.ReadAllBytes(Application.persistentDataPath + "/Task3.png");
                Texture2D myTexture = new Texture2D(2, 2);
                myTexture.LoadImage(test);
                
                m_PhotoHolder.texture = myTexture;
				m_PhotoHolder.transform.rotation = m_PhotoHolder.transform.rotation * Quaternion.AngleAxis(90, Vector3.back);
                m_EditButton.SetActive(true);
				m_CaptureButton.SetActive(false);
            }
        }

        public void OnExitJournal()
        {
            if (webCamTexture != null)
            {
                webCamTexture.Stop();

            }
        }

        public void OnDisable()
        {
            if (webCamTexture!=null)
            {
                webCamTexture.Stop();
            }
#if UNITY_IOS
            m_PhotoHolder.transform.localScale = m_InitScale;
#endif
			m_PhotoHolder.transform.rotation = m_InitRotation;

        }

        // ** Buttons Methodes ** //
        public virtual void TakePhoto()
        {
			if (webCamTexture != null) {
				Texture2D photo = new Texture2D (webCamTexture.width, webCamTexture.height);
				photo.SetPixels (webCamTexture.GetPixels ());
				photo.Apply ();

				if (webCamTexture != null) {
					webCamTexture.Stop ();
				}
  
				byte[] bytes = photo.EncodeToPNG ();
				File.WriteAllBytes (Application.persistentDataPath + "/Task3.png", bytes);

#if UNITY_IOS
				m_PhotoHolder.transform.localScale = m_InitScale;
#endif
				m_PhotoHolder.transform.rotation = m_InitRotation;
            
				byte[] test = File.ReadAllBytes (Application.persistentDataPath + "/Task3.png");
				Texture2D myTexture = new Texture2D (2, 2);
				myTexture.LoadImage (test);
			
				m_PhotoHolder.texture = myTexture;

                Vector3 v = m_PhotoHolder.transform.rotation.eulerAngles;
                m_PhotoHolder.transform.rotation = Quaternion.Euler(9, 2, -90);


                m_EditButton.SetActive (true);
            

				TaskStatus status = PlayerProfile.GetProfile ().TaskStatus.FirstOrDefault (r => r.ID == m_Task.ID);

				if (status == null) {
					PlayerProfile.GetProfile ().AddCoins (3);
					PlayerProfile.GetProfile ().RecordTaskStatus (m_Task.ID, "/Task3.png");
				}

				m_CaptureButton.SetActive (false);
				m_EditButton.SetActive (true);
			}
        }
    

        public void EditBT()
        {
			webCamTexture = new WebCamTexture ();
			if (webCamTexture != null) 
			{
				
				m_PhotoHolder.transform.rotation = m_InitRotation;
#if UNITY_IOS
			m_PhotoHolder.transform.localScale = new Vector3 (-1,1, 1);
            m_PhotoHolder.transform.rotation = m_PhotoHolder.transform.rotation * Quaternion.AngleAxis(-90, Vector3.back);
#elif UNITY_ANDROID
                m_PhotoHolder.transform.rotation = m_PhotoHolder.transform.rotation * Quaternion.AngleAxis(90, Vector3.back);
#endif

                m_PhotoHolder.texture = webCamTexture;
				webCamTexture.Play ();

				m_CaptureButton.SetActive (true);
				m_EditButton.SetActive (false);
			}
        }

    }
}
