﻿using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

	public Vector3 rotation;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(rotation.x, rotation.y, rotation.z);
	}
}
