using System;
using NendUnityPlugin.Common;

namespace NendUnityPlugin.Callback
{
	/// <summary>
	/// Event callbacks for icon ad.
	/// </summary>
	/// \deprecated Use <c>EventHandler</c> instead.
	[Obsolete ("This interface is obsolete; use EventHandler instead")]
	public interface NendAdIconCallback
	{
		/// <summary>
		/// Invoked when ad request finished.
		/// </summary>
		/// \warning It is not invoked when the platform is Android.
		/// \deprecated Use NendUnityPlugin.AD.NendAdIcon.AdLoaded instead.
		[Obsolete ("Use 'NendUnityPlugin.AD.NendAdIcon.AdLoaded' instead.")]
		void OnFinishLoadIcon ();
		
		/// <summary>
		/// Invoked when ad clicked.
		/// </summary>
		/// \deprecated Use NendUnityPlugin.AD.NendAdIcon.AdClicked instead.
		[Obsolete ("Use 'NendUnityPlugin.AD.NendAdIcon.AdClicked' instead.")]
		void OnClickIconAd ();
		
		/// <summary>
		/// Invoked when download of ad image finished.
		/// </summary>
		/// \deprecated Use NendUnityPlugin.AD.NendAdIcon.AdReceived instead.
		[Obsolete ("Use 'NendUnityPlugin.AD.NendAdIcon.AdReceived' instead.")]
		void OnReceiveIconAd ();
		
		/// <summary>
		/// Invoked when ad request failed.
		/// </summary>
		/// <param name="errorCode">Error code.</param>
		/// <param name="message">Error message.</param>
		/// \sa NendUnityPlugin.Common.NendErrorCode
		/// \deprecated Use NendUnityPlugin.AD.NendAdIcon.AdFailedToReceive instead.
		[Obsolete ("Use 'NendUnityPlugin.AD.NendAdIcon.AdFailedToReceive' instead.")]
		void OnFailToReceiveIconAd (NendErrorCode errorCode, string message);
	}
}