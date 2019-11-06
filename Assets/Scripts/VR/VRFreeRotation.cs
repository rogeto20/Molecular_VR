using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ===============================
// AUTHOR: Peter Short
// DATE: 4 December, 2017
// PURPOSE: VRFreeRotation allows the user to rotate Molecules freely
// 			using the controller (left)
// ===============================

public class VRFreeRotation : MonoBehaviour {


    public SteamVR_TrackedObject trackedobj;

    private SteamVR_Controller.Device controller {  
		get {
			return SteamVR_Controller.Input ( ( int ) trackedobj.index );
		}
    }

    public GameObject molecule;

    public float[] controllerRot;

    public bool triggerButtonDown = false;

    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;


    void Start() {

        controllerRot = new float[4];

        //if the molecule cannot be retrieved by setting it in the Unity Editor, look for it.
        if(molecule == null)
        {
            molecule = GameObject.FindGameObjectWithTag("Mol");
        }

        trackedobj = GetComponent<SteamVR_TrackedObject>();
        // Debug.Log(trackedobj.index);
        
    }

	void Update () {
        //get the rotation of the controller
        controllerRot[0] = controller.transform.rot.eulerAngles.x;
        controllerRot[1] = controller.transform.rot.eulerAngles.y;
        controllerRot[2] = controller.transform.rot.eulerAngles.z;

        //Test to see if the button is pressed down
        if (controller.GetPress(SteamVR_Controller.ButtonMask.Trigger))
        {
           // molecule = VRUIController.CurrentMolecule;
            //molecule.transform.eulerAngles = new Vector3(controllerRot[0], controllerRot[1], controllerRot[2]); //This speedds it up
            molecule.transform.rotation = Quaternion.Slerp ( molecule.transform.rotation, controller.transform.rot, Time.deltaTime * 3.0f );
        }
        
    }
}
