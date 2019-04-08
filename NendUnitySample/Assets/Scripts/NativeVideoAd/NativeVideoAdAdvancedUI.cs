using UnityEngine;
using UnityEngine.UI;
using NendUnityPlugin.AD.Native;

public class NativeVideoAdAdvancedUI : NativeVideoAdAdvanced
{
	public RawImage logo;
	public Text pr;
	public Text title;
	public Text promotion;
	public Text actionButton;

	protected override void OnReceive (INativeVideoAd ad, int code, string message)
	{
		if (ad != null && ad.HasVideo) {
			title.text = ad.TitleText;
			promotion.text = ad.AdvertiserName;
			actionButton.text = ad.CallToActionText;
			StartCoroutine (ad.LoadLogoImage ((Texture2D texture) => {
				logo.texture = texture;
			}));
			ad.Activate(nativeVideoView, null);
		} else {
			Debug.LogFormat ("Failed to load ad. code = {0}, message = {1}", code, message);
		}
	}
}