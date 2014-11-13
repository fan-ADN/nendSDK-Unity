using UnityEngine;
using System.Collections;

public class SecondBehaviourScript : MonoBehaviour {

	private const string bannerGameObject = "NendAdBanner2";
	private static bool isResumeNeeded = false;
	private NendAdBanner banner;
	private NendAdBannerCallbackImpl bannerCallback = new NendAdBannerCallbackImpl();

	void Awake () {
		UnityEngine.Debug.Log("Awake() => " + gameObject.name);
		banner = NendUtils.GetBannerComponent (bannerGameObject);
		banner.Callback = bannerCallback;

		NendAdInterstitial.Instance.Callback = new NendAdInterstitialCallbackImpl();
	}

	// Use this for initialization
	void Start () {
		UnityEngine.Debug.Log("Start() => " + gameObject.name);
		if ( isResumeNeeded ) {
			banner.Show();
			banner.Resume();
			isResumeNeeded = false;
		}

		StartCoroutine(ShowAdDelay());
	}
	
	// Update is called once per frame
	void Update () {
		if (Application.platform == RuntimePlatform.Android && Input.GetKey(KeyCode.Escape)) {
			GoBack();
		}
	}
	
	void OnGUI () {
		GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
		GUILayout.FlexibleSpace();
		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		if ( GUILayout.Button ("BACK", GUILayout.MinWidth (200), GUILayout.MinHeight (150)) ) {
			GoBack();
		}
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();
		GUILayout.FlexibleSpace();
		GUILayout.EndArea();
	}

	private IEnumerator ShowAdDelay() {
		yield return new WaitForSeconds(0.5f);
		NendAdInterstitial.Instance.Show();
	}

	private void GoBack() {
		banner.Hide();
		banner.Pause();
		isResumeNeeded = true;
		Application.LoadLevel("First");
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