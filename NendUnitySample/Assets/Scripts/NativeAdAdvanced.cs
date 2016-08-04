using UnityEngine;
using NendUnityPlugin.AD.Native;

public class NativeAdAdvanced : MonoBehaviour
{
	private INativeAdClient m_AdClient;

	// Use this for initialization
	void Start ()
	{
		#if UNITY_EDITOR
		m_AdClient = NativeAdClientFactory.NewClient (NativeAdClientFactory.NativeAdType.LargeWide);
		#elif UNITY_IPHONE
		m_AdClient = NativeAdClientFactory.NewClient ("485504", "30fda4b3386e793a14b27bedb4dcd29f03d638e5");
		#elif UNITY_ANDROID
		m_AdClient = NativeAdClientFactory.NewClient ("485520", "a88c0bcaa2646c4ef8b2b656fd38d6785762f2ff");
		#endif	

		m_AdClient.LoadNativeAd (OnReceive);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void OnValueChanged (bool isOn)
	{
		if (isOn) {
			m_AdClient.EnableAutoReload (30000.0, OnReceive);
		} else {
			m_AdClient.DisableAutoReload ();
		}
	}

	protected virtual void OnReceive (INativeAd ad, int code, string message)
	{
	}
}