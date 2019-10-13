using UnityEngine;
using System.Collections;

// ===============================
// AUTHOR: Craig Milby
// DATE: 25 October, 2017
// PURPOSE: This updates the height of the molecule in the VR world based
//			on the value of the linear drive.
// ===============================
public class VRMolHeightHandler : MonoBehaviour {

	private VRUIController Controller;
	private VRLinearDrive Drive;

	void Start ( ) {
		Controller = GameObject.FindObjectOfType<VRUIController> ( );
		Drive = GetComponentInChildren < VRLinearDrive > ( );
	}

	/// <summary>
	/// Update the molecule height dependong on the value of the 
	/// attached linear drive
	/// </summary>
	void Update ( ) {
		Controller.SetMoleculeHeight ( Drive.LinearMapping.value );
	}

    public void SetDriveVale(float p_value)
    {
        Drive.SetInitialPosition();
    }
}

