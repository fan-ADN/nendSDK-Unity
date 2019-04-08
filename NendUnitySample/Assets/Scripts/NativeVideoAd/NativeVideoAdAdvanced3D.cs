using UnityEngine;
using NendUnityPlugin.AD.Native;

public class NativeVideoAdAdvanced3D : NativeAdAdvanced
{
	public TextMesh title;
	public TextMesh pr;
	public SpriteRenderer render;

	protected override void OnReceive (INativeAd ad, int code, string message)
	{
		if (null != ad) {
			pr.text = ad.GetAdvertisingExplicitlyText (AdvertisingExplicitly.AD);
			var text = ad.ShortText;
			title.text = text.Insert (text.Length / 2, "\n");
			StartCoroutine (ad.LoadAdImage ((Texture2D texture) => {
				render.sprite = Sprite.Create (texture, new Rect (0.0f, 0.0f, texture.width, texture.height), new Vector2 (0.5f, 0.5f));
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
