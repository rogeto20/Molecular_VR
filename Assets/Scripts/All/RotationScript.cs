using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript : MonoBehaviour {

	public float rate;
	public Transform molecule;
	// Use this for initialization
	void Start ( ) {

	}
	
	// Update is called once per frame
	void FixedUpdate ( ) {
		molecule.Rotate ( Vector3.right * Time.deltaTime * rate );
		molecule.Rotate ( Vector3.up * Time.deltaTime * rate / 2 );
	}
}
