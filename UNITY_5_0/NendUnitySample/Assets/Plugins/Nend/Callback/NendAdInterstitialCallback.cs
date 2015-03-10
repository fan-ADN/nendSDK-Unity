using System;
using NendUnityPlugin.Common;

namespace NendUnityPlugin.Callback
{
	/// <summary>
	/// Event callbacks for interstitial ad.
	/// </summary>
	/// \deprecated Use <c>EventHandler</c> instead.
	[Obsolete ("This interface is obsolete; use EventHandler instead")]
	public interface NendAdInterstitialCallback
	{
		/// <summary>
		/// Invoked when download of ad finished.
		/// </summary>
		/// <param name="statusCode">Status code.</param>
		/// \sa NendUnityPlugin.Common.NendAdInterstitialStatusCode
		/// \deprecated Use NendUnityPlugin.AD.NendAdInterstitial.AdLoaded instead.
		[Obsolete ("Use 'NendUnityPlugin.AD.NendAdInterstitial.AdLoaded' instead.")]
		void OnFinishLoadInterstitialAd (NendAdInterstitialStatusCode statusCode);
		
		/// <summary>
		/// Invoked when ad clicked.
		/// </summary>
		/// <param name="clickType">Click type.</param>
		/// \sa NendUnityPlugin.Common.NendAdInterstitialClickType
		/// \deprecated Use NendUnityPlugin.AD.NendAdInterstitial.AdClicked instead.
		[Obsolete ("Use 'NendUnityPlugin.AD.NendAdInterstitial.AdClicked' instead.")]
		void OnClickInterstitialAd (NendAdInterstitialClickType clickType);
		
		/// <summary>
		/// Invoked when ad is being displayed.
		/// </summary>
		/// <param name="showResult">Show result.</param>
		/// \sa NendUnityPlugin.Common.NendAdInterstitialShowResult
		/// \deprecated Use NendUnityPlugin.AD.NendAdInterstitial.AdShown instead.
		[Obsolete ("Use 'NendUnityPlugin.AD.NendAdInterstitial.AdShown' instead.")]
		void OnShowInterstitialAd (NendAdInterstitialShowResult showResult);
	}

	/// <summary>
	/// Event callbacks for interstitial ad.
	/// </summary>
	/// \remarks Callback the events of each spot.
	/// \deprecated Use EventHandler instead.
	[Obsolete ("This interface is obsolete; use EventHandler instead")]
	public interface NendAdInterstitialCallbackWithSpot : NendAdInterstitialCallback
	{
		/// <summary>
		/// Invoked when download of ad finished.
		/// </summary>
		/// <param name="statusCode">Status code.</param>
		/// <param name="spotId">Spot id which event occurred.</param>
		/// \sa NendUnityPlugin.Common.NendAdInterstitialStatusCode
		/// \deprecated Use NendUnityPlugin.AD.NendAdInterstitial.AdLoaded instead.
		[Obsolete ("Use 'NendUnityPlugin.AD.NendAdInterstitial.AdLoaded' instead.")]
		void OnFinishLoadInterstitialAd (NendAdInterstitialStatusCode statusCode, string spotId);
		
		/// <summary>
		/// Invoked when ad clicked.
		/// </summary>
		/// <param name="clickType">Click type.</param>
		/// <param name="spotId">Spot id which event occurred.</param>
		/// \sa NendUnityPlugin.Common.NendAdInterstitialClickType
		/// \deprecated Use NendUnityPlugin.AD.NendAdInterstitial.AdClicked instead.
		[Obsolete ("Use 'NendUnityPlugin.AD.NendAdInterstitial.AdClicked' instead.")]
		void OnClickInterstitialAd (NendAdInterstitialClickType clickType, string spotId);
		
		/// <summary>
		/// Invoked when ad is being displayed.
		/// </summary>
		/// <param name="showResult">Show result.</param>
		/// <param name="spotId">Spot id which event occurred.</param>
		/// \sa NendUnityPlugin.Common.NendAdInterstitialShowResult
		/// \deprecated Use NendUnityPlugin.AD.NendAdInterstitial.AdShown instead.
		[Obsolete ("Use 'NendUnityPlugin.AD.NendAdInterstitial.AdShown' instead.")]
		void OnShowInterstitialAd (NendAdInterstitialShowResult showResult, string spotId);
	}
}