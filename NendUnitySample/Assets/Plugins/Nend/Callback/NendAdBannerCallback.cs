using System;
using NendUnityPlugin.Common;

namespace NendUnityPlugin.Callback
{
	/// <summary>
	/// Event callbacks for banner ad.
	/// </summary>
	/// \deprecated Use <c>EventHandler</c> instead.
	[Obsolete ("This interface is obsolete; use EventHandler instead")]
	public interface NendAdBannerCallback
	{
		/// <summary>
		/// Invoked when ad request finished.
		/// </summary>
		/// \warning It is not invoked when the platform is Android.
		/// \deprecated Use NendUnityPlugin.AD.NendAdBanner.AdLoaded instead.
		[Obsolete ("Use 'NendUnityPlugin.AD.NendAdBanner.AdLoaded' instead.")]
		void OnFinishLoadBanner ();
		
		/// <summary>
		/// Invoked when ad clicked.
		/// </summary>
		/// \deprecated Use NendUnityPlugin.AD.NendAdBanner.AdClicked instead.
		[Obsolete ("Use 'NendUnityPlugin.AD.NendAdBanner.AdClicked' instead.")]
		void OnClickBannerAd ();
		
		/// <summary>
		/// Invoked when download of ad image finished.
		/// </summary>
		/// \deprecated Use NendUnityPlugin.AD.NendAdBanner.AdReceived instead.
		[Obsolete ("Use 'NendUnityPlugin.AD.NendAdBanner.AdReceived' instead.")]
		void OnReceiveBannerAd ();
		
		/// <summary>
		/// Invoked when ad request failed.
		/// </summary>
		/// <param name="errorCode">Error code.</param>
		/// <param name="message">Error message.</param>
		/// \sa NendUnityPlugin.Common.NendErrorCode
		/// \deprecated Use NendUnityPlugin.AD.NendAdBanner.AdFailedToReceive instead.
		[Obsolete ("Use 'NendUnityPlugin.AD.NendAdBanner.AdFailedToReceive' instead.")]
		void OnFailToReceiveBannerAd (NendErrorCode errorCode, string message);
		
		/// <summary>
		/// invoked when ad comes back to screen.
		/// </summary>
		/// \warning It is not invoked when the platform is iOS.
		/// \deprecated Use NendUnityPlugin.AD.NendAdBanner.AdBacked instead.
		[Obsolete ("Use 'NendUnityPlugin.AD.NendAdBanner.AdBacked' instead.")]
		void OnDismissScreen ();
	}
}