#if UNITY_ANDROID
using UnityEngine;

namespace NendUnityPlugin.Platform.Android
{
	internal class AndroidBannerInterface : NendAdBannerInterface
	{
		private AndroidJavaClass plugin;

		internal AndroidBannerInterface ()
		{
			plugin = new AndroidJavaClass ("net.nend.unity.plugin.NendPlugin");
			if (null == plugin) {
				throw new System.ApplicationException ("AndroidJavaClass(net.nend.unity.plugin.NendPlugin) is not found.");
			}
		}

		public void TryCreateBanner (string paramString)
		{
			plugin.CallStatic ("_TryCreateBanner", paramString); 
		}

		public void ShowBanner (string gameObject)
		{
			plugin.CallStatic ("_ShowBanner", gameObject); 
		}

		public void HideBanner (string gameObject)
		{
			plugin.CallStatic ("_HideBanner", gameObject); 
		}

		public void ResumeBanner (string gameObject)
		{
			plugin.CallStatic ("_ResumeBanner", gameObject); 
		}

		public void PauseBanner (string gameObject)
		{
			plugin.CallStatic ("_PauseBanner", gameObject); 
		}

		public void DestroyBanner (string gameObject)
		{
			plugin.CallStatic ("_DestroyBanner", gameObject); 
		}

		public void LayoutBanner (string gameObject, string paramString)
		{
			plugin.CallStatic ("_LayoutBanner", gameObject, paramString); 
		}
	}
}
#endif