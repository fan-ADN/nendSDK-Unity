using System;

namespace NendUnityPlugin.Common
{
	/// <summary>
	/// Information of error.
	/// </summary>
	public class NendAdErrorEventArgs : EventArgs
	{
		/// <summary>
		/// Error code.
		/// </summary>
		/// \sa NendUnityPlugin.Common.NendErrorCode
		public NendErrorCode ErrorCode { get; set; }

		/// <summary>
		/// Error message.
		/// </summary>
		public String Message { get; set; }
	}
}

