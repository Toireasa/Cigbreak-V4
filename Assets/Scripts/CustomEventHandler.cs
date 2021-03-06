using UnityEngine;
using System.Collections;

public class CustomEventHandler : MonoBehaviour {

    public static CustomEventHandler Instance;
    // Use this for initialization

    void Awake()
    {
        if (Instance)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
    }

    void Start ()
    {
        DontDestroyOnLoad(gameObject);

		Debug.Log ("555");
		StartCoroutine (LS());
		//
		WebUtil wb = new WebUtil();
		string HtmlText = wb.CheckConnection ("http://google.com");
		if(HtmlText == "")
		{
			// No connection
			Debug.Log("No connection.");
		}
		else if (!HtmlText.Contains("schema.org/WebPage"))
		{
			// Redirecting since the begining of googles html contains that
			// pharse and it was not found
			Debug.Log("Redirecting.");
		}
		else
		{
			// Success
			Debug.Log("Connection Success.");
			LocationData ld = wb.GetHtmlFromUri ("http://ip-api.com/json");
			Debug.Log ("lat = " + ld.lat + "; lon = " + ld.lon);
			Debug.Log ("Test : " + Input.location.lastData.latitude);
		}
    }

	IEnumerator LS()
	{
		// First, check if user has location service enabled
		if (!Input.location.isEnabledByUser)
			yield break;

		// Start service before querying location
		Input.location.Start();

		// Wait until service initializes
		int maxWait = 20;
		while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
		{
			yield return new WaitForSeconds(1);
			maxWait--;
		}

		// Service didn't initialize in 20 seconds
		if (maxWait < 1)
		{
			Debug.Log ("Timed out");
			yield break;
		}

		// Connection has failed
		if (Input.location.status == LocationServiceStatus.Failed)
		{
			Debug.Log ("Unable to determine device location");
			yield break;
		}
		else
		{
			// Access granted and location value could be retrieved
			Debug.Log ("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
		}

		// Stop service if there is no need to query location updates continuously
		Input.location.Stop();
	}

	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

    }
}
