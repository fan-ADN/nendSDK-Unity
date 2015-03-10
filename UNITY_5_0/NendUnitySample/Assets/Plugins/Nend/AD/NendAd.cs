using System;
using UnityEngine;

using NendUnityPlugin.Common;
using NendUnityPlugin.Layout;

namespace NendUnityPlugin.AD
{	
	/// <summary>
	/// Base class of Nend ad.
	/// </summary>
	public abstract class NendAd : MonoBehaviour
	{
		[SerializeField]
		protected Account account;
		[SerializeField]
		protected bool automaticDisplay = true;
		[SerializeField]
		protected bool outputLog = false;

		[SerializableAttribute]
		protected class Margin
		{
			[SerializeField]
			internal float left = 0;
			[SerializeField]
			internal float top = 0;
			[SerializeField]
			internal float right = 0;
			[SerializeField]
			internal float bottom = 0;
		}
		
		[SerializableAttribute]
		protected class Account
		{
			[SerializeField]
			internal NendID android;
			[SerializeField]
			internal NendID iOS;
		}
		
		[SerializableAttribute]
		protected class NendID
		{
			[SerializeField]
			internal string apiKey;
			[SerializeField]
			internal int spotID;
		}

		void Awake ()
		{
			gameObject.hideFlags = HideFlags.HideAndDontSave;
			DontDestroyOnLoad (gameObject);
			
			Create ();
		}
		
		void Start ()
		{
			if (automaticDisplay) {
				Show ();
			}
		}
		
		void OnDestroy ()
		{
			Destroy ();
		}
		
		protected int GetBitGravity (Gravity[] gravity)
		{
			int bit = 0;
			foreach (int flag in gravity) {
				bit |= flag;
			}
			return bit;
		}

		protected abstract void Create ();

		/// <summary>
		/// Show ad on the screen.
		/// </summary>
		public abstract void Show ();

		/// <summary>
		/// Hide ad from the screen.
		/// </summary>
		public abstract void Hide ();

		/// <summary>
		/// Resume ad rotation.
		/// </summary>
		public abstract void Resume ();

		/// <summary>
		/// Pause ad rotation.
		/// </summary>
		public abstract void Pause ();

		/// <summary>
		/// Destroy ad.
		/// </summary>
		public abstract void Destroy ();

		/// <summary>
		/// Layout by specified builder.
		/// </summary>
		/// <param name="builder">Layout builder.</param>
		public abstract void Layout (NendAdLayoutBuilder builder);
	}
}