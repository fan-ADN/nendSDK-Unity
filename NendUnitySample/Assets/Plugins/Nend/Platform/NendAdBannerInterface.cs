namespace NendUnityPlugin.Platform
{
	internal interface NendAdBannerInterface
	{
		void TryCreateBanner (string paramString);

		void ShowBanner (string gameObject);

		void HideBanner (string gameObject);

		void ResumeBanner (string gameObject);

		void PauseBanner (string gameObject);

		void DestroyBanner (string gameObject);

		void LayoutBanner (string gameObject, string paramString);
	}
}