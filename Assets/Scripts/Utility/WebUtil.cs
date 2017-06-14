using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.IO;

// Use for location data
public class LocationData
{
	// use @ at beginning to skip reserved word
	public string @as;
	public string city;
	public string country;
	public string countryCode;
	public string isp;
	public float lat;
	public float lon;
	public string org;
	public string query;
	public string region;
	public string regionName;
	public string status;
	public string timezone;
	public string zip;

	public LocationData() {
		status = "new";
	}
}

public class WebUtil
{
	private string secretKey = "Cigbreak4"; // Edit this value and make sure it's the same as the one stored on the server

	public string MD5Sum(string strToTncrypt) {
		System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding ();
		byte[] bytes = ue.GetBytes (strToTncrypt);

		// encrypt bytes
		System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
		byte[] hashBytes = md5.ComputeHash (bytes);

		// convert the encrypted bytes back to a string (base 16)
		string hashString = "";

		for (int i = 0; i < hashBytes.Length; i++) {
			hashString += System.Convert.ToString (hashBytes [i], 16).PadLeft (2, '0');
		}

		return hashString.PadLeft (32, '0');
	}

	public string CheckConnection (string resource)
	{
		string html = string.Empty;
		HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create (resource);

		try {
			using (HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse ()) {
				bool isSuccess = (int)WebResp.StatusCode < 299 && (int)WebResp.StatusCode >= 200;
				if (isSuccess) {
					using (StreamReader reader = new StreamReader (WebResp.GetResponseStream ())) {
						// We are limiting the array to 80 so we don have
						// to parse the entire html document feel free to
						// adjust (probably stay under 300)
						char[] cs = new char[80];
						reader.Read (cs, 0, cs.Length);
						foreach (char ch in cs) {
							html += ch;
						}
					}
				}
			}
		} catch {
			return "";
		}

		//
		if (SystemInfo.unsupportedIdentifier != SystemInfo.deviceUniqueIdentifier) {
			Debug.Log ("SystemInfo : " + SystemInfo.deviceUniqueIdentifier.ToString ());
			Debug.Log ("MD5Sum : " + MD5Sum(SystemInfo.deviceUniqueIdentifier + secretKey));
		} else {
			Debug.Log ("Unsupported");
		}

		return html;
	}

	public LocationData GetHtmlFromUri (string resource)
	{
		string html = string.Empty;
		LocationData ld = new LocationData();
		HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create (resource);

		try {
			using (HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse ()) {
				bool isSuccess = (int)WebResp.StatusCode < 299 && (int)WebResp.StatusCode >= 200;
				if (isSuccess) {
					using (StreamReader reader = new StreamReader (WebResp.GetResponseStream ())) {
						html = reader.ReadToEnd ();
						return JsonUtility.FromJson<LocationData> (html);
					}
				}
			}
		} catch {
			return ld;
		}

		return ld;
	}
}
