using UnityEngine;
using System.Collections;

// ===============================
// AUTHOR: Craig Milby
// DATE: 25 October, 2017
// PURPOSE: This script allows an oject to rotate
//			around an arbitrary point.
// ===============================
public class RotateAround : MonoBehaviour {

	public Transform RotateAroundTransform;
	public float RotateAngle;

	void Awake ( ) {
		transform.RotateAround ( RotateAroundTransform.position, Vector3.up, RotateAngle );
	}
}

