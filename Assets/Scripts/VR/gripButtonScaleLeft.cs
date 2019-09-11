using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ===============================
// AUTHOR: Joe Pernick
// DATE: 24 September, 2018
// PURPOSE: VRScaling on the left controller that allows the molecules to be scaled down
// ===============================

public class gripButtonScaleLeft : MonoBehaviour
{

    //creates an instance of a tracked object
    public SteamVR_TrackedObject trackedobj;

   


    //tracks the movement of the left controller
    private SteamVR_Controller.Device controller
    {
        get
        {
            return SteamVR_Controller.Input((int)trackedobj.index);
        }
    }

    //creates a game object of the molecule so all the atoms scale together
    public GameObject molecule;

    //boolean to see if the grip button is pushed on this controller
    public bool gripButtonPushed = false;


    void Start()
    {


        //if the molecule cannot be retrieved by setting it in the Unity Editor, look for it.
        if (molecule == null)
        {
            molecule = GameObject.FindGameObjectWithTag("Mol");
        }

        trackedobj = GetComponent<SteamVR_TrackedObject>();
        // Debug.Log(trackedobj.index);

    }

    void Update()
    {
        //Test to see if the button is pressed down on the left controller
        if (controller.GetPress(SteamVR_Controller.ButtonMask.Grip))
        {

            //IF the left grip button is pressed scale the molecule down by .5f
            molecule.transform.localScale += new Vector3(-.05f, -.05f, -.05f);

            
            gripButtonPushed = true;
            //Debug.Log(gripButtonPushed);
        }
        

    }
}