using UnityEngine;

using System;
using System.Text;
using System.Collections.Generic;

using NendUnityPlugin.Callback;
using NendUnityPlugin.Layout;
using NendUnityPlugin.Common;
using NendUnityPlugin.Platform;

namespace NendUnityPlugin.AD
{
	/// <summary>
	/// Icon ad.
	/// </summary>
	public class NendAdIcon : NendAd
	{
		[SerializableAttribute]
		class Icon
		{
			[SerializeField]
			internal int tag;
			[SerializeField]
			internal int size = 75;
			[SerializeField]
			internal bool spaceEnabled = true;
			[SerializeField]
			internal bool titleVisible = true;
			[SerializeField]
			internal string titleColor = "#000000";
			[SerializeField]
			internal Gravity[] gravity;
			[SerializeField]
			internal Margin margin;
		}

		[SerializeField]
		Orientation orientation;
		[SerializeField]
		Gravity[] gravity;
		[SerializeField]
		Margin margin;
		[SerializeField]
		Icon[] icon = new Icon[4];

		private NendAdIconCallback _callback = null;
		/// <summary>
		/// Sets the callback.
		/// </summary>
		/// \deprecated Use <c>EventHandler</c> instead.
		[Obsolete ("Use EventHandler instead")]
		public NendAdIconCallback Callback {
			set {
				_callback = value;
			}
		}

		private NendAdIconInterface _interface = null;
		private NendAdIconInterface Interface {
			get {
				if (null == _interface) {
					_interface = NendAdNativeInterfaceFactory.CreateIconAdInterface ();
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
		#endregion

		protected override void Create ()
		{
			Interface.TryCreateIcons (MakeParams ());
		}

		/// <summary>
		/// Show icon ad on the screen.
		/// </summary>
		public override void Show ()
		{
			Interface.ShowIcons (gameObject.name);
		}

		/// <summary>
		/// Hide icon ad from the screen.
		/// </summary>
		public override void Hide ()
		{
			Interface.HideIcons (gameObject.name);
		}

		/// <summary>
		/// Resume ad rotation.
		/// </summary>
		public override void Resume ()
		{
			Interface.ResumeIcons (gameObject.name);
		}

		/// <summary>
		/// Pause ad rotation.
		/// </summary>
		public override void Pause ()
		{
			Interface.PauseIcons (gameObject.name);
		}

		/// <summary>
		/// Destroy ad.
		/// </summary>
		/// \note
		/// It releases resources of native side, but GameObject with NendAdIcon script is not released.
		/// When GameObject is destroyed, this method automatically will be called.
		public override void Destroy ()
		{
			Interface.DestroyIcons (gameObject.name);
		}

		/// <summary>
		/// Layout by specified builder.
		/// </summary>
		/// <param name="builder">In the case of icon ad, use <see cref="NendUnityPlugin.Layout.NendAdIconLayoutBuilder"/>.</param>
		public override void Layout (NendAdLayoutBuilder builder)
		{
			if (null != builder && builder is NendAdIconLayoutBuilder) {
				bool isDuplicated = hasDuplicatedTag (icon);
				int[] tags = new int[icon.Length];
				for (int i = 0; i < tags.Length; i++) {
					tags [i] = isDuplicated ? i : icon [i].tag;
				}
				Interface.LayoutIcons (gameObject.name, ((NendAdIconLayoutBuilder)builder).Build (tags));
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
			builder.Append ((int)orientation);
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
			builder.Append (":");
			builder.Append (icon.Length);
			
			bool isDuplicated = hasDuplicatedTag (icon);
			int idx = 0;
			foreach (Icon iconInfo in icon) {
				builder.Append (":");
				builder.Append (isDuplicated ? idx++ : iconInfo.tag);
				builder.Append (",");
				builder.Append (iconInfo.size);
				builder.Append (",");
				builder.Append (iconInfo.spaceEnabled ? "true" : "false");
				builder.Append (",");
				builder.Append (iconInfo.titleVisible ? "true" : "false");
				builder.Append (",");
				builder.Append (iconInfo.titleColor);
				builder.Append (",");
				builder.Append (GetBitGravity (iconInfo.gravity));
				builder.Append (",");
				builder.Append (iconInfo.margin.left);
				builder.Append (",");
				builder.Append (iconInfo.margin.top);
				builder.Append (",");
				builder.Append (iconInfo.margin.right);
				builder.Append (",");
				builder.Append (iconInfo.margin.bottom);
			}
			return builder.ToString ();
		}
		
		void NendAdIconLoader_OnFinishLoad (string message)
		{
			EventHandler handler = AdLoaded;
			if (null != handler) {
				handler (this, EventArgs.Empty);
			}
			if (null != _callback) {
				_callback.OnFinishLoadIcon ();
			}
		}
		
		void NendAdIconLoader_OnFailToReceiveAd (string message)
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
				_callback.OnFailToReceiveIconAd ((NendErrorCode)int.Parse (errorInfo [0]), errorInfo [1]);
			}
		}
		
		void NendAdIconLoader_OnReceiveAd (string message)
		{
			EventHandler handler = AdReceived;
			if (null != handler) {
				handler (this, EventArgs.Empty);
			}
			if (null != _callback) {
				_callback.OnReceiveIconAd ();
			}
		}
		
		void NendAdIconLoader_OnClickAd (string message)
		{
			EventHandler handler = AdClicked;
			if (null != handler) {
				handler (this, EventArgs.Empty);
			}
			if (null != _callback) {
				_callback.OnClickIconAd ();
			}		
		}
		
		private bool hasDuplicatedTag (Icon[] icon)
		{
			HashSet<int> tags = new HashSet<int> ();
			foreach (Icon item in icon) {
				if (!tags.Add (item.tag)) {
					return true;
				}
			}
			return false;
		}
	}
}