using System.Collections.Generic;
using System.Text;

using NendUnityPlugin.Common;

namespace NendUnityPlugin.Layout
{
	/// <summary>
	/// Builder to set layout of icon ad.
	/// </summary>
	/// <example>
	/// e.g. Place icon ad aligned horizontally in the upper right of the screen, leaving little margin.
	/// <code>
	/// NendAdIcon icon = ...; // Get NendAdIcon instance.
	/// icon.Layout (new NendAdIconLayoutBuilder ()
	///                 .Orientation (Orientation.HORIZONTAL)
	///                 .Gravity ((int)Gravity.TOP | (int)Gravity.RIGHT)
	///                 .MarginTop (16)
	///                 .MarginRight (16));
	/// </code>
	/// </example>
	/// <para>
	/// The layout of each icon ad can be changed with specifying icon add tag to each methods.
	///	The tag can be set at UnityPlayer's Inspector.	
	/// </para>
	/// <example>
	/// e.g. Place icon ad on the screen four corners, leaving little margin.
	/// <code>
	/// // Set tag of 0-3 to four icon ads.
	/// NendAdIcon icon = ...; // Get NendAdIcon instance.
	/// icon.Layout (new NendAdIconLayoutBuilder ()
	///                .Orientation (Orientation.UNSPECIFIED) // Not aligned.
	///                .Gravity (0, (int)Gravity.TOP | (int)Gravity.LEFT)
	///		           .MarginLeft (0, 8)
	///                .MarginTop (0, 8)
	///	               .Gravity (1, (int)Gravity.TOP | (int)Gravity.RIGHT)
	///                .MarginRight (1, 8)
	///                .MarginTop (1, 8)
	///                .Gravity (2, (int)Gravity.BOTTOM | (int)Gravity.LEFT)
	///                .MarginLeft (2, 8)
	///                .MarginBottom (2, 8)
	///                .Gravity (3, (int)Gravity.BOTTOM | (int)Gravity.RIGHT)
	///                .MarginRight (3, 8)
	///	               .MarginBottom (3, 8));
	/// </code>
	/// </example>
	/// <example>
	/// e.g. Place icon ad in specific size.
	/// <code>
	/// // Set tag of 0-3 to four icon ads.
	/// NendAdIcon icon = ...; // Get NendAdIcon instance.
	/// icon.Layout (new NendAdIconLayoutBuilder ()
	///                .Orientation (Orientation.HORIZONTAL) // Horizontal aligned.
	///		           .Size (0, 120)
	///                .Size (1, 80)
	///                .Size (2, 120)
	///	               .Size (3, 80));
	/// </code>
	/// </example>
	public class NendAdIconLayoutBuilder : NendAdDefaultLayoutBuilder
	{
		private Dictionary<int, NendAdIconLayoutParams> layoutParamsDict = new Dictionary<int, NendAdIconLayoutParams> ();
		private Orientation orientation = NendUnityPlugin.Common.Orientation.HORIZONTAL;
		private int[] tags;

		/// <summary>
		/// Set orientation.
		/// </summary>
		/// <returns>This instance.</returns>
		/// <param name="orientation">Orientation.</param>
		/// \remarks Default is horizontal.
		/// \sa NendUnityPlugin.Common.Orientation
		public NendAdIconLayoutBuilder Orientation (Orientation orientation)
		{
			this.orientation = orientation;
			return this;
		}

		/// <summary>
		/// Set gravity to icon of specific tag.
		/// </summary>
		/// <returns>This instance.</returns>
		/// <param name="tag">Tag of icon.</param>
		/// <param name="gravity">Gravity.</param>
		/// \remarks Default is 0 (no gravity).
		/// \sa NendUnityPlugin.Common.Gravity
		public NendAdIconLayoutBuilder Gravity (int tag, int gravity)
		{
			GetLayoutParams (tag).gravity = gravity;
			return this;
		}

		/// <summary>
		/// Set left margin to icon of specific tag.
		/// </summary>
		/// <returns>This instance.</returns>
		/// <param name="tag">Tag of icon.</param>
		/// <param name="value">Value of left margin.</param>
		/// \remarks Default is 0.
		public NendAdIconLayoutBuilder MarginLeft (int tag, float value)
		{
			GetLayoutParams (tag).left = value;
			return this;
		}

		/// <summary>
		/// Set top margin to icon of specific tag.
		/// </summary>
		/// <returns>This instance.</returns>
		/// <param name="tag">Tag of icon.</param>
		/// <param name="value">Value of top margin.</param>
		/// \remarks Default is 0.
		public NendAdIconLayoutBuilder MarginTop (int tag, float value)
		{
			GetLayoutParams (tag).top = value;
			return this;
		}

		/// <summary>
		/// Set right margin to icon of specific tag.
		/// </summary>
		/// <returns>This instance.</returns>
		/// <param name="tag">Tag of icon.</param>
		/// <param name="value">Value of right margin.</param>
		/// \remarks Default is 0.
		public NendAdIconLayoutBuilder MarginRight (int tag, float value)
		{
			GetLayoutParams (tag).right = value;
			return this;
		}

		/// <summary>
		/// Set bottom margin to icon of specific tag.
		/// </summary>
		/// <returns>This instance.</returns>
		/// <param name="tag">Tag of icon.</param>
		/// <param name="bottom">Value of bottom margin.</param>
		/// \remarks Default is 0.
		public NendAdIconLayoutBuilder MarginBottom (int tag, float value)
		{
			GetLayoutParams (tag).bottom = value;
			return this;
		}

		/// <summary>
		/// Set size to icon of specific tag.
		/// </summary>
		/// <returns>This instance.</returns>
		/// <param name="tag">Tag of icon.</param>
		/// <param name="size">Size of icon.</param>
		/// \remarks Default is the previous value.
		public NendAdIconLayoutBuilder Size (int tag, int size)
		{
			GetLayoutParams (tag).size = size;
			return this;
		}

		/// <summary>
		/// Set presence or absence of space to icon of specific tag.
		/// </summary>
		/// <returns>This instance.</returns>
		/// <param name="tag">Tag of icon.</param>
		/// <param name="enabled">If set to <c>true</c>, space of icon enabled.</param>
		/// \remarks Default is <c>true</c>.
		public NendAdIconLayoutBuilder SpaceEnabled (int tag, bool enabled)
		{
			GetLayoutParams (tag).isSpaceEnabled = enabled;
			return this;
		}

		/// <summary>
		/// Set presence or absence of the display of title to icon of specific tag.
		/// </summary>
		/// <returns>This instance.</returns>
		/// <param name="tag">Tag of icon.</param>
		/// <param name="titleVisible">If set to <c>true</c>, title of icon visible.</param>
		/// \remarks Default is <c>true</c>.
		public NendAdIconLayoutBuilder TitleVisible (int tag, bool titleVisible)
		{
			GetLayoutParams (tag).isTitleVisible = titleVisible;
			return this;
		}

		/// <summary>
		/// Set title color to icon of specific tag.
		/// </summary>
		/// <returns>This instance.</returns>
		/// <param name="tag">Tag of icon.</param>
		/// <param name="titleColor">Title color.</param>
		/// <example>
		/// e.g. Set red.
		/// <code>
		/// NendAdIconLayoutBuilder builder = new NendAdIconLayoutBuilder ();
		/// builder.TitleColor (0, "#FF0000");
		/// </code>
		/// </example>
		/// \remarks Default is <c>"#000000"</c>(Black).
		/// \warning Please specify in <c>"#rrggbb"</c> format.
		public NendAdIconLayoutBuilder TitleColor (int tag, string titleColor)
		{
			GetLayoutParams (tag).titleColor = titleColor;
			return this;
		}

		private NendAdIconLayoutParams GetLayoutParams (int tag)
		{
			NendAdIconLayoutParams layoutParams;
			if (layoutParamsDict.ContainsKey (tag)) {
				layoutParams = layoutParamsDict [tag];
			} else {
				layoutParams = new NendAdIconLayoutParams ();
				layoutParamsDict [tag] = layoutParams;
			}
			return layoutParams;
		}

		internal string Build(int[] tags) 
		{
			this.tags = tags;
			foreach (int tag in tags) {
				if (!layoutParamsDict.ContainsKey (tag)) {
					layoutParamsDict [tag] = new NendAdIconLayoutParams ();
				}
			}
			return Build ();
		}

		public override string Build ()
		{
			StringBuilder builder = new StringBuilder (base.Build ());
			builder.Append (":");
			builder.Append ((int)orientation);
			builder.Append (":");
			builder.Append (layoutParamsDict.Count);
			foreach (int tag in tags) {
				if (layoutParamsDict.ContainsKey (tag)) {
					builder.Append (":");
					builder.Append (tag);
					builder.Append (",");
					builder.Append (layoutParamsDict [tag].Join (","));
				}
			}
			return builder.ToString ();
		}
	}
}
