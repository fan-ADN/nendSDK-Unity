using System.Text;

namespace NendUnityPlugin.Layout
{
	internal class NendAdIconLayoutParams : NendAdDefaultLayoutParams
	{
		internal int size = 0;
		internal bool isSpaceEnabled = true;
		internal bool isTitleVisible = true;
		internal string titleColor = "#000000";

		internal override string Join (string delimiter)
		{
			StringBuilder builder = new StringBuilder (base.Join (delimiter));
			builder.Append (delimiter);
			builder.Append (size);
			builder.Append (delimiter);
			builder.Append (isSpaceEnabled ? "true" : "false");
			builder.Append (delimiter);
			builder.Append (isTitleVisible ? "true" : "false");
			builder.Append (delimiter);
			builder.Append (titleColor);
			return builder.ToString ();
		}
	}
}