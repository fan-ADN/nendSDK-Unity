using UnityEngine;

using System;
using System.Collections.Generic;

using NendUnityPlugin.AD;
using NendUnityPlugin.Common;

public class FirstScene : BaseScene
{
	private const string bannerGameObject = "NendAdBanner1";
	private const string iconGameObject = "NendAdIcon1";
	private static bool isResumeNeeded = false;
	private List<NendAd> adList = null;

	// Use this for initialization
	public override void Start ()
	{
		base.Start ();

		Debug.Log ("Start() => " + gameObject.name);

		NendAdBanner banner = NendUtils.GetBannerComponent (bannerGameObject);
		NendAdIcon icon = NendUtils.GetIconComponent (iconGameObject);
		adList = new List<NendAd> ();
		adList.Add (banner);
		adList.Add (icon);

		// attach EventHandlers
		banner.AdLoaded += OnFinishLoadBannerAd;
		banner.AdReceived += OnReceiveBannerAd;
		banner.AdFailedToReceive += OnFailToReceiveBannerAd;
		banner.AdClicked += OnClickBannerAd;
		banner.AdBacked += OnDismissScreen;

		icon.AdLoaded += OnFinishLoadIconAd;
		icon.AdReceived += OnReceiveIconAd;
		icon.AdFailedToReceive += OnFailToReceiveIconAd;
		icon.AdClicked += OnClickIconAd;

		RegisterAction ("Next", delegate() {
			LoadScene("Second");
		});
		RegisterAction ("BannerLayout", delegate() {
			LoadScene("Banner");
		});
		RegisterAction ("IconLayout", delegate() {
			LoadScene("Icon");
		});
#if UNITY_ANDROID
		RegisterAction ("Exit", delegate() {
			// Show the interstitial ad(upon completion)
			NendAdInterstitial.Instance.Finish ();
		});
#endif
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
			} else {
				NendAdIcon icon = (NendAdIcon)ad;
				icon.AdLoaded -= OnFinishLoadIconAd;
				icon.AdReceived -= OnReceiveIconAd;
				icon.AdFailedToReceive -= OnFailToReceiveIconAd;
				icon.AdClicked -= OnClickIconAd;
			}
		}
	}

	private void LoadScene (string name) {
		foreach (NendAd ad in adList) {
			HideAd (ad);
		}
		isResumeNeeded = true;
		Application.LoadLevel (name);
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
		Debug.Log (bannerGameObject + " -> OnFailToReceiveBannerAd: " + args.Message);
	}
	
	public void OnDismissScreen (object sender, EventArgs args) 
	{
		Debug.Log (bannerGameObject + " -> OnDismissScreen");
	}

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
	#endregion
}