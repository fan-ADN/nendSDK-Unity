using UnityEngine;
using System.Collections;

public class TitleBehaviourScript : MonoBehaviour {
	
	void Awake () {
		NendAdInterstitial.Instance.Callback = new NendAdInterstitialCallbackImpl(this);
	}

	// Use this for initialization
	void Start () {
		StartCoroutine(LoadAd());
	}
	
	// Update is called once per frame
	void Update () {
		if (Application.platform == RuntimePlatform.Android && Input.GetKey(KeyCode.Escape)) {
			Application.Quit();
		}
	}

	private IEnumerator LoadAd() {
		string apiKey = "";
		string spotId = "";

#if UNITY_IPHONE
		apiKey = "308c2499c75c4a192f03c02b2fcebd16dcb45cc9";
		spotId = "213208";
		Handheld.SetActivityIndicatorStyle(iOSActivityIndicatorStyle.Gray);
#elif UNITY_ANDROID
		apiKey = "8c278673ac6f676dae60a1f56d16dad122e23516";
		spotId = "213206";
		Handheld.SetActivityIndicatorStyle(AndroidActivityIndicatorStyle.Small);
#endif

		Handheld.StartActivityIndicator();
		yield return new WaitForSeconds(0.0f);

		NendAdInterstitial.Instance.Load(apiKey, spotId);
	}

	private void ReloadAd() {
		StartCoroutine(LoadAdDelay());
	}

	private IEnumerator LoadAdDelay() {
		yield return new WaitForSeconds(3.0f);
		StartCoroutine(LoadAd());
	}

	private class NendAdInterstitialCallbackImpl : NendAdInterstitialCallback
	{
		private TitleBehaviourScript monoBehaviour;

		public NendAdInterstitialCallbackImpl(TitleBehaviourScript monoBehaviour) {
			this.monoBehaviour = monoBehaviour;
		}

		public void OnFinishLoadInterstitialAd (NendAdInterstitialStatusCode statusCode)
		{
			Handheld.StopActivityIndicator();

			switch (statusCode) {
			case NendAdInterstitialStatusCode.SUCCESS:
				// Move to the next scene when Interstitial-AD load completed.
				UnityEngine.Debug.Log(">> OnFinishLoadInterstitialAd: SUCCESS");
				Application.LoadLevel("First");
				break;
			default:
				// When failed to get of Interstitial-AD, try to reload.
				UnityEngine.Debug.Log(">> OnFinishLoadInterstitialAd: FAILURE");
				monoBehaviour.ReloadAd();
				break;
			}
		}
		
		public void OnClickInterstitialAd (NendAdInterstitialClickType clickType) {
			// nop
		}

		public void OnShowInterstitialAd (NendAdInterstitialShowResult showResult) {
			// nop
		}
	}
}
