using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using CigBreak;
using UnityEngine.SceneManagement;

/// <summary>
/// Debug settings panel for adjusting in game values
/// </summary>
public class SettingsPanel : MonoBehaviour
{
    [SerializeField]
    private Button settingsButton = null;
    [SerializeField]
    private GameObject settingsPanel = null;

    [SerializeField]
    private Slider packSizeSlider = null;
    [SerializeField]
    private Slider objectSizeSlider = null;

    [SerializeField]
    private Text packSizeValue = null;
    [SerializeField]
    private Text objectSizeValue = null;

    [SerializeField]
    private GameObject pack = null;

    private Gameplay gameplay = null;
    private ObjectFactory factory = null;

    private void Start()
    {
        gameplay = Gameplay.gameplay;
        factory = ObjectFactory.objectFactory;
        settingsButton.onClick.AddListener(ToggleSettings);
        packSizeSlider.onValueChanged.AddListener(AdjustPackSize);
        objectSizeSlider.onValueChanged.AddListener(AdjustObjectSize);
    }

    private void ToggleSettings()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
        gameplay.TogglePause(settingsPanel.activeSelf);
    }

    private void AdjustPackSize(float value)
    {
        foreach (Transform t in pack.transform)
        {
           t.localScale = new Vector3(value, value, 1f);
        }
        packSizeValue.text = value.ToString();
    }

    private void AdjustObjectSize(float value)
    {
        factory.UpdateObjectScale(value);
        objectSizeValue.text = value.ToString();
    }

    public void ReturnToMap()
    {
        SceneManager.LoadScene(CigBreakConstants.SceneNames.MapScreen);        
        
    }

}
