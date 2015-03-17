using UnityEngine;
using System.Collections;

using NendUnityPlugin.AD;
using NendUnityPlugin.Common;

public class SecondScene : BaseScene
{
	private const string bannerGameObject = "NendAdBanner2";
	private static bool isResumeNeeded = false;
	private NendAdBanner banner;

	void Awake ()
	{
		UnityEngine.Debug.Log ("Awake() => " + gameObject.name);
		banner = NendUtils.GetBannerComponent (bannerGameObject);

		// attach EventHandlers
		NendAdInterstitial.Instance.AdLoaded += OnFinishLoadInterstitialAd;
		NendAdInterstitial.Instance.AdShown += OnShowInterstitialAd;
		NendAdInterstitial.Instance.AdClicked += OnClickInterstitialAd;
	}

	// Use this for initialization
	public override void Start ()
	{
		UnityEngine.Debug.Log ("Start() => " + gameObject.name);
		if (isResumeNeeded) {
			ShowAd (banner);
			isResumeNeeded = false;
		}

		StartCoroutine (ShowAdDelay ());
	}

	// Update is called once per frame
	public override void Update ()
	{
		if (Application.platform == RuntimePlatform.Android && Input.GetKey (KeyCode.Escape)) {
			GoBack ();
		}
	}

	void OnGUI ()
	{
		GUILayout.BeginArea (new Rect (0, 0, Screen.width, Screen.height));
		GUILayout.FlexibleSpace ();
		GUILayout.BeginHorizontal ();
		GUILayout.FlexibleSpace ();
		if (GUILayout.Button ("BACK", GUILayout.MinWidth (200), GUILayout.MinHeight (150))) {
			GoBack ();
		}
		GUILayout.FlexibleSpace ();
		GUILayout.EndHorizontal ();
		GUILayout.FlexibleSpace ();
		GUILayout.EndArea ();
	}

	void OnDestroy ()
	{
		Debug.Log ("OnDestroy() => " + gameObject.name);
		// detach EventHandlers
		NendAdInterstitial.Instance.AdLoaded -= OnFinishLoadInterstitialAd;
		NendAdInterstitial.Instance.AdShown -= OnShowInterstitialAd;
		NendAdInterstitial.Instance.AdClicked -= OnClickInterstitialAd;
	}

	private IEnumerator ShowAdDelay ()
	{
		yield return new WaitForSeconds (0.5f);
#if UNITY_IPHONE
		NendAdInterstitial.Instance.Show("213208");
#elif UNITY_ANDROID
		NendAdInterstitial.Instance.Show("213206");
#endif
	}

	private void GoBack ()
	{
		HideAd (banner);
		isResumeNeeded = true;
		Application.LoadLevel ("First");
	}

	#region EventHandlers
	public void OnFinishLoadInterstitialAd (object sender, NendAdInterstitialLoadEventArgs args) 
	{
		switch (args.StatusCode) {
		case NendAdInterstitialStatusCode.SUCCESS:
			Debug.Log (">> OnFinishLoadInterstitialAd: SUCCESS");
			break;
		case NendAdInterstitialStatusCode.INVALID_RESPONSE_TYPE:
			Debug.Log (">> OnFinishLoadInterstitialAd: INVALID_RESPONSE_TYPE");
			break;
		case NendAdInterstitialStatusCode.FAILED_AD_REQUEST:
			Debug.Log (">> OnFinishLoadInterstitialAd: FAILED_AD_REQUEST");
			break;
		case NendAdInterstitialStatusCode.FAILED_AD_DOWNLOAD:
			Debug.Log (">> OnFinishLoadInterstitialAd: FAILED_AD_DOWNLOAD");
			break;
		}
	}
	
	public void OnClickInterstitialAd (object sender, NendAdInterstitialClickEventArgs args) 
	{
		switch (args.ClickType) {
		case NendAdInterstitialClickType.DOWNLOAD:
			Debug.Log (">> OnClickInterstitialAd: DOWNLOAD");
			break;
		case NendAdInterstitialClickType.CLOSE:
			Debug.Log (">> OnClickInterstitialAd: CLOSE");
			break;
		case NendAdInterstitialClickType.EXIT:
			Debug.Log (">> OnClickInterstitialAd: EXIT");
			break;
		}
	}
	
	public void OnShowInterstitialAd (object sender, NendAdInterstitialShowEventArgs args) 
	{
		switch (args.ShowResult) {
		case NendAdInterstitialShowResult.AD_SHOW_SUCCESS:
			Debug.Log (">> OnShowInterstitialAd: AD_SHOW_SUCCESS");
			break;
		case NendAdInterstitialShowResult.AD_SHOW_ALREADY:
			Debug.Log (">> OnShowInterstitialAd: AD_SHOW_ALREADY");
			break;
		case NendAdInterstitialShowResult.AD_REQUEST_INCOMPLETE:
			Debug.Log (">> OnShowInterstitialAd: AD_REQUEST_INCOMPLETE");
			break;
		case NendAdInterstitialShowResult.AD_LOAD_INCOMPLETE:
			Debug.Log (">> OnShowInterstitialAd: AD_LOAD_INCOMPLETE");
			break;
		case NendAdInterstitialShowResult.AD_FREQUENCY_NOT_RECHABLE:
			Debug.Log (">> OnShowInterstitialAd: AD_FREQUENCY_NOT_RECHABLE");
			break;
		case NendAdInterstitialShowResult.AD_DOWNLOAD_INCOMPLETE:
			Debug.Log (">> OnShowInterstitialAd: AD_DOWNLOAD_INCOMPLETE");
			break;
		}
	}
	#endregion
}