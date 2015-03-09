using UnityEngine;

using System;
using System.Text;

using NendUnityPlugin.Callback;
using NendUnityPlugin.Layout;
using NendUnityPlugin.Common;
using NendUnityPlugin.Platform;

namespace NendUnityPlugin.AD
{
	/// <summary>
	/// Banner ad.
	/// </summary>
	public class NendAdBanner : NendAd
	{
		enum BannerSize : int
		{
			SIZE_320x50 = 0,
			SIZE_320x100,
			SIZE_300x100,
			SIZE_300x250,
			SIZE_728x90
		}

		[SerializeField]
		BannerSize size;
		[SerializeField]
		Gravity[] gravity;
		[SerializeField]
		Margin margin;

		private NendAdBannerCallback _callback = null;
		/// <summary>
		/// Sets the callback.
		/// </summary>
		/// \deprecated Use <c>EventHandler</c> instead.
		[Obsolete ("Use EventHandler instead")]
		public NendAdBannerCallback Callback {
			set {
				_callback = value;
			}
		}

		private NendAdBannerInterface _interface = null;
		private NendAdBannerInterface Interface {
			get {
				if (null == _interface) {
					_interface = NendAdNativeInterfaceFactory.CreateBannerAdInterface ();
				}
				return _interface;
			}
		}

		#region EventHandlers
		/// <summary>
		/// Occurs when ad loaded.
		/// </summary>
		/// \warning It is not occurred when the platform is Android.
		public event EventHandler AdLoaded;

		/// <summary>
		/// Occurs when failed to receive ad.
		/// </summary>
		/// \sa NendUnityPlugin.Common.NendAdErrorEventArgs
		public event EventHandler<NendAdErrorEventArgs> AdFailedToReceive;

		/// <summary>
		/// Occurs when ad received.
		/// </summary>
		public event EventHandler AdReceived;

		/// <summary>
		/// Occurs when ad clicked.
		/// </summary>
		public event EventHandler AdClicked;

		/// <summary>
		/// Occurs when ad comes back to screen.
		/// </summary>
		/// \warning It is not occurred when the platform is iOS.
		public event EventHandler AdBacked;
		#endregion

		protected override void Create ()
		{
			Interface.TryCreateBanner (MakeParams ());
		}

		/// <summary>
		/// Show banner ad on the screen.
		/// </summary>
		public override void Show ()
		{
			Interface.ShowBanner (gameObject.name);
		}

		/// <summary>
		/// Hide banner ad from the screen.
		/// </summary>
		public override void Hide ()
		{
			Interface.HideBanner (gameObject.name);
		}

		/// <summary>
		/// Resume ad rotation.
		/// </summary>
		public override void Resume ()
		{
			Interface.ResumeBanner (gameObject.name);
		}

		/// <summary>
		/// Pause ad rotation.
		/// </summary>
		public override void Pause ()
		{
			Interface.PauseBanner (gameObject.name);
		}

		/// <summary>
		/// Destroy ad.
		/// </summary>
		/// \note
		/// It releases resources of native side, but GameObject with NendAdBanner script is not released.
		/// When GameObject is destroyed, this method automatically will be called.
		public override void Destroy ()
		{
			Interface.DestroyBanner (gameObject.name);
		}

		/// <summary>
		/// Layout by specified builder.
		/// </summary>
		/// <param name="builder">In the case of banner ad, use <see cref="NendUnityPlugin.Layout.NendAdDefaultLayoutBuilder"/>.</param>
		public override void Layout (NendAdLayoutBuilder builder)
		{
			if (null != builder && builder is NendAdDefaultLayoutBuilder) {
				Interface.LayoutBanner (gameObject.name, builder.Build ());
			}
		}

		private string MakeParams ()
		{
			StringBuilder builder = new StringBuilder ();
			builder.Append (gameObject.name);
			builder.Append (":");
#if UNITY_ANDROID && !UNITY_EDITOR
			builder.Append(account.android.apiKey);
			builder.Append(":");
			builder.Append(account.android.spotID);
#elif UNITY_IPHONE && !UNITY_EDITOR
			builder.Append(account.iOS.apiKey);
			builder.Append(":");
			builder.Append(account.iOS.spotID);
#else
			builder.Append ("");
			builder.Append (":");
			builder.Append (0);
#endif
			builder.Append (":");
			builder.Append (outputLog ? "true" : "false");
			builder.Append (":");
			builder.Append ((int)size);
			builder.Append (":");
			builder.Append (GetBitGravity (gravity));
			builder.Append (":");
			builder.Append (margin.left);
			builder.Append (":");
			builder.Append (margin.top);
			builder.Append (":");
			builder.Append (margin.right);
			builder.Append (":");
			builder.Append (margin.bottom);
			return builder.ToString ();
		}
		
		void NendAdView_OnFinishLoad (string message)
		{
			EventHandler handler = AdLoaded;
			if (null != handler) {
				handler (this, EventArgs.Empty);
			}
			if (null != _callback) {
				_callback.OnFinishLoadBanner ();
			}
		}
		
		void NendAdView_OnFailToReceiveAd (string message)
		{
			string[] errorInfo = message.Split (':');
			if (2 != errorInfo.Length) {
				return;
			}
			EventHandler<NendAdErrorEventArgs> handler = AdFailedToReceive;
			if (null != handler) {
				NendAdErrorEventArgs args = new NendAdErrorEventArgs ();
				args.ErrorCode = (NendErrorCode)int.Parse (errorInfo [0]);
				args.Message = errorInfo [1];
				handler (this, args);
			}
			if (null != _callback) {
				_callback.OnFailToReceiveBannerAd ((NendErrorCode)int.Parse (errorInfo [0]), errorInfo [1]);
			}
		}
		
		void NendAdView_OnReceiveAd (string message)
		{
			EventHandler handler = AdReceived;
			if (null != handler) {
				handler (this, EventArgs.Empty);
			}
			if (null != _callback) {
				_callback.OnReceiveBannerAd ();
			}
		}
		
		void NendAdView_OnClickAd (string message)
		{
			EventHandler handler = AdClicked;
			if (null != handler) {
				handler (this, EventArgs.Empty);
			}
			if (null != _callback) {
				_callback.OnClickBannerAd ();
			}
		}
		
		void NendAdView_OnDismissScreen (string message)
		{
			EventHandler handler = AdBacked;
			if (null != handler) {
				handler (this, EventArgs.Empty);
			}
			if (null != _callback) {
				_callback.OnDismissScreen ();
			}
		}		
	}
}