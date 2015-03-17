using System.Text;

namespace NendUnityPlugin.Layout
{
	internal class NendAdDefaultLayoutParams
	{
		internal int gravity = 0;
		internal float left = 0.0f;
		internal float top = 0.0f;
		internal float right = 0.0f;
		internal float bottom = 0.0f;

		internal virtual string Join (string delimiter)
		{
			StringBuilder builder = new StringBuilder ();
			builder.Append (gravity);
			builder.Append (delimiter);
			builder.Append (left);
			builder.Append (delimiter);
			builder.Append (top);
			builder.Append (delimiter);
			builder.Append (right);
			builder.Append (delimiter);
			builder.Append (bottom);
			return builder.ToString ();
		}
	}
}