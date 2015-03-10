namespace NendUnityPlugin.Platform
{
	internal interface NendAdInterstitialInterface
	{
		void LoadInterstitialAd (string apiKey, string spotId, bool isOutputLog);

		void ShowInterstitialAd (string spotId);

		void ShowFinishInterstitialAd (string spotId);

		void DismissInterstitialAd ();
	}
}
