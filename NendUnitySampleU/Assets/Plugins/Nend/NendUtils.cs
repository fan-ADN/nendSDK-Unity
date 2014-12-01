using UnityEngine;

public class NendUtils 
{
	public static NendAdBanner GetBannerComponent(string gameObjectName)
	{
		return GetNendComponent<NendAdBanner>(gameObjectName);
	}

	public static NendAdIcon GetIconComponent(string gameObjectName)
	{
		return GetNendComponent<NendAdIcon>(gameObjectName);
	}

	private static T GetNendComponent<T>(string gameObjectName) where T : NendAd 
	{
		GameObject gameObject = GameObject.Find(gameObjectName);
		if ( null != gameObject ) {
			return gameObject.GetComponent<T>();
		} else {
			return null;
		}
	}
}
