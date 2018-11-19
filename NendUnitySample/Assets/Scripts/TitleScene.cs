using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

using NendUnityPlugin.AD;
using NendUnityPlugin.Common;

public class TitleScene : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{
        NendAdLogger.LogLevel = NendAdLogger.NendAdLogLevel.Debug;
        // attach EventHandler
        NendAdInterstitial.Instance.AdLoaded += OnFinishLoadInterstitialAd;
		StartCoroutine (LoadAd ());
	}

	// Update is called once per frame
	void Update ()
	{
		if (Application.platform == RuntimePlatform.Android && Input.GetKey (KeyCode.Escape)) {
			Application.Quit ();
		}
	}

	void OnDestroy ()
	{
		Debug.Log ("OnDestroy() => " + gameObject.name);
		// detach EventHandler
		NendAdInterstitial.Instance.AdLoaded -= OnFinishLoadInterstitialAd;
	}

	private IEnumerator LoadAd ()
	{
		string apiKey = "";
		string spotId = "";

#if UNITY_IOS
		apiKey = "308c2499c75c4a192f03c02b2fcebd16dcb45cc9";
		spotId = "213208";
		Handheld.SetActivityIndicatorStyle(UnityEngine.iOS.ActivityIndicatorStyle.Gray);
#elif UNITY_ANDROID
		apiKey = "8c278673ac6f676dae60a1f56d16dad122e23516";
		spotId = "213206";
		Handheld.SetActivityIndicatorStyle(AndroidActivityIndicatorStyle.Small);
#endif

		Handheld.StartActivityIndicator ();
		yield return new WaitForSeconds (0.0f);

		NendAdInterstitial.Instance.Load (apiKey, spotId);
	}

	private void ReloadAd ()
	{
		StartCoroutine (LoadAdDelay ());
	}

	private IEnumerator LoadAdDelay ()
	{
		yield return new WaitForSeconds (3.0f);
		StartCoroutine (LoadAd ());
	}

	public void OnFinishLoadInterstitialAd (object sender, NendAdInterstitialLoadEventArgs args) 
	{
		Handheld.StopActivityIndicator ();

		NendAdInterstitialStatusCode statusCode = args.StatusCode;
		switch (statusCode) {
		case NendAdInterstitialStatusCode.SUCCESS:
			// Move to the next scene when Interstitial-AD load completed.
			Debug.Log (">> OnFinishLoadInterstitialAd: SUCCESS");
			SceneManager.LoadScene ("First");
			break;
		default:
			// When failed to get of Interstitial-AD, try to reload.
			Debug.Log (">> OnFinishLoadInterstitialAd: FAILURE");
			ReloadAd ();
			break;
		}
	}
}