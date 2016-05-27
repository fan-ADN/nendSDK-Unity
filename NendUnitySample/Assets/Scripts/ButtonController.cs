using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

using NendUnityPlugin.AD.Native.Utils;

public class ButtonController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		NendAdLogger.LogLevel = NendAdLogger.NendAdLogLevel.Debug;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnClickOverlay ()
	{
		SceneManager.LoadScene ("Overlay");
	}

	public void OnClickCamera ()
	{
		SceneManager.LoadScene ("Camera");
	}

	public void OnClickWorld ()
	{
		SceneManager.LoadScene ("World");
	}

	public void OnClickList ()
	{
		SceneManager.LoadScene ("List");
	}

	public void OnClickMenu ()
	{
		SceneManager.LoadScene ("Menu");
	}

	public void OnClickTelop ()
	{
		SceneManager.LoadScene ("Telop");
	}

	public void OnClickBack ()
	{
		SceneManager.LoadScene ("First");
	}
}