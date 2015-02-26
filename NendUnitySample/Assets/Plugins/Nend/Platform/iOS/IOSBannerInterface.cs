using System.Runtime.InteropServices;

namespace NendUnityPlugin.Platform.iOS
{
	internal class IOSBannerInterface : NendAdBannerInterface
	{
		public void TryCreateBanner (string paramString)
		{
			_TryCreateBanner (paramString);
		}

		public void ShowBanner (string gameObject)
		{
			_ShowBanner (gameObject);
		}

		public void HideBanner (string gameObject)
		{
			_HideBanner (gameObject);
		}

		public void ResumeBanner (string gameObject)
		{
			_ResumeBanner (gameObject);
		}

		public void PauseBanner (string gameObject)
		{
			_PauseBanner (gameObject);
		}

		public void DestroyBanner (string gameObject)
		{
			_DestroyBanner (gameObject);
		}

		public void LayoutBanner (string gameObject, string paramString)
		{
			_LayoutBanner (gameObject, paramString);
		}

		[DllImport ("__Internal")]
		private static extern void _TryCreateBanner (string paramString);

		[DllImport ("__Internal")]
		private static extern void _ShowBanner (string gameObject);

		[DllImport ("__Internal")]
		private static extern void _HideBanner (string gameObject);

		[DllImport ("__Internal")]
		private static extern void _ResumeBanner (string gameObject);

		[DllImport ("__Internal")]
		private static extern void _PauseBanner (string gameObject);

		[DllImport ("__Internal")]
		private static extern void _DestroyBanner (string gameObject);

		[DllImport ("__Internal")]
		private static extern void _LayoutBanner (string gameObject, string paramString);
	}
}