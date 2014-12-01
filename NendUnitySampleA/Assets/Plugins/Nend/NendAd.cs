using UnityEngine;
using System.Collections;

public enum NendErrorCode : int 
{
	/// <summary>
	/// Ad size is larger than the display size
	/// </summary>
	AD_SIZE_TOO_LARGE = 0,

	/// <summary>
	/// Unknown ad view type
	/// </summary>
	INVALID_RESPONSE_TYPE,

	/// <summary>
	/// Failed to get ad
	/// </summary>
	FAILED_AD_REQUEST,

	/// <summary>
	/// Failure to obtain ad image
	/// </summary>
	FAILED_AD_DOWNLOAD,

	/// <summary>
	/// Gets the size and request size is different
	/// </summary>
	AD_SIZE_DIFFERENCES
};

/// <summary>
/// Nend ad.
/// </summary>
public abstract class NendAd : MonoBehaviour
{
	[SerializeField]
	protected Account account;
	[SerializeField]
	protected bool automaticDisplay = true;
	[SerializeField]
	protected bool outputLog = false;

	protected enum Gravity : int 
	{
		LEFT = 1,
		TOP = 2,
		RIGHT = 4,
		BOTTOM = 8,
		CENTER_VERTICAL = 16,
		CENTER_HORIZONTAL = 32
	}
	
	protected enum BannerSize : int 
	{
		SIZE_320x50 = 0,
		SIZE_320x100,
		SIZE_300x100,
		SIZE_300x250,
		SIZE_728x90
	}

	protected enum Orientation : int 
	{
		HORIZONTAL = 0,
		VERTICAL,
		UNSPECIFIED
	}

	[System.SerializableAttribute]
	protected class Margin 
	{
		public float left = 0;
		public float top = 0;
		public float right = 0;
		public float bottom = 0;
	}

	[System.SerializableAttribute]
	protected class Icon
	{
		public int size = 75;
		public bool spaceEnabled = true;
		public bool titleVisible = true;
		public string titleColor = "#000000";
		public Gravity[] gravity;
		public Margin margin;
	}

	[System.SerializableAttribute]
	protected class Account 
	{
		public NendID android;
		public NendID iOS;
	}
	
	[System.SerializableAttribute]
	protected class NendID
	{
		public string apiKey;
		public int spotID;
	}

	/// <summary>
	/// Create this instance.
	/// </summary>
	protected abstract void Create();
	
	/// <summary>
	/// Show this instance.
	/// </summary>
	public abstract void Show();
	
	/// <summary>
	/// Hide this instance.
	/// </summary>
	public abstract void Hide();
	
	/// <summary>
	/// Resume this instance.
	/// </summary>
	public abstract void Resume();
	
	/// <summary>
	/// Pause this instance.
	/// </summary>
	public abstract void Pause();
	
	/// <summary>
	/// Destroy this instance.
	/// </summary>
	public abstract void Destroy();

#if UNITY_ANDROID && !UNITY_EDITOR
	protected static AndroidJavaClass _plugin;
#endif

	void Awake()
	{
		if ( outputLog ) {
			UnityEngine.Debug.Log ("Awake => " + gameObject.name);
		}
		gameObject.hideFlags = HideFlags.HideAndDontSave;
		DontDestroyOnLoad(gameObject);

#if UNITY_ANDROID && !UNITY_EDITOR
		_plugin = new AndroidJavaClass("net.nend.unity.plugin.NendPlugin");
		if ( null == _plugin ) 
		{
			throw new System.ApplicationException("AndroidJavaClass(net.nend.unity.plugin.NendPlugin) is not found.");
		}
#endif
		Create();
	}
	
	// Use this for initialization
	void Start()
	{
		if ( automaticDisplay ) {
			Show();
		}
	}

	void OnDestroy()
	{
		if ( outputLog ) {
			UnityEngine.Debug.Log ("Destroy => " + gameObject.name);
		}
		Destroy();
	}
	
	protected int GetBitGravity(Gravity[] gravity)
	{
		int bit = 0;
		foreach ( int flag in gravity ) {
			bit |= flag;
		}
		return bit;
	}
}