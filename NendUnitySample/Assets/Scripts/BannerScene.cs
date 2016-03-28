using UnityEngine;
using UnityEngine.SceneManagement;

using NendUnityPlugin.AD;
using NendUnityPlugin.Layout;
using NendUnityPlugin.Common;

/// <summary>
/// http://fan-adn.github.io/nendSDK-Unity/html/class_nend_unity_plugin_1_1_layout_1_1_nend_ad_default_layout_builder.html
/// </summary>
public class BannerScene : BaseScene {

	private NendAdBanner banner;

	// Use this for initialization
	public override void Start () 
	{
		base.Start ();

		banner = NendUtils.GetBannerComponent ("NendAdBanner3");

		RegisterAction ("Back", delegate() {
			banner.Destroy ();
			SceneManager.LoadScene ("First");
		});
		RegisterAction ("Show", delegate() {
			ShowAd (banner);
		});
		RegisterAction ("Hide", delegate() {
			HideAd (banner);
		});
		RegisterAction ("CenterTop", delegate() {
			banner.Layout (new NendAdDefaultLayoutBuilder ().Gravity ((int)Gravity.TOP | (int)Gravity.CENTER_HORIZONTAL));
		});
		RegisterAction ("CenterBottom", delegate() {
			banner.Layout (new NendAdDefaultLayoutBuilder ().Gravity ((int)Gravity.BOTTOM | (int)Gravity.CENTER_HORIZONTAL));
		});
		RegisterAction ("UpperLeft", delegate() {
			banner.Layout (new NendAdDefaultLayoutBuilder ().Gravity ((int)Gravity.TOP | (int)Gravity.LEFT));
		});
		RegisterAction ("UpperRight", delegate() {
			banner.Layout (new NendAdDefaultLayoutBuilder ().Gravity ((int)Gravity.TOP | (int)Gravity.RIGHT));
		});
		RegisterAction ("LowerLeft", delegate() {
			banner.Layout (new NendAdDefaultLayoutBuilder ().Gravity ((int)Gravity.BOTTOM | (int)Gravity.LEFT));
		});
		RegisterAction ("LowerRight", delegate() {
			banner.Layout (new NendAdDefaultLayoutBuilder ().Gravity ((int)Gravity.BOTTOM | (int)Gravity.RIGHT));
		});
		RegisterAction ("Center", delegate() {
			banner.Layout (new NendAdDefaultLayoutBuilder ().Gravity ((int)Gravity.CENTER_HORIZONTAL | (int)Gravity.CENTER_VERTICAL));
		});
		RegisterAction ("CenterLeft", delegate() {
			banner.Layout (new NendAdDefaultLayoutBuilder ().Gravity ((int)Gravity.CENTER_VERTICAL | (int)Gravity.LEFT));
		});
		RegisterAction ("CenterRight", delegate() {
			banner.Layout (new NendAdDefaultLayoutBuilder ().Gravity ((int)Gravity.CENTER_VERTICAL | (int)Gravity.RIGHT));
		});
		RegisterAction ("UpperLeftWithClearance", delegate() {
			banner.Layout (new NendAdDefaultLayoutBuilder ()
			               .MarginLeft (16)
			               .MarginTop (16));
		});
#if UNITY_ANDROID
		RegisterAction ("UpperLeftWithClearancePx", delegate() {
			banner.Layout (new NendAdDefaultLayoutBuilder ()
			               .Unit (ComplexUnit.PX)
			               .MarginLeft (16)
			               .MarginTop (16));
		});
#endif
		RegisterAction ("LowerRightWithClearance", delegate() {
			banner.Layout (new NendAdDefaultLayoutBuilder ()
			               .Gravity ((int)Gravity.RIGHT | (int)Gravity.BOTTOM)
			               .MarginRight (16)
			               .MarginBottom (16));
		});
#if UNITY_ANDROID
		RegisterAction ("LowerRightWithClearancePx", delegate() {
			banner.Layout (new NendAdDefaultLayoutBuilder ()
			               .Gravity ((int)Gravity.RIGHT | (int)Gravity.BOTTOM)
			               .Unit (ComplexUnit.PX)
			               .MarginRight (16)
			               .MarginBottom (16));
		});
#endif
		RegisterAction ("CenterTopWithClearance", delegate() {
			banner.Layout (new NendAdDefaultLayoutBuilder ()
			               .Gravity ((int)Gravity.CENTER_HORIZONTAL | (int)Gravity.TOP)
			               .MarginTop (16));
		});
#if UNITY_ANDROID
		RegisterAction ("CenterTopWithClearancePx", delegate() {
			banner.Layout (new NendAdDefaultLayoutBuilder()
			               .Gravity ((int)Gravity.CENTER_HORIZONTAL | (int)Gravity.TOP)
			               .Unit (ComplexUnit.PX)
			               .MarginTop (16));
		});
#endif
		RegisterAction ("CenterLeftWithClearance", delegate() {
			banner.Layout (new NendAdDefaultLayoutBuilder ()
			               .Gravity ((int)Gravity.CENTER_VERTICAL | (int)Gravity.LEFT)
			               .MarginLeft (16));
		});
		RegisterAction ("CenterRightWithClearance", delegate() {
			banner.Layout (new NendAdDefaultLayoutBuilder ()
			               .Gravity ((int)Gravity.CENTER_VERTICAL | (int)Gravity.RIGHT)
			               .MarginRight (16));
		});
		RegisterAction ("CenterBottomWithClearance", delegate() {
			banner.Layout (new NendAdDefaultLayoutBuilder ()
			               .Gravity ((int)Gravity.CENTER_HORIZONTAL | (int)Gravity.BOTTOM)
			               .MarginBottom (16));
		});
#if UNITY_ANDROID
		RegisterAction ("CenterBottomWithClearancePx", delegate() {
			banner.Layout (new NendAdDefaultLayoutBuilder()
			               .Gravity ((int)Gravity.CENTER_HORIZONTAL | (int)Gravity.BOTTOM)
			               .Unit (ComplexUnit.PX)
			               .MarginBottom (16));
		});
#endif
	}
}
