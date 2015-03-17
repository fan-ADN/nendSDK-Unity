namespace NendUnityPlugin.Common
{
	/// <summary>
	/// The error code of ad.
	/// </summary>
	public enum NendErrorCode : int
	{
		/// <summary>
		/// Ad size is larger than display size.
		/// </summary>
		AD_SIZE_TOO_LARGE = 0,
		
		/// <summary>
		/// The response type is invalid.
		/// </summary>
		INVALID_RESPONSE_TYPE,
		
		/// <summary>
		/// Failed to ad request.
		/// </summary>
		FAILED_AD_REQUEST,
		
		/// <summary>
		/// Failed to ad download.
		/// </summary>
		FAILED_AD_DOWNLOAD,
		
		/// <summary>
		/// Gets size and request size is different.
		/// </summary>
		AD_SIZE_DIFFERENCES
	}
}