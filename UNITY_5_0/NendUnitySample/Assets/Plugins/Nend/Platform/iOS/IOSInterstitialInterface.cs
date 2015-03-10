using System.Runtime.InteropServices;

namespace NendUnityPlugin.Platform.iOS
{
	internal class IOSInterstitialInterface : NendAdInterstitialInterface
	{
		public void LoadInterstitialAd (string apiKey, string spotId, bool isOutputLog)
		{
			_LoadInterstitialAd (apiKey, spotId, isOutputLog);
		}

		public void ShowInterstitialAd (string spotId)
		{
			_ShowInterstitialAd (spotId);
		}

		public void ShowFinishInterstitialAd (string spotId)
		{
			// noop
		}

		public void DismissInterstitialAd ()
		{
			_DismissInterstitialAd ();
		}

		[DllImport ("__Internal")]
		private static extern void _LoadInterstitialAd (string apiKey, string spotId, bool isOutputLog);

		[DllImport ("__Internal")]
		private static extern void _ShowInterstitialAd (string spotId);

		[DllImport ("__Internal")]
		private static extern void _DismissInterstitialAd ();
	}
}