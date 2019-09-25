using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ===============================
// AUTHOR: Joe Pernick
// DATE: 24 September, 2018
// PURPOSE: VRScaling on the right controller that allows the molecules to be scaled up 
// ===============================

public class createMirror : MonoBehaviour
{



    //create an instance of a tracked object
    public SteamVR_TrackedObject trackedobj;


    //tracks the movement of the right controller
    private SteamVR_Controller.Device controller
    {
        get
        {
            return SteamVR_Controller.Input((int)trackedobj.index);
        }
    }

    //creates a game object of the molecule so all the atoms can scale together
    public GameObject molecule;


    void Mirror()
    {
        if (molecule == null)
        {
            molecule = GameObject.FindGameObjectWithTag("Mol");
        }

        moleculeTwo = new gameObject();
        trackedobj = GetComponent<SteamVR_TrackedObject>();

        // set molecule two to the first molecules position plus 10 to its x and y values
        Instantiate(molecule, Vector3((molecule.transform.x + 10f), (molecule.transform.y + 10f), (molecule.transform.z)), moleculeTwo);
    }


    void Start()
    {


        //if the molecule cannot be retrieved by setting it in the Unity Editor, look for it.
        if (molecule == null)
        {
            molecule = GameObject.FindGameObjectWithTag("Mol");
        }

        moleculeTwo = new gameObject();
        trackedobj = GetComponent<SteamVR_TrackedObject>();

        // set molecule two to the first molecules position plus 10 to its x and y values
        Instantiate(molecule, Vector3((molecule.transform.x + 10f), (molecule.transform.y + 10f), (molecule.transform.z)), moleculeTwo);


    }


}