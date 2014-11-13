using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public enum NendAdInterstitialShowResult : int
{
	AD_SHOW_SUCCESS = 0,
	AD_LOAD_INCOMPLETE = 1,
	AD_REQUEST_INCOMPLETE = 2,
	AD_DOWNLOAD_INCOMPLETE = 3,
	AD_FREQUENCY_NOT_RECHABLE = 4,
	AD_SHOW_ALREADY = 5
}

public enum NendAdInterstitialStatusCode : int
{
	SUCCESS = 0,
	INVALID_RESPONSE_TYPE = 1,
	FAILED_AD_REQUEST = 2,
	FAILED_AD_DOWNLOAD = 3
}

public enum NendAdInterstitialClickType : int
{
	DOWNLOAD = 0,
	CLOSE = 1,
	EXIT = 2	// Use only Android
}

public interface NendAdInterstitialCallback 
{
	/// <summary>
	/// Raises the finish load ad event.
	/// </summary>
	/// <param name="statusCode">Status code.</param>
	void OnFinishLoadInterstitialAd(NendAdInterstitialStatusCode statusCode);

	/// <summary>
	/// Raises the click ad event.
	/// </summary>
	/// <param name="clickType">Click type.</param>
	void OnClickInterstitialAd(NendAdInterstitialClickType clickType);

	/// <summary>
	/// Raises the show interstitial ad event.
	/// </summary>
	/// <param name="showResult">Show result.</param>
	void OnShowInterstitialAd(NendAdInterstitialShowResult showResult);
}

public class NendAdInterstitial : MonoBehaviour {
	
	[SerializeField]
	bool outputLog = false;

	private static NendAdInterstitial _instance = null;
	public static NendAdInterstitial Instance 
	{
		get {
			return _instance;
		}
	}

	private NendAdInterstitialCallback _callback = null;
	public NendAdInterstitialCallback Callback
	{
		set {
			_callback = value;
		}
	}

#if UNITY_ANDROID && !UNITY_EDITOR
	private static AndroidJavaClass _plugin;
#endif

	void Awake () 
	{
		if ( outputLog ) {
			UnityEngine.Debug.Log ("Awake => " + gameObject.name);
		}

		gameObject.hideFlags = HideFlags.HideAndDontSave;
		DontDestroyOnLoad(gameObject);

		if ( null == _instance ) {
			_instance = this;
#if UNITY_ANDROID && !UNITY_EDITOR
			_plugin = new AndroidJavaClass("net.nend.unity.plugin.NendPlugin");
			if ( null == _plugin ) {
				throw new System.ApplicationException("AndroidJavaClass(net.nend.unity.plugin.NendPlugin) is not found.");
			}
#endif
		}
	}

	/// <summary>
	/// Load AD the specified apiKey and spotId.
	/// </summary>
	/// <param name="apiKey">API key.</param>
	/// <param name="spotId">Spot identifier.</param>
	public void Load (string apiKey, string spotId) 
	{
		_LoadInterstitialAd (apiKey, spotId, outputLog);
	}

	/// <summary>
	/// Show this instance.
	/// </summary>
	public void Show () 
	{
		_ShowInterstitialAd();
	}

	/// <summary>
	/// Show this instance at the end.
	/// </summary>
	/// <remarks>Implemented only Android</remarks>
	public void Finish () 
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		_ShowFinishInterstitialAd();
#endif
	}

	/// <summary>
	/// Dismiss this instance.
	/// </summary>
	public void Dismiss ()
	{
		_DismissInterstitialAd ();
	}

	void NendAdInterstitial_OnFinishLoad(string message)
	{
		if ( null != _callback ) {
			_callback.OnFinishLoadInterstitialAd((NendAdInterstitialStatusCode)int.Parse(message));
		}
	}

	void NendAdInterstitial_OnClickAd(string message)
	{
		if ( null != _callback ) {
			_callback.OnClickInterstitialAd((NendAdInterstitialClickType)int.Parse(message));
		}		
	}

	void NendAdInterstitial_OnShowAd(string message)
	{
		if ( null != _callback ) {
			_callback.OnShowInterstitialAd((NendAdInterstitialShowResult)int.Parse(message));	
		}
	}

#if UNITY_IPHONE && !UNITY_EDITOR
	[DllImport ("__Internal")]
	private static extern void _LoadInterstitialAd(string apiKey, string spotId, bool isOutputLog);
	[DllImport ("__Internal")]
	private static extern void _ShowInterstitialAd();
	[DllImport ("__Internal")]
	private static extern void _DismissInterstitialAd();
#elif UNITY_ANDROID && !UNITY_EDITOR
	private static void _LoadInterstitialAd(string apiKey, string spotId, bool isOutputLog) { 
		_plugin.CallStatic("_LoadInterstitialAd", apiKey, spotId, isOutputLog); 
	}
	private static void _ShowInterstitialAd() { 
		_plugin.CallStatic("_ShowInterstitialAd"); 
	}
	private static void _ShowFinishInterstitialAd() { 
		_plugin.CallStatic("_ShowFinishInterstitialAd"); 
	}
	private static void _DismissInterstitialAd() { 
		_plugin.CallStatic("_DismissInterstitialAd"); 
	}
#else
	private static void _LoadInterstitialAd(string apiKey, string spotId, bool isOutputLog){ UnityEngine.Debug.Log("_LoadInterstitialAd() : " + apiKey + ", " + spotId + ", " + isOutputLog); }
	private static void _ShowInterstitialAd(){ UnityEngine.Debug.Log("_ShowInterstitialAd()"); }
	private static void _ShowFinishInterstitialAd(){ UnityEngine.Debug.Log("_ShowFinishInterstitialAd()"); }
	private static void _DismissInterstitialAd(){ UnityEngine.Debug.Log("_DismissInterstitialAd()"); }
#endif
}