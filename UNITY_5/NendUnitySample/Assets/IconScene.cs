#if UNITY_ANDROID
using UnityEngine;
using UnityEngine.SceneManagement;

using NendUnityPlugin.AD;
using NendUnityPlugin.Layout;
using NendUnityPlugin.Common;

/// <summary>
/// http://fan-adn.github.io/nendSDK-Unity/html/class_nend_unity_plugin_1_1_layout_1_1_nend_ad_icon_layout_builder.html
/// </summary>
public class IconScene : BaseScene
{
	private NendAdIcon icon;

	// Use this for initialization
	public override void Start ()
	{
		base.Start ();

		icon = NendUtils.GetIconComponent ("NendAdIcon2");

		RegisterAction ("Back", delegate() {
			icon.Destroy ();
			SceneManager.LoadScene ("First");
		});
		RegisterAction ("Show", delegate() {
			ShowAd (icon);
		});
		RegisterAction ("Hide", delegate() {
			HideAd (icon);
		});
		RegisterAction ("CenterTop", delegate() {
			icon.Layout (new NendAdIconLayoutBuilder ()
			             .Orientation (Orientation.VERTICAL)
			             .Gravity ((int)Gravity.TOP | (int)Gravity.CENTER_HORIZONTAL));
		});
		RegisterAction ("CenterBottom", delegate() {
			icon.Layout (new NendAdIconLayoutBuilder ()
			             .Orientation (Orientation.VERTICAL)
			             .Gravity ((int)Gravity.BOTTOM | (int)Gravity.CENTER_HORIZONTAL));
		});
		RegisterAction ("UpperLeft", delegate() {
			icon.Layout (new NendAdIconLayoutBuilder ()
			             .Orientation (Orientation.VERTICAL)
			             .Gravity ((int)Gravity.TOP | (int)Gravity.LEFT));
		});
		RegisterAction ("UpperRight", delegate() {
			icon.Layout (new NendAdIconLayoutBuilder ()
			             .Orientation (Orientation.VERTICAL)
			             .Gravity ((int)Gravity.TOP | (int)Gravity.RIGHT));
		});
		RegisterAction ("LowerLeft", delegate() {
			icon.Layout (new NendAdIconLayoutBuilder ()
			             .Orientation (Orientation.VERTICAL)
			             .Gravity ((int)Gravity.BOTTOM | (int)Gravity.LEFT));
		});
		RegisterAction ("LowerRight", delegate() {
			icon.Layout (new NendAdIconLayoutBuilder ()
			             .Orientation (Orientation.VERTICAL)
			             .Gravity ((int)Gravity.BOTTOM | (int)Gravity.RIGHT));
		});
		RegisterAction ("Center", delegate() {
			icon.Layout (new NendAdIconLayoutBuilder ()
			             .Orientation (Orientation.VERTICAL)
			             .Gravity ((int)Gravity.CENTER_HORIZONTAL | (int)Gravity.CENTER_VERTICAL));
		});
		RegisterAction ("CenterLeft", delegate() {
			icon.Layout (new NendAdIconLayoutBuilder ()
			             .Orientation (Orientation.VERTICAL)
			             .Gravity ((int)Gravity.LEFT | (int)Gravity.CENTER_VERTICAL));
		});
		RegisterAction ("CenterRight", delegate() {
			icon.Layout (new NendAdIconLayoutBuilder ()
			             .Orientation (Orientation.VERTICAL)
			             .Gravity ((int)Gravity.RIGHT | (int)Gravity.CENTER_VERTICAL));
		});
		RegisterAction ("UpperLeftWithClearance", delegate() {
			icon.Layout (new NendAdIconLayoutBuilder ()
			             .Orientation (Orientation.VERTICAL)
			             .MarginLeft (16)
			             .MarginTop (16));
		});
		RegisterAction ("UpperLeftWithClearancePx", delegate() {
			icon.Layout (new NendAdIconLayoutBuilder ()
			             .Orientation (Orientation.VERTICAL)
			             .MarginLeft (16)
			             .MarginTop (16)
			             .Unit (ComplexUnit.PX));
		});
		RegisterAction ("LowerRightWithClearance", delegate() {
			icon.Layout (new NendAdIconLayoutBuilder ()
			             .Orientation (Orientation.VERTICAL)
			             .Gravity ((int)Gravity.RIGHT | (int)Gravity.BOTTOM)
			             .MarginRight (16)
                         .MarginBottom (16));
		});
		RegisterAction ("LowerRightWithClearancePx", delegate() {
			icon.Layout (new NendAdIconLayoutBuilder ()
			             .Orientation (Orientation.VERTICAL)
			             .Gravity ((int)Gravity.RIGHT | (int)Gravity.BOTTOM)
                         .MarginRight (16)
                         .MarginBottom (16)
                         .Unit (ComplexUnit.PX));
		});
		RegisterAction ("CenterTopWithClearance", delegate() {
			icon.Layout (new NendAdIconLayoutBuilder ()
			             .Orientation (Orientation.VERTICAL)
			             .Gravity ((int)Gravity.CENTER_HORIZONTAL | (int)Gravity.TOP)
                         .MarginTop (16));
		});
		RegisterAction ("CenterTopWithClearancePx", delegate() {
			icon.Layout (new NendAdIconLayoutBuilder ()
			             .Orientation (Orientation.VERTICAL)
			             .Gravity ((int)Gravity.CENTER_HORIZONTAL | (int)Gravity.TOP)
                         .MarginTop (16)
                         .Unit (ComplexUnit.PX));
		});
		RegisterAction ("CenterLeftWithClearance", delegate() {
			icon.Layout (new NendAdIconLayoutBuilder ()
			             .Orientation (Orientation.VERTICAL)
			             .Gravity ((int)Gravity.LEFT | (int)Gravity.CENTER_VERTICAL)
                         .MarginLeft (16));
		});
		RegisterAction ("CenterRightWithClearance", delegate() {
			icon.Layout (new NendAdIconLayoutBuilder ()
			             .Orientation (Orientation.VERTICAL)
			             .Gravity ((int)Gravity.RIGHT | (int)Gravity.CENTER_VERTICAL)
                         .MarginRight (16));
		});
		RegisterAction ("CenterBottomWithClearance", delegate() {
			icon.Layout (new NendAdIconLayoutBuilder ()
			             .Orientation (Orientation.VERTICAL)
			             .Gravity ((int)Gravity.CENTER_HORIZONTAL | (int)Gravity.BOTTOM)
                         .MarginBottom (16));
		});
		RegisterAction ("CenterBottomWithClearancePx", delegate() {
			icon.Layout (new NendAdIconLayoutBuilder ()
			             .Orientation (Orientation.VERTICAL)
			             .Gravity ((int)Gravity.CENTER_HORIZONTAL | (int)Gravity.BOTTOM)
                         .MarginBottom (16)
                         .Unit (ComplexUnit.PX));
		});
		RegisterAction ("ToVertical", delegate() {
			icon.Layout (new NendAdIconLayoutBuilder ().Orientation (Orientation.VERTICAL));
		});
		RegisterAction ("ToVerticalCenter", delegate() {
			icon.Layout (new NendAdIconLayoutBuilder ()
			             .Orientation (Orientation.VERTICAL)
                         .Gravity ((int)Gravity.CENTER_VERTICAL));
		});
		RegisterAction ("ToHorizontal", delegate() {
			icon.Layout (new NendAdIconLayoutBuilder ().Orientation (Orientation.HORIZONTAL));
		});
		RegisterAction ("ToHorizontalCenter", delegate() {
			icon.Layout (new NendAdIconLayoutBuilder ()
			             .Orientation (Orientation.HORIZONTAL)
                         .Gravity ((int)Gravity.CENTER_HORIZONTAL));
		});
		RegisterAction ("ToCorner", delegate() {
			icon.Layout (new NendAdIconLayoutBuilder ()
			             .Orientation (Orientation.UNSPECIFIED)
                         .Gravity (0, (int)Gravity.TOP | (int)Gravity.LEFT)
                         .Gravity (1, (int)Gravity.TOP | (int)Gravity.RIGHT)
                         .Gravity (2, (int)Gravity.BOTTOM | (int)Gravity.LEFT)
                         .Gravity (3, (int)Gravity.BOTTOM | (int)Gravity.RIGHT));
		});
		RegisterAction ("ToCornerWithClearance", delegate() {
			icon.Layout (new NendAdIconLayoutBuilder ()
			             .Orientation (Orientation.UNSPECIFIED)
			             .Gravity (0, (int)Gravity.TOP | (int)Gravity.LEFT)
			             .MarginLeft (0, 16)
			             .MarginTop (0, 16)
			             .Gravity (1, (int)Gravity.TOP | (int)Gravity.RIGHT)
			             .MarginRight (1, 16)
			             .MarginTop (1, 16)
			             .Gravity (2, (int)Gravity.BOTTOM | (int)Gravity.LEFT)
			             .MarginLeft (2, 16)
			             .MarginBottom (2, 16)
			             .Gravity (3, (int)Gravity.BOTTOM | (int)Gravity.RIGHT)
			             .MarginRight (3, 16)
			             .MarginBottom (3, 16));
		});
		RegisterAction ("ToCentering", delegate() {
			icon.Layout (new NendAdIconLayoutBuilder ()
			             .Orientation (Orientation.UNSPECIFIED)
			             .Gravity (0, (int)Gravity.LEFT | (int)Gravity.CENTER_VERTICAL)
			             .Size (0, 75)
			             .MarginLeft (0, 16)
			             .MarginBottom (0, 75 / 2)
			             .Gravity (1, (int)Gravity.LEFT | (int)Gravity.CENTER_VERTICAL)
			             .Size (1, 75)
			             .MarginLeft (1, 16)
			             .MarginTop (1, 75 / 2)
			             .Size (2, 75)
			             .Gravity (2, (int)Gravity.RIGHT | (int)Gravity.CENTER_VERTICAL)
			             .MarginRight (2, 16)
			             .MarginBottom (2, 75 / 2)
			             .Size (3, 75)
			             .Gravity (3, (int)Gravity.RIGHT | (int)Gravity.CENTER_VERTICAL)
			             .MarginRight (3, 16)
			             .MarginTop (3, 75 / 2));
		});
		RegisterAction ("SizeChange", delegate() {
			icon.Layout (new NendAdIconLayoutBuilder ()
			             .Orientation (Orientation.HORIZONTAL)
			             .Size (0, 60)
			             .Size (2, 60));
		});
		RegisterAction ("SizeChangeAll", delegate() {
			icon.Layout (new NendAdIconLayoutBuilder ()
			             .Orientation (Orientation.HORIZONTAL)
			             .Size (0, 80)
			             .Size (1, 60)
			             .Size (2, 80)
			             .Size (3, 60));
		});
		RegisterAction ("SizeChangePx", delegate() {
			icon.Layout (new NendAdIconLayoutBuilder ()
			             .Orientation (Orientation.HORIZONTAL)
			             .Size (0, 150)
			             .Size (2, 150)
			             .Unit (ComplexUnit.PX));
		});
		RegisterAction ("SizeChangeAllPx", delegate() {
			icon.Layout (new NendAdIconLayoutBuilder ()
			             .Orientation (Orientation.HORIZONTAL)
			             .Size (0, 160)
			             .Size (1, 150)
			             .Size (2, 160)
			             .Size (3, 150)
			             .Unit (ComplexUnit.PX));
		});
		RegisterAction ("ResetSize", delegate() {
			icon.Layout (new NendAdIconLayoutBuilder ()
			             .Orientation (Orientation.HORIZONTAL)
			             .Size (0, 75)
			             .Size (1, 75)
			             .Size (2, 75)
			             .Size (3, 75));
		});
		RegisterAction ("SpaceEnabled", delegate() {
			icon.Layout (new NendAdIconLayoutBuilder ()
			             .Orientation (Orientation.HORIZONTAL)
			             .SpaceEnabled (0, true)
			             .SpaceEnabled (1, false)
			             .SpaceEnabled (2, true)
			             .SpaceEnabled (3, false));
		});
		RegisterAction ("SpaceEnabledAll", delegate() {
			icon.Layout (new NendAdIconLayoutBuilder ()
			             .Orientation (Orientation.HORIZONTAL)
			             .SpaceEnabled (0, true)
			             .SpaceEnabled (1, true)
			             .SpaceEnabled (2, true)
			             .SpaceEnabled (3, true));
		});
		RegisterAction ("SpaceDisabled", delegate() {
			icon.Layout (new NendAdIconLayoutBuilder ()
			             .Orientation (Orientation.HORIZONTAL)
			             .SpaceEnabled (0, false)
			             .SpaceEnabled (1, true)
			             .SpaceEnabled (2, false)
			             .SpaceEnabled (3, true));
		});
		RegisterAction ("SpaceDisbledAll", delegate() {
			icon.Layout (new NendAdIconLayoutBuilder ()
			             .Orientation (Orientation.HORIZONTAL)
			             .SpaceEnabled (0, false)
			             .SpaceEnabled (1, false)
			             .SpaceEnabled (2, false)
			             .SpaceEnabled (3, false));
		});
		RegisterAction ("SpaceDisabledAndTitleInvisible", delegate() {
			icon.Layout (new NendAdIconLayoutBuilder ()
			             .Orientation (Orientation.HORIZONTAL)
			             .SpaceEnabled (0, false)
			             .TitleVisible (0, false)
			             .SpaceEnabled (2, false)
			             .TitleVisible (2, false));
		});
		RegisterAction ("SpaceDisabledAndTitleInvisibleAll", delegate() {
			icon.Layout (new NendAdIconLayoutBuilder ()
			             .Orientation (Orientation.HORIZONTAL)
			             .SpaceEnabled (0, false)
			             .SpaceEnabled (1, false)
			             .SpaceEnabled (2, false)
			             .SpaceEnabled (3, false)
			             .TitleVisible (0, false)
			             .TitleVisible (1, false)
			             .TitleVisible (2, false)
			             .TitleVisible (3, false));
		});
		RegisterAction ("TitleVisible", delegate() {
			icon.Layout (new NendAdIconLayoutBuilder ()
			             .Orientation (Orientation.HORIZONTAL)
			             .TitleVisible (0, true)
			             .TitleVisible (1, false)
			             .TitleVisible (2, true)
			             .TitleVisible (3, false));
		});
		RegisterAction ("TitleVisibleAll", delegate() {
			icon.Layout (new NendAdIconLayoutBuilder ()
			             .Orientation (Orientation.HORIZONTAL)
			             .TitleVisible (0, true)
			             .TitleVisible (1, true)
			             .TitleVisible (2, true)
			             .TitleVisible (3, true));
		});
		RegisterAction ("TitleInvisible", delegate() {
			icon.Layout (new NendAdIconLayoutBuilder ()
			             .Orientation (Orientation.HORIZONTAL)
			             .TitleVisible (0, false)
			             .TitleVisible (1, true)
			             .TitleVisible (2, false)
			             .TitleVisible (3, true));
		});
		RegisterAction ("TitleInvisibleAll", delegate() {
			icon.Layout (new NendAdIconLayoutBuilder ()
			             .Orientation (Orientation.HORIZONTAL)
			             .TitleVisible (0, false)
			             .TitleVisible (1, false)
			             .TitleVisible (2, false)
			             .TitleVisible (3, false));
		});
		RegisterAction ("ToBlackTitle", delegate() {
			icon.Layout (new NendAdIconLayoutBuilder ()
			             .Orientation (Orientation.HORIZONTAL)
			             .TitleColor (0, "#000000")
			             .TitleColor (1, "#000000")
			             .TitleColor (2, "#000000")
			             .TitleColor (3, "#000000"));
		});
		RegisterAction ("ToRedTitle", delegate() {
			icon.Layout (new NendAdIconLayoutBuilder ()
			             .Orientation (Orientation.HORIZONTAL)
			             .TitleColor (0, "#FF0000")
			             .TitleColor (1, "#FF0000")
			             .TitleColor (2, "#FF0000")
			             .TitleColor (3, "#FF0000"));
		});
	}
}
#endif