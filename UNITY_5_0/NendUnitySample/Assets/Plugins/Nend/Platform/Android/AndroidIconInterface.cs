#if UNITY_ANDROID
using UnityEngine;

namespace NendUnityPlugin.Platform.Android
{
	internal class AndroidIconInterface : NendAdIconInterface
	{
		private AndroidJavaClass plugin;

		internal AndroidIconInterface ()
		{
			plugin = new AndroidJavaClass ("net.nend.unity.plugin.NendPlugin");
			if (null == plugin) {
				throw new System.ApplicationException ("AndroidJavaClass(net.nend.unity.plugin.NendPlugin) is not found.");
			}
		}

		public void TryCreateIcons (string paramString)
		{
			plugin.CallStatic ("_TryCreateIcons", paramString); 
		}

		public void ShowIcons (string gameObject)
		{
			plugin.CallStatic ("_ShowIcons", gameObject); 
		}

		public void HideIcons (string gameObject)
		{
			plugin.CallStatic ("_HideIcons", gameObject); 
		}

		public void ResumeIcons (string gameObject)
		{
			plugin.CallStatic ("_ResumeIcons", gameObject); 
		}

		public void PauseIcons (string gameObject)
		{
			plugin.CallStatic ("_PauseIcons", gameObject); 
		}

		public void DestroyIcons (string gameObject)
		{
			plugin.CallStatic ("_DestroyIcons", gameObject); 
		}

		public void LayoutIcons (string gameObject, string paramString)
		{
			plugin.CallStatic ("_LayoutIcons", gameObject, paramString); 
		}
	}
}
#endif