using UnityEngine;

// ===============================
// AUTHOR: Craig Milby
// DATE: 25 October, 2017
// PURPOSE: This updates the rotation of the molecule in the VR world
//			based on the value of a linear drive.
// ===============================
public class VRMolRotationHandler : MonoBehaviour {

	private VRUIController Controller;
	private VRLinearDrive Drive;

	void Start ( ) {
		Controller = GameObject.FindObjectOfType<VRUIController> ( );
		Drive = GetComponentInChildren < VRLinearDrive > ( );
	}

	/// <summary>
	/// Update the molecule rotation dependong on the value
	/// of the attached linear drive
	/// </summary>
	void Update ( ) {
		Controller.SetMoleculeRotation ( Drive.LinearMapping.value );
	}

    public void SetDriveVale(float p_value)
    {
        if (Drive)
        {
            Drive.SetInitialPosition();
        }
    }
}
