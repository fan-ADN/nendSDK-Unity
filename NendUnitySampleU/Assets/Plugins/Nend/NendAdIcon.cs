using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.Text;

public interface NendAdIconCallback 
{
	/// <summary>
	/// Raises the finish load icon event.
	/// </summary>
	/// <remarks>It is called only iOS</remarks>
	void OnFinishLoadIcon();

	/// <summary>
	/// Raises the click icon ad event.
	/// </summary>
	void OnClickIconAd();

	/// <summary>
	/// Raises the receive icon ad event.
	/// </summary>
	void OnReceiveIconAd();

	/// <summary>
	/// Raises the fail to receive icon ad event.
	/// </summary>
	/// <param name="errorCode">Error code.</param>
	/// <param name="message">Message.</param>
	void OnFailToReceiveIconAd(NendErrorCode errorCode, string message);
}

public class NendAdIcon : NendAd 
{
	[SerializeField]
	Orientation orientation;
	[SerializeField]
	Gravity[] gravity;
	[SerializeField]
	Margin margin;
	[SerializeField]
	Icon[] icon = new Icon[4];
	
	private NendAdIconCallback _callback = null;
	public NendAdIconCallback Callback
	{
		set {
			_callback = value;
		}
	}

	protected override void Create()
	{
		_TryCreateIcons(MakeParams());
	}

	public override void Show() 
	{
		_ShowIcons(gameObject.name);
	}
	
	public override void Hide()
	{
		_HideIcons(gameObject.name);
	}
	
	public override void Resume()
	{
		_ResumeIcons(gameObject.name);
	}
	
	public override void Pause()
	{
		_PauseIcons(gameObject.name);
	}
	
	public override void Destroy()
	{
		_DestroyIcons(gameObject.name);
	}
		
	private string MakeParams() 
	{
		StringBuilder builder = new StringBuilder();
		builder.Append(gameObject.name);
		builder.Append(":");
#if UNITY_ANDROID && !UNITY_EDITOR
		builder.Append(account.android.apiKey);
		builder.Append(":");
		builder.Append(account.android.spotID);
#elif UNITY_IPHONE && !UNITY_EDITOR
		builder.Append(account.iOS.apiKey);
		builder.Append(":");
		builder.Append(account.iOS.spotID);
#else
		builder.Append("");
		builder.Append(":");
		builder.Append(0);
#endif
		builder.Append(":");
		builder.Append(outputLog ? "true" : "false");
		builder.Append(":");
		builder.Append((int)orientation);
		builder.Append(":");
		builder.Append(GetBitGravity(gravity));
		builder.Append(":");
		builder.Append(margin.left);
		builder.Append(":");
		builder.Append(margin.top);
		builder.Append(":");
		builder.Append(margin.right);
		builder.Append(":");
		builder.Append(margin.bottom);
		builder.Append(":");
		builder.Append(icon.Length);
		foreach ( Icon iconInfo in icon ) {
			builder.Append(":");
			builder.Append(iconInfo.size);
			builder.Append(",");
			builder.Append(iconInfo.spaceEnabled ? "true" : "false");
			builder.Append(",");
			builder.Append(iconInfo.titleVisible ? "true" : "false");
			builder.Append(",");
			builder.Append(iconInfo.titleColor);
			builder.Append(",");
			builder.Append(GetBitGravity(iconInfo.gravity));
			builder.Append(",");
			builder.Append(iconInfo.margin.left);
			builder.Append(",");
			builder.Append(iconInfo.margin.top);
			builder.Append(",");
			builder.Append(iconInfo.margin.right);
			builder.Append(",");
			builder.Append(iconInfo.margin.bottom);
		}
		return builder.ToString();
	}		

	void NendAdIconLoader_OnFinishLoad(string message)
	{
		if ( null != _callback ) {
			_callback.OnFinishLoadIcon();
		}
	}
	
	void NendAdIconLoader_OnFailToReceiveAd(string message)
	{	
		if ( null != _callback ) {
			string[] errorInfo = message.Split(':');
			if ( 2 != errorInfo.Length ) {
				return;
			}
			_callback.OnFailToReceiveIconAd((NendErrorCode)int.Parse(errorInfo[0]), errorInfo[1]);
		}				
	}
	
	void NendAdIconLoader_OnReceiveAd(string message)
	{
		if ( null != _callback ) {
			_callback.OnReceiveIconAd();
		}
	}
	
	void NendAdIconLoader_OnClickAd(string message)
	{
		if ( null != _callback ) {
			_callback.OnClickIconAd();
		}		
	}

#if UNITY_IPHONE && !UNITY_EDITOR
	[DllImport ("__Internal")]
	private static extern void _TryCreateIcons(string paramString);
	[DllImport ("__Internal")]
	private static extern void _ShowIcons(string gameObject);
	[DllImport ("__Internal")]
	private static extern void _HideIcons(string gameObject);
	[DllImport ("__Internal")]
	private static extern void _ResumeIcons(string gameObject);
	[DllImport ("__Internal")]
	private static extern void _PauseIcons(string gameObject);
	[DllImport ("__Internal")]
	private static extern void _DestroyIcons(string gameObject);
#elif UNITY_ANDROID && !UNITY_EDITOR
	private void _TryCreateIcons(string paramString)  { 
		_plugin.CallStatic("_TryCreateIcons", paramString); 
	}
	private void _ShowIcons(string gameObject) { 
		_plugin.CallStatic("_ShowIcons", gameObject); 
	}
	private void _HideIcons(string gameObject) { 
		_plugin.CallStatic("_HideIcons", gameObject); 
	}
	private void _ResumeIcons(string gameObject) { 
		_plugin.CallStatic("_ResumeIcons", gameObject); 
	}
	private void _PauseIcons(string gameObject) { 
		_plugin.CallStatic("_PauseIcons", gameObject); 
	}
	private void _DestroyIcons(string gameObject) { 
		_plugin.CallStatic("_DestroyIcons", gameObject); 
	}
#else
	private void _TryCreateIcons(string paramString){ UnityEngine.Debug.Log("_TryCreateIcons() : " + paramString); }
	private void _ShowIcons(string gameObject){ UnityEngine.Debug.Log("_ShowIcons() : " + gameObject); }
	private void _HideIcons(string gameObject){ UnityEngine.Debug.Log("_HideIcons() : " + gameObject); }
	private void _ResumeIcons(string gameObject){ UnityEngine.Debug.Log("_ResumeIcons() : " + gameObject); }
	private void _PauseIcons(string gameObject){ UnityEngine.Debug.Log("_PauseIcons() : " + gameObject); }
	private void _DestroyIcons(string gameObject){ UnityEngine.Debug.Log("_DestroyIcons() : " + gameObject); }
#endif
}