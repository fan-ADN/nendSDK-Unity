namespace NendUnityPlugin.Common
{
	/// <summary>
	/// The result of ad display.
	/// </summary>
	public enum NendAdInterstitialShowResult : int
	{
		/// <summary>
		/// Show ad on the screen.
		/// </summary>
		AD_SHOW_SUCCESS = 0,
		
		/// <summary>
		/// Request of ad has been failed.
		/// </summary>
		AD_LOAD_INCOMPLETE = 1,
		
		/// <summary>
		/// Request of ad has not been completed.
		/// </summary>
		AD_REQUEST_INCOMPLETE = 2,
		
		/// <summary>
		/// Download of ad image has not been completed.
		/// </summary>
		AD_DOWNLOAD_INCOMPLETE = 3,
		
		/// <summary>
		/// Not reached the frequency count.
		/// </summary>
		AD_FREQUENCY_NOT_RECHABLE = 4,
		
		/// <summary>
		/// Ad is already display.
		/// </summary>
		AD_SHOW_ALREADY = 5
	}

	/// <summary>
	/// The status of ad load.
	/// </summary>
	public enum NendAdInterstitialStatusCode : int
	{
		/// <summary>
		/// Success.
		/// </summary>
		SUCCESS = 0,
		
		/// <summary>
		/// The response type is invalid.
		/// </summary>
		INVALID_RESPONSE_TYPE = 1,
		
		/// <summary>
		/// Failed to ad request.
		/// </summary>
		FAILED_AD_REQUEST = 2,
		
		/// <summary>
		/// Failed to ad download.
		/// </summary>
		FAILED_AD_DOWNLOAD = 3
	}

	/// <summary>
	/// The type of ad click.
	/// </summary>
	public enum NendAdInterstitialClickType : int
	{
		/// <summary>
		/// The download button was clicked.
		/// </summary>
		DOWNLOAD = 0,
		
		/// <summary>
		/// The close button was clicked.
		/// </summary>
		CLOSE = 1,
		
		/// <summary>
		/// The exit button was clicked.
		/// </summary>
		/// \warning It is not used when the platform is iOS.
		EXIT = 2
	}
}