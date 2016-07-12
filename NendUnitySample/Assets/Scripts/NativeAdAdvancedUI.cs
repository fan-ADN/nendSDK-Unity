using UnityEngine;
using UnityEngine.UI;
using NendUnityPlugin.AD.Native;

public class NativeAdAdvancedUI : NativeAdAdvanced
{
	public RawImage image;
	public RawImage logo;
	public Text pr;
	public Text title;
	public Text promotion;
	public Text actionButton;

	protected override void OnReceive (INativeAd ad, int code, string message)
	{
		if (null != ad) {
			pr.text = ad.GetAdvertisingExplicitlyText (AdvertisingExplicitly.Sponsored);
			title.text = ad.ShortText;
			promotion.text = ad.PromotionName;
			actionButton.text = ad.ActionButtonText;
			StartCoroutine (ad.LoadAdImage ((Texture2D texture) => {
				image.texture = texture;
			}));
			StartCoroutine (ad.LoadLogoImage ((Texture2D texture) => {
				logo.texture = texture;
			}));
			ad.Activate (this.gameObject, pr.gameObject);
			ad.AdClicked += (sender, e) => {
				Debug.Log ("Click AD.");
			};
		} else {
			Debug.LogFormat ("Failed to load ad. code = {0}, message = {1}", code, message);
		}
	}
}