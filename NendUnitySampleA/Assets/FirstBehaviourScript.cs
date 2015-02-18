using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FirstBehaviourScript : MonoBehaviour {

	private const string bannerGameObject = "NendAdBanner1";
	private const string iconGameObject = "NendAdIcon1";
	private static bool isResumeNeeded = false;
	private List<NendAd> adList = null;
	private NendAdBannerCallbackImpl bannerCallback = new NendAdBannerCallbackImpl();
	private NendAdIconCallbackImpl iconCallback = new NendAdIconCallbackImpl();

	void Awake () {
		UnityEngine.Debug.Log("Awake() => " + gameObject.name);
		NendAdBanner banner = NendUtils.GetBannerComponent (bannerGameObject);
		NendAdIcon icon = NendUtils.GetIconComponent (iconGameObject);
		banner.Callback = bannerCallback;
		icon.Callback = iconCallback;
		adList = new List<NendAd>();
		adList.Add(banner);
		adList.Add(icon);

		NendAdInterstitial.Instance.Callback = new NendAdInterstitialCallbackImpl ();
	}

	// Use this for initialization
	void Start () {
		UnityEngine.Debug.Log("Start() => " + gameObject.name);
		if ( isResumeNeeded ) {
			foreach ( NendAd ad in adList ) {
				ad.Show();
				ad.Resume();
			}
			isResumeNeeded = false;
		}
	}
	
	// Update is called once per frame
	void Update () {}
	
	void OnGUI () {
		GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
		GUILayout.FlexibleSpace();
		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		if ( GUILayout.Button ("NEXT", GUILayout.MinWidth (200), GUILayout.MinHeight (150)) ) {
			foreach ( NendAd ad in adList ) {
				ad.Hide();
				ad.Pause();
			}
			isResumeNeeded = true;
			Application.LoadLevel("Second");
		}
		if ( GUILayout.Button ("EXIT", GUILayout.MinWidth (200), GUILayout.MinHeight (150)) ) {
			// Show the Interstitial-AD(upon completion)
			NendAdInterstitial.Instance.Finish();
		}
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();
		GUILayout.FlexibleSpace();
		GUILayout.EndArea();
	}

	private class NendAdBannerCallbackImpl : NendAdBannerCallback
	{
		public void OnFinishLoadBanner ()
		{
			UnityEngine.Debug.Log (bannerGameObject + " -> OnFinishLoadBanner()");
		}

		public void OnClickBannerAd ()
		{
			UnityEngine.Debug.Log (bannerGameObject + " -> OnClickBannerAd()");
		}

		public void OnReceiveBannerAd ()
		{
			UnityEngine.Debug.Log (bannerGameObject + " -> OnReceiveBannerAd()");
		}

		public void OnFailToReceiveBannerAd (NendErrorCode errorCode, string message)
		{
			UnityEngine.Debug.Log (bannerGameObject + " -> OnFailToReceiveBannerAd()");
		}

		public void OnDismissScreen ()
		{
			UnityEngine.Debug.Log (bannerGameObject + " -> OnDismissScreen()");
		}
	}

	private class NendAdIconCallbackImpl : NendAdIconCallback
	{
		public void OnFinishLoadIcon ()
		{
			UnityEngine.Debug.Log (iconGameObject + " -> OnFinishLoadIcon()");
		}

		public void OnClickIconAd ()
		{
			UnityEngine.Debug.Log (iconGameObject + " -> OnClickIconAd()");
		}

		public void OnReceiveIconAd ()
		{
			UnityEngine.Debug.Log (iconGameObject + " -> OnReceiveIconAd()");
		}

		public void OnFailToReceiveIconAd (NendErrorCode errorCode, string message)
		{
			UnityEngine.Debug.Log (iconGameObject + " -> OnFailToReceiveIconAd()");
		}
	}

	private class NendAdInterstitialCallbackImpl : NendAdInterstitialCallback
	{
		public void OnFinishLoadInterstitialAd (NendAdInterstitialStatusCode statusCode)
		{
			switch (statusCode) {
			case NendAdInterstitialStatusCode.SUCCESS:
				UnityEngine.Debug.Log(">> OnFinishLoadInterstitialAd: SUCCESS");
				break;
			case NendAdInterstitialStatusCode.INVALID_RESPONSE_TYPE:
				UnityEngine.Debug.Log(">> OnFinishLoadInterstitialAd: INVALID_RESPONSE_TYPE");
				break;
			case NendAdInterstitialStatusCode.FAILED_AD_REQUEST:
				UnityEngine.Debug.Log(">> OnFinishLoadInterstitialAd: FAILED_AD_REQUEST");
				break;
			case NendAdInterstitialStatusCode.FAILED_AD_DOWNLOAD:
				UnityEngine.Debug.Log(">> OnFinishLoadInterstitialAd: FAILED_AD_DOWNLOAD");
				break;
			}
		}

		public void OnClickInterstitialAd (NendAdInterstitialClickType clickType)
		{
			switch (clickType) {
			case NendAdInterstitialClickType.DOWNLOAD:
				UnityEngine.Debug.Log(">> OnClickInterstitialAd: DOWNLOAD");
				break;
			case NendAdInterstitialClickType.CLOSE:
				UnityEngine.Debug.Log(">> OnClickInterstitialAd: CLOSE");
				break;
			case NendAdInterstitialClickType.EXIT:
				UnityEngine.Debug.Log(">> OnClickInterstitialAd: EXIT");
				break;
			}
		}

		public void OnShowInterstitialAd (NendAdInterstitialShowResult showResult) {
			switch ( showResult ) {
			case NendAdInterstitialShowResult.AD_SHOW_SUCCESS:
				UnityEngine.Debug.Log(">> OnShowInterstitialAd: AD_SHOW_SUCCESS");
				break;
			case NendAdInterstitialShowResult.AD_SHOW_ALREADY:
				UnityEngine.Debug.Log(">> OnShowInterstitialAd: AD_SHOW_ALREADY");
				break;
			case NendAdInterstitialShowResult.AD_REQUEST_INCOMPLETE:
				UnityEngine.Debug.Log(">> OnShowInterstitialAd: AD_REQUEST_INCOMPLETE");
				break;
			case NendAdInterstitialShowResult.AD_LOAD_INCOMPLETE:
				UnityEngine.Debug.Log(">> OnShowInterstitialAd: AD_LOAD_INCOMPLETE");
				break;
			case NendAdInterstitialShowResult.AD_FREQUENCY_NOT_RECHABLE:
				UnityEngine.Debug.Log(">> OnShowInterstitialAd: AD_FREQUENCY_NOT_RECHABLE");
				break;
			case NendAdInterstitialShowResult.AD_DOWNLOAD_INCOMPLETE:
				UnityEngine.Debug.Log(">> OnShowInterstitialAd: AD_DOWNLOAD_INCOMPLETE");
				break;
			}		
		}
	}
}