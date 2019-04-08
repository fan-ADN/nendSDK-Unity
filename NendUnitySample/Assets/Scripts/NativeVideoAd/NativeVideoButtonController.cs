using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

using NendUnityPlugin.Common;

public class NativeVideoButtonController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		NendAdLogger.LogLevel = NendAdLogger.NendAdLogLevel.Debug;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnClickOverlay ()
	{
		SceneManager.LoadScene ("NativeVideoOverlay");
	}

	public void OnClickCamera ()
	{
		SceneManager.LoadScene ("NativeVideoCamera");
	}

	public void OnClickWorld ()
	{
		SceneManager.LoadScene ("NativeVideoWorld");
	}

	public void OnClickList ()
	{
		SceneManager.LoadScene ("NativeVideoList");
	}

	public void OnClickMenu ()
	{
		SceneManager.LoadScene ("NativeVideoMenu");
	}

	public void OnClickTelop ()
	{
		SceneManager.LoadScene ("NativeVideoTelop");
	}

	public void OnClickAdvanced ()
	{
		SceneManager.LoadScene ("NativeVideoAdvanced");
	}

	public void OnClickBack ()
	{
		SceneManager.LoadScene ("First");
	}
}