using UnityEngine;
using System.Collections;
using CigBreak;
using System.Linq;

public class SceneCanvas : MonoBehaviour
{
    [SerializeField]
    string SceneName = null;

	// Use this for initialization
	void Start ()
    {

        if (PlayerPrefs.GetString(SceneName) != string.Empty)
        {            
            gameObject.SetActive(false);
        }
        else if (SceneName =="Market")
        {
            if (PlayerProfile.GetProfile().GetUnlockedVeg().Sum(v => v.Value) >= 4)
            {
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

    }
	
}
