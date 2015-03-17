using System;
using UnityEngine;

using NendUnityPlugin.Callback;
using NendUnityPlugin.Platform;
using NendUnityPlugin.Common;

namespace NendUnityPlugin.AD
{
	/// <summary>
	/// Interstitial ad.
	/// </summary>
	public class NendAdInterstitial : MonoBehaviour
	{	
		[SerializeField]
		bool outputLog = false;

		private static NendAdInterstitial _instance = null;
		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <returns>This instance.</returns>
		/// \warning When GameObject that added a NendAdInterstitial is not loaded, it returns null.
		public static NendAdInterstitial Instance {
			get {
				return _instance;
			}
		}
		
		private NendAdInterstitialCallback _callback = null;
		/// <summary>
		/// Sets the callback.
		/// </summary>
		/// \deprecated Use <c>EventHandler</c> instead.
		[Obsolete ("Use EventHandler instead")]
		public NendAdInterstitialCallback Callback {
			set {
				_callback = value;
			}
		}

		private NendAdInterstitialInterface _interface = null;
		private NendAdInterstitialInterface Interface {
			get {
				if (null == _interface) {
					_interface = NendAdNativeInterfaceFactory.CreateInterstitialAdInterface ();
				}
				return _interface;
			}
		}

		#region EventHandlers
		/// <summary>
		/// Occurs when ad loaded.
		/// </summary>
		/// \sa NendUnityPlugin.AD.NendAdInterstitialLoadEventArgs
		public event EventHandler<NendAdInterstitialLoadEventArgs> AdLoaded;

		/// <summary>
		/// Occurs when ad clicked.
		/// </summary>
		/// \sa NendUnityPlugin.AD.NendAdInterstitialClickEventArgs
		public event EventHandler<NendAdInterstitialClickEventArgs> AdClicked;

		/// <summary>
		/// Occurs when ad is being displayed.
		/// </summary>
		/// \sa NendUnityPlugin.AD.NendAdInterstitialShowEventArgs
		public event EventHandler<NendAdInterstitialShowEventArgs> AdShown;
		#endregion

		void Awake ()
		{
			gameObject.hideFlags = HideFlags.HideAndDontSave;
			DontDestroyOnLoad (gameObject);
			
			if (null == _instance) {
				_instance = this;
			}
		}
		
		/// <summary>
		/// Load interstitial ad.
		/// </summary>
		/// <param name="apiKey">API key.</param>
		/// <param name="spotId">Spot id.</param>
		public void Load (string apiKey, string spotId)
		{
			Interface.LoadInterstitialAd (apiKey, spotId, outputLog);
		}
		
		/// <summary>
		/// Show interstitial ad.
		/// </summary>
		/// \note Show interstitial ad to the ad space which is loaded at last.
		public void Show ()
		{
			Interface.ShowInterstitialAd ("");
		}
		
		/// <summary>
		/// Show interstitial ad on specific ad space.
		/// </summary>
		/// <param name="spotId">Spot id.</param>
		public void Show (string spotId)
		{
			Interface.ShowInterstitialAd (spotId);
		}
		
		/// <summary>
		/// Show interstitial ad when it ends.
		/// </summary>
		/// \note Show interstitial ad to the ad space which is loaded at last.
		/// \warning Implemented only Android.
		public void Finish ()
		{
			Interface.ShowFinishInterstitialAd ("");
		}
		
		/// <summary>
		/// Show interstitial ad on specific ad space when it ends.
		/// </summary>
		/// <param name="spotId">Spot id.</param>
		/// \warning Implemented only Android.
		public void Finish (string spotId)
		{
			Interface.ShowFinishInterstitialAd (spotId);
		}
		
		/// <summary>
		/// Dismiss interstitial ad from the screen.
		/// </summary>
		public void Dismiss ()
		{
			Interface.DismissInterstitialAd ();
		}
		
		void NendAdInterstitial_OnFinishLoad (string message)
		{
			string[] array = message.Split (':');
			if (2 != array.Length) {
				return;
			}
			NendAdInterstitialStatusCode status = (NendAdInterstitialStatusCode)int.Parse (array [0]);
			string spotId = array [1];
			EventHandler<NendAdInterstitialLoadEventArgs> handler = AdLoaded;
			if (null != handler) {
				NendAdInterstitialLoadEventArgs args = new NendAdInterstitialLoadEventArgs();
				args.StatusCode = status;
				args.SpotId = spotId;
				handler(this, args);
			}
			if (null != _callback) {
				if (_callback is NendAdInterstitialCallbackWithSpot) {
					((NendAdInterstitialCallbackWithSpot)_callback).OnFinishLoadInterstitialAd (status, spotId);
				} else {
					_callback.OnFinishLoadInterstitialAd (status);
				}			
			}
		}
		
		void NendAdInterstitial_OnClickAd (string message)
		{
			string[] array = message.Split (':');
			if (2 != array.Length) {
				return;
			}
			NendAdInterstitialClickType type = (NendAdInterstitialClickType)int.Parse (array [0]);
			string spotId = array [1];
			EventHandler<NendAdInterstitialClickEventArgs> handler = AdClicked;
			if (null != handler) {
				NendAdInterstitialClickEventArgs args = new NendAdInterstitialClickEventArgs();
				args.ClickType = type;
				args.SpotId = spotId;
				handler(this, args);
			}
			if (null != _callback) {
				if (_callback is NendAdInterstitialCallbackWithSpot) {
					((NendAdInterstitialCallbackWithSpot)_callback).OnClickInterstitialAd (type, spotId);
				} else {
					_callback.OnClickInterstitialAd (type);
				}
			}
		}
		
		void NendAdInterstitial_OnShowAd (string message)
		{
			string[] array = message.Split (':');
			if (2 != array.Length) {
				return;
			}
			NendAdInterstitialShowResult result = (NendAdInterstitialShowResult)int.Parse (array [0]);
			string spotId = array [1];
			EventHandler<NendAdInterstitialShowEventArgs> handler = AdShown;
			if (null != handler) {
				NendAdInterstitialShowEventArgs args = new NendAdInterstitialShowEventArgs();
				args.ShowResult = result;
				args.SpotId = spotId;
				handler(this, args);
			}
			if (null != _callback) {
				if (_callback is NendAdInterstitialCallbackWithSpot) {
					((NendAdInterstitialCallbackWithSpot)_callback).OnShowInterstitialAd (result, spotId);
				} else {
					_callback.OnShowInterstitialAd (result);
				}
			}
		}		
	}

	/// <summary>
	/// Information of load event.
	/// </summary>
	public class NendAdInterstitialLoadEventArgs : EventArgs 
	{
		/// <summary>
		/// Status of ad load.
		/// </summary>
		public NendAdInterstitialStatusCode StatusCode { get; set; }

		/// <summary>
		/// Spot id which event occurred.
		/// </summary>
		public String SpotId { get; set; }
	}

	/// <summary>
	/// Information of show event.
	/// </summary>
	public class NendAdInterstitialShowEventArgs : EventArgs 
	{
		/// <summary>
		/// Result of ad show.
		/// </summary>
		public NendAdInterstitialShowResult ShowResult { get; set; }

		/// <summary>
		/// Spot id which event occurred.
		/// </summary>
		public String SpotId { get; set; }
	}

	/// <summary>
	/// Information of click event.
	/// </summary>
	public class NendAdInterstitialClickEventArgs : EventArgs 
	{
		/// <summary>
		/// Type of ad click.
		/// </summary>
		public NendAdInterstitialClickType ClickType { get; set; }

		/// <summary>
		/// Spot id which event occurred.
		/// </summary>
		public String SpotId { get; set; }
	}
}