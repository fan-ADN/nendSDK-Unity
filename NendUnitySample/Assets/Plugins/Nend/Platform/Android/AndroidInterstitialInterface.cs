#if UNITY_ANDROID
using UnityEngine;

namespace NendUnityPlugin.Platform.Android
{
	internal class AndroidInterstitialInterface : NendAdInterstitialInterface
	{
		private AndroidJavaClass plugin;
		
		internal AndroidInterstitialInterface ()
		{
			plugin = new AndroidJavaClass ("net.nend.unity.plugin.NendPlugin");
			if (null == plugin) {
				throw new System.ApplicationException ("AndroidJavaClass(net.nend.unity.plugin.NendPlugin) is not found.");
			}
		}

		public void LoadInterstitialAd (string apiKey, string spotId, bool isOutputLog)
		{
			plugin.CallStatic ("_LoadInterstitialAd", apiKey, spotId, isOutputLog); 
		}

		public void ShowInterstitialAd (string spotId)
		{
			plugin.CallStatic ("_ShowInterstitialAd", spotId); 
		}

		public void ShowFinishInterstitialAd (string spotId)
		{
			plugin.CallStatic ("_ShowFinishInterstitialAd", spotId); 
		}

		public void DismissInterstitialAd ()
		{
			plugin.CallStatic ("_DismissInterstitialAd"); 
		}
	}
}
#endif