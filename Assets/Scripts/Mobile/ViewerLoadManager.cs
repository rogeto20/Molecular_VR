using UnityEngine;
using System.Collections;

// ===============================
// AUTHOR: Craig Milby
// DATE: 25 October, 2017
// PURPOSE: Load the molecule into the 3d world, scale and rotate correctly
// ===============================
public class ViewerLoadManager : MonoBehaviour {

	/// <summary>
	/// Called before the first frame
	/// 
	/// Load the molecuel specified from the main menu
	/// </summary>
	void Awake ( ) {
		string molecule = SceneManager.GetParameter ( "Molecule" );

		GameObject molPrefab = ( GameObject ) Resources.Load ( molecule );
		GameObject mol = Instantiate ( molPrefab, Vector3.zero, Quaternion.identity );

		VRCameraController camera = Camera.main.GetComponent<VRCameraController> ( );
		camera.Target = mol.transform;
	}
}

