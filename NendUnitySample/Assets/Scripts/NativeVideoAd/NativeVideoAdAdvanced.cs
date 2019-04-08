using UnityEngine;
using NendUnityPlugin.AD.Native;

public class NativeVideoAdAdvanced : MonoBehaviour
{
	private INativeVideoAdLoader loader;
	public GameObject nativeVideoAdPanel;
	internal NendAdNativeVideoView nativeVideoView;

	// Use this for initialization
	void Start ()
	{
		nativeVideoView = nativeVideoAdPanel.GetComponent<NendAdNativeVideoView>();
		
		#if UNITY_EDITOR
		loader = NativeVideoAdLoaderFactory.NewLoader (VideoOrientation.Landscape);
		#elif UNITY_IOS
		loader = NativeVideoAdLoaderFactory.NewLoader (887595, "e7c1e68e7c16e94270bf39719b60534596b1e70d");
		#elif UNITY_ANDROID
		loader = NativeVideoAdLoaderFactory.NewLoader (887591, "a284d892c3617bf5705facd3bfd8e9934a8b2491");
		#endif	

		loader.LoadNativeAd (OnReceive);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	protected virtual void OnReceive (INativeVideoAd ad, int code, string message)
	{
	}
}