using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField]
    Text m_Text = null;

    private static string LevelToLoad { get; set; }

    private void Start()
    {
        SceneManager.LoadSceneAsync(LevelToLoad);
    }

    float timeLeft = 4.0f;

    void Update()
    {
        timeLeft -= Time.deltaTime*50;

        if (timeLeft < 3)
        {
            m_Text.text = "Loading..";
        }
        else if (timeLeft < 2)
        {
            m_Text.text = "Loading...";
        }
        else if(timeLeft < 1)
        {
            m_Text.text = "Loading.";
            timeLeft = 3;
        }
    }

    public static void LoadScene(string levelName)
    {
        LevelToLoad = levelName;
        SceneManager.LoadSceneAsync(CigBreakConstants.SceneNames.LoadingScreen);
    }

    public static void LoadLevel()
    {
        LoadScene(CigBreakConstants.SceneNames.Level);
    }

}
