using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.Text;

public interface NendAdBannerCallback 
{
	/// <summary>
	/// Raises the finish load banner event.
	/// </summary>
	/// <remarks>It is called only iOS</remarks>
	void OnFinishLoadBanner();

	/// <summary>
	/// Raises the click banner ad event.
	/// </summary>
	void OnClickBannerAd();

	/// <summary>
	/// Raises the receive banner ad event.
	/// </summary>
	void OnReceiveBannerAd();

	/// <summary>
	/// Raises the fail to receive banner ad event.
	/// </summary>
	/// <param name="errorCode">Error code.</param>
	/// <param name="message">Message.</param>
	void OnFailToReceiveBannerAd(NendErrorCode errorCode, string message);

	/// <summary>
	/// Raises the dismiss screen event.
	/// </summary>
	/// <remarks>It is called only Android</remarks>
	void OnDismissScreen();
}

public class NendAdBanner : NendAd 
{
	[SerializeField]
	BannerSize size;
	[SerializeField]
	Gravity[] gravity;
	[SerializeField]
	Margin margin;
	
	private NendAdBannerCallback _callback = null;
	public NendAdBannerCallback Callback
	{
		set {
			_callback = value;
		}
	}

	protected override void Create()
	{
		_TryCreateBanner(MakeParams());
	}

	public override void Show()
	{
		_ShowBanner(gameObject.name);
	}
	
	public override void Hide()
	{
		_HideBanner(gameObject.name);
	}
	
	public override void Resume()
	{
		_ResumeBanner(gameObject.name);
	}
	
	public override void Pause()
	{
		_PauseBanner(gameObject.name);
	}
	
	public override void Destroy()
	{
		_DestroyBanner(gameObject.name);
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
		builder.Append((int)size);
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
		return builder.ToString();
	}		

	void NendAdView_OnFinishLoad(string message)
	{
		if ( null != _callback ) {
			_callback.OnFinishLoadBanner();
		}
	}
	
	void NendAdView_OnFailToReceiveAd(string message)
	{
		if ( null != _callback ) {
			string[] errorInfo =  message.Split(':');
			if ( 2 != errorInfo.Length ) {
				return;
			}
			_callback.OnFailToReceiveBannerAd((NendErrorCode)int.Parse(errorInfo[0]), errorInfo[1]);
		}				
	}
	
	void NendAdView_OnReceiveAd(string message)
	{
		if ( null != _callback ) {
			_callback.OnReceiveBannerAd();
		}
	}
	
	void NendAdView_OnClickAd(string message)
	{
		if ( null != _callback ) {
			_callback.OnClickBannerAd();
		}
	}

	void NendAdView_OnDismissScreen(string message)
	{
		if ( null != _callback ) {
			_callback.OnDismissScreen();
		}
	}

#if UNITY_IPHONE && !UNITY_EDITOR
	[DllImport ("__Internal")]
	private static extern void _TryCreateBanner(string paramString);
	[DllImport ("__Internal")]
	private static extern void _ShowBanner(string gameObject);
	[DllImport ("__Internal")]
	private static extern void _HideBanner(string gameObject);
	[DllImport ("__Internal")]
	private static extern void _ResumeBanner(string gameObject);
	[DllImport ("__Internal")]
	private static extern void _PauseBanner(string gameObject);
	[DllImport ("__Internal")]
	private static extern void _DestroyBanner(string gameObject);
#elif UNITY_ANDROID && !UNITY_EDITOR
	private void _TryCreateBanner(string paramString) { 
		_plugin.CallStatic("_TryCreateBanner", paramString); 
	}
	private void _ShowBanner(string gameObject) { 
		_plugin.CallStatic("_ShowBanner", gameObject); 
	}
	private void _HideBanner(string gameObject) { 
		_plugin.CallStatic("_HideBanner", gameObject); 
	}
	private void _ResumeBanner(string gameObject) { 
		_plugin.CallStatic("_ResumeBanner", gameObject); 
	}
	private void _PauseBanner(string gameObject) { 
		_plugin.CallStatic("_PauseBanner", gameObject); 
	}
	private void _DestroyBanner(string gameObject) { 
		_plugin.CallStatic("_DestroyBanner", gameObject); 
	}
#else
	private void _TryCreateBanner(string paramString){ UnityEngine.Debug.Log("_TryCreateBanner() : " + paramString); }
	private void _ShowBanner(string gameObject){ UnityEngine.Debug.Log("_ShowBanner() : " + gameObject); }
	private void _HideBanner(string gameObject){ UnityEngine.Debug.Log("_HideBanner() : " + gameObject); }
	private void _ResumeBanner(string gameObject){ UnityEngine.Debug.Log("_ResumeBanner() : " + gameObject); }
	private void _PauseBanner(string gameObject){ UnityEngine.Debug.Log("_PauseBanner() : " + gameObject ); }
	private void _DestroyBanner(string gameObject){ UnityEngine.Debug.Log("_DestroyBanner() : " + gameObject); }
#endif
}