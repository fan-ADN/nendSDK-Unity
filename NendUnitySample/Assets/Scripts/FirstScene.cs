using UnityEngine;
using UnityEngine.SceneManagement;

using System;
using System.Collections.Generic;

using NendUnityPlugin.AD;
using NendUnityPlugin.Common;

public class FirstScene : BaseScene
{
	private const string bannerGameObject = "NendAdBanner1";
#if UNITY_ANDROID
	private const string iconGameObject = "NendAdIcon1";
#endif
	private static bool isResumeNeeded = false;
	private List<NendAd> adList = null;

	// Use this for initialization
	public override void Start ()
	{
		base.Start ();

		Debug.Log ("Start() => " + gameObject.name);

		adList = new List<NendAd> ();

		NendAdBanner banner = NendUtils.GetBannerComponent (bannerGameObject);
		adList.Add (banner);

		banner.AdLoaded += OnFinishLoadBannerAd;
		banner.AdReceived += OnReceiveBannerAd;
		banner.AdFailedToReceive += OnFailToReceiveBannerAd;
		banner.AdClicked += OnClickBannerAd;
		banner.AdBacked += OnDismissScreen;
		banner.InformationClicked += OnClickBannerInformation;

#if UNITY_ANDROID
		NendAdIcon icon = NendUtils.GetIconComponent (iconGameObject);
		adList.Add (icon);

		icon.AdLoaded += OnFinishLoadIconAd;
		icon.AdReceived += OnReceiveIconAd;
		icon.AdFailedToReceive += OnFailToReceiveIconAd;
		icon.AdClicked += OnClickIconAd;
		icon.InformationClicked += OnClickIconInformation;
#endif
	
		RegisterAction ("Next", delegate() {
			LoadScene("Second");
		});
		RegisterAction ("BannerLayout", delegate() {
			LoadScene("Banner");
		});
#if UNITY_ANDROID
		RegisterAction ("IconLayout", delegate() {
			LoadScene("Icon");
		});
#endif
		RegisterAction ("NativeAd", delegate() {
			LoadScene("Menu");
		});
		RegisterAction ("FullBoard", delegate() {
			LoadScene("FullBoard");
		});
		RegisterAction ("Video", delegate() {
			LoadScene("Video");
		});
		RegisterAction ("Quit", delegate() {
			Application.Quit ();
		});

		if (isResumeNeeded) {
			foreach (NendAd ad in adList) {
				ShowAd (ad);
			}
			isResumeNeeded = false;
		}
	}

	void OnDestroy ()
	{
		Debug.Log ("OnDestroy() => " + gameObject.name);

		// detach EventHandlers
		foreach (NendAd ad in adList) {
			if (ad is NendAdBanner) {
				NendAdBanner banner = (NendAdBanner)ad;
				banner.AdLoaded -= OnFinishLoadBannerAd;
				banner.AdReceived -= OnReceiveBannerAd;
				banner.AdFailedToReceive -= OnFailToReceiveBannerAd;
				banner.AdClicked -= OnClickBannerAd;
				banner.AdBacked -= OnDismissScreen;
				banner.InformationClicked -= OnClickBannerInformation;
			} else {
#if UNITY_ANDROID
				NendAdIcon icon = (NendAdIcon)ad;
				icon.AdLoaded -= OnFinishLoadIconAd;
				icon.AdReceived -= OnReceiveIconAd;
				icon.AdFailedToReceive -= OnFailToReceiveIconAd;
				icon.AdClicked -= OnClickIconAd;
				icon.InformationClicked -= OnClickIconInformation;
#endif
			}
		}
	}

	private void LoadScene (string name) {
		foreach (NendAd ad in adList) {
			HideAd (ad);
		}
		isResumeNeeded = true;
		SceneManager.LoadScene (name);
	}

	#region EventHandlers
	public void OnFinishLoadBannerAd (object sender, EventArgs args) 
	{
		Debug.Log (bannerGameObject + " -> OnFinishLoadBannerAd");
	}
	
	public void OnClickBannerAd (object sender, EventArgs args) 
	{
		Debug.Log (bannerGameObject + " -> OnClickBannerAd");
	}
	
	public void OnReceiveBannerAd (object sender, EventArgs args) 
	{
		Debug.Log (bannerGameObject + " -> OnReceiveBannerAd");
	}
	
	public void OnFailToReceiveBannerAd (object sender, NendAdErrorEventArgs args) 
	{
		Debug.Log (bannerGameObject + " -> OnFailToReceiveBannerAd: " + args.ErrorCode.ToString () + ", " + args.Message);
	}
	
	public void OnDismissScreen (object sender, EventArgs args) 
	{
		Debug.Log (bannerGameObject + " -> OnDismissScreen");
	}

	public void OnClickBannerInformation (object sender, EventArgs args) 
	{
		Debug.Log (bannerGameObject + " -> OnClickBannerInformation");
	}

#if UNITY_ANDROID
	public void OnFinishLoadIconAd (object sender, EventArgs args) 
	{
		Debug.Log (iconGameObject + " -> OnFinishLoadIconAd");
	}

	public void OnClickIconAd (object sender, EventArgs args) 
	{
		Debug.Log (iconGameObject + " -> OnClickIconAd");
	}

	public void OnReceiveIconAd (object sender, EventArgs args) 
	{
		Debug.Log (iconGameObject + " -> OnReceiveIconAd");
	}

	public void OnFailToReceiveIconAd (object sender, NendAdErrorEventArgs args) 
	{
		Debug.Log (iconGameObject + " -> OnFailToReceiveIconAd: " + args.Message);
	}

	public void OnClickIconInformation (object sender, EventArgs args) 
	{
		Debug.Log (iconGameObject + " -> OnClickIconInformation");
	}
#endif
	#endregion
}