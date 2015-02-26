using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

using NendUnityPlugin.AD;

public class BaseScene : MonoBehaviour {

	private Vector2 scrollViewVector = Vector2.zero;
	private IDictionary<string, Action> functions;

	// Use this for initialization
	public virtual void Start () {
		functions = new Dictionary<string, Action> ();
	}
	
	// Update is called once per frame
	public virtual void Update () 
	{
		if (0 < Input.touchCount) {
			Touch touch = Input.touches [0];
			if (TouchPhase.Moved == touch.phase) {
				scrollViewVector.y += touch.deltaPosition.y;
			}
		}
	}

	protected void RegisterAction(string name, Action action) 
	{
		functions [name] = action;
	}

	protected void ShowAd (NendAd ad) 
	{
		ad.Show ();
		ad.Resume ();
	}

	protected void HideAd (NendAd ad) 
	{
		ad.Hide ();
		ad.Pause ();
	}

	void OnGUI ()
	{
		GUILayout.BeginArea (new Rect (0, 0, Screen.width, Screen.height));
		GUILayout.FlexibleSpace ();
		GUILayout.BeginHorizontal ();
		GUILayout.FlexibleSpace ();
		scrollViewVector = GUILayout.BeginScrollView (
			scrollViewVector,
			GUILayout.Width (Screen.width / 3 * 2),
			GUILayout.Height (Screen.height / 3 * 2));
		foreach (string key in functions.Keys) {	
			if (GUILayout.Button (key, GUILayout.MinHeight (100))) {
				Action action = functions [key];
				action ();
			}
		}
		GUILayout.EndScrollView ();
		GUILayout.FlexibleSpace ();
		GUILayout.EndHorizontal ();
		GUILayout.FlexibleSpace ();
		GUILayout.EndArea ();
	}
}
