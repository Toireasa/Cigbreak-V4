using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using CigBreak;

public class SplashFade : MonoBehaviour {

	// 
	public Image splashLogo;
	// 
	public Image splashPersonSmoking;
	//
	public GameObject introPanel;

	private AudioSource m_SoundEffectSource = null;

	[SerializeField]
	private AudioClip m_ClickSound = null;        

	[SerializeField]
	BackEndController m_BackEndController = null;

	void Awake()
	{
		m_SoundEffectSource = this.GetComponentInParent<AudioSource>();
	}

	// using co-routine
	IEnumerator Start()
	{
		splashLogo.canvasRenderer.SetAlpha (0.0f);
		splashPersonSmoking.canvasRenderer.SetAlpha (1.0f);

		FadeIn ();

		yield return new WaitForSeconds (1.5f);

		FadeOut ();

		yield return new WaitForSeconds (1.5f);

		introPanel.SetActive (true);
	}

	void FadeIn()
	{
		splashPersonSmoking.CrossFadeAlpha (0.0f, 0.25f, false);
		splashLogo.CrossFadeAlpha (1.0f, 0.5f, false);
	}

	void FadeOut()
	{
		splashLogo.CrossFadeAlpha (0.0f, 0.5f, false);
		splashPersonSmoking.CrossFadeAlpha (1.0f, 0.5f, false);
	}

	public void PlaySound()
	{
		m_SoundEffectSource.PlayOneShot(m_ClickSound);
	}

	public void Map()
	{
		LoadingScreen.LoadScene(CigBreakConstants.SceneNames.MapScreen);
	}
}
