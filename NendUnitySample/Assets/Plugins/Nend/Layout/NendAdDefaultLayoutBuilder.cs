using System.Text;

namespace NendUnityPlugin.Layout
{
	/// <summary>
	/// The unit of numeric value.
	/// </summary>
	/// \note Not valid for iOS.
	public enum ComplexUnit
	{
		/// <summary>
		/// Use density-independent pixels.
		/// </summary>
		DIP,

		/// <summary>
		/// Use pixels.
		/// </summary>
		PX
	}

	/// <summary>
	/// Builder to set layout of banner ad.
	/// </summary>
	/// <example>
	/// e.g. Place a banner ad in the center bottom of the screen, leaving little margin.
	/// <code>
	/// NendAdBanner banner = ...; // Get NendAdBanner instance.
	/// banner.Layout (new NendAdDefaultLayoutBuilder ()
	///                 .Gravity ((int)Gravity.BOTTOM | (int)Gravity.CENTER_HORIZONTAL)
	///                 .MarginBottom (16));
	/// </code>
	/// </example>
	public class NendAdDefaultLayoutBuilder : NendAdLayoutBuilder
	{	
		private NendAdDefaultLayoutParams layoutParams = new NendAdDefaultLayoutParams ();
		protected ComplexUnit unit = ComplexUnit.DIP;

		/// <summary>
		/// Set unit of numeric value.
		/// </summary>
		/// <returns>This instance.</returns>
		/// <param name="unit">Unit of numeric value.</param>
		/// \remarks Default is DIP.
		/// \sa NendUnityPlugin.Layout.ComplexUnit
		/// \note Not valid for iOS.
		public NendAdDefaultLayoutBuilder Unit (ComplexUnit unit)
		{
			this.unit = unit;
			return this;
		}

		/// <summary>
		/// Set gravity.
		/// </summary>
		/// <returns>This instance.</returns>
		/// <param name="gravity">Gravity.</param>
		/// \remarks Default is 0 (no gravity).
		/// \sa NendUnityPlugin.Common.Gravity
		public NendAdDefaultLayoutBuilder Gravity (int gravity)
		{
			layoutParams.gravity = gravity;
			return this;
		}

		/// <summary>
		/// Set left margin.
		/// </summary>
		/// <returns>This instance.</returns>
		/// <param name="value">value of left margin.</param>
		/// \remarks Default is 0.
		public NendAdDefaultLayoutBuilder MarginLeft (float value)
		{
			layoutParams.left = value;
			return this;
		}

		/// <summary>
		/// Set top margin.
		/// </summary>
		/// <returns>This instance.</returns>
		/// <param name="value">value of top margin.</param>
		/// \remarks Default is 0.
		public NendAdDefaultLayoutBuilder MarginTop (float value)
		{
			layoutParams.top = value;
			return this;
		}

		/// <summary>
		/// Set right margin.
		/// </summary>
		/// <returns>This instance.</returns>
		/// <param name="value">value of right margin.</param>
		/// \remarks Default is 0.
		public NendAdDefaultLayoutBuilder MarginRight (float value)
		{
			layoutParams.right = value;
			return this;
		}

		/// <summary>
		/// Set bottom margin.
		/// </summary>
		/// <returns>This instance.</returns>
		/// <param name="value">value of bottom margin.</param>
		/// \remarks Default is 0.
		public NendAdDefaultLayoutBuilder MarginBottom (float bottom)
		{
			layoutParams.bottom = bottom;
			return this;
		}
		
		public virtual string Build ()
		{
			StringBuilder builder = new StringBuilder ();
#if UNITY_ANDROID && !UNITY_EDITOR
			builder.Append(ComplexUnit.DIP == unit ? "true" : "false");
			builder.Append(":");
#endif
			builder.Append (layoutParams.Join (":"));
			return builder.ToString ();
		}
	}
}