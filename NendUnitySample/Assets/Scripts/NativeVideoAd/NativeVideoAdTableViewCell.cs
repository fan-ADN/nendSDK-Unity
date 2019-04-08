using UnityEngine;
using NendUnityPlugin.AD.Native;

public class NativeVideoAdTableViewCell : TableViewCell
{
	public NendAdNative nativeAd;
	public NendAdNativeView adView;

	public override void UpdateContent (int index)
	{
		if (!adView.Loaded) {
			adView.ViewTag = index;
			nativeAd.RegisterAdView (adView);
			nativeAd.LoadAd (index);
		}
	}

	#region AdEvents

	public void OnShown (NendAdNativeView view)
	{
		Debug.Log ("OnShown: " + view.ViewTag);
	}

	public void OnClick (NendAdNativeView view)
	{
		Debug.Log ("OnClick: " + view.ViewTag);
	}

	#endregion
}