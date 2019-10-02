using UnityEngine;
using System.Collections;

// ===============================
// AUTHOR: Craig Milby
// DATE: 25 October, 2017
// PURPOSE: This class simply acts as a wrapper method to
// 			keep track of a value between [0, 1] for linear
//			and bezier drives.
// ===============================
public class VRLinearMapping : MonoBehaviour {

	[Range ( 0.0f, 1.0f )]
	public float value;

	[HideInInspector]
	public bool Changed = false;
	[HideInInspector]
	public float LastValue;

	/// <summary>
	/// Debug purposses
	/// 
	/// We use this so if we change the value in the inspector or other
	/// classes it stays updated
	/// 
	/// Will be resynced in at least 2 frames
	/// </summary>
	void Update ( ) {
		Changed = false;

		if ( value != LastValue ) {
			Changed = true;
		}

		LastValue = value;
	}
}

