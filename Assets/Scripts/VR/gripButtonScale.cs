﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ===============================
// AUTHOR: Joe Pernick
// DATE: 24 September, 2018  
// UPDATED: 31 October, 2018
// PURPOSE: VRScaling on the right controller that allows the molecules to be scaled up 
// ===============================

public class gripButtonScale : MonoBehaviour
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

    //boolean to see if the grip button was pushed on this controller
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
        //Test to see if the grip button is pressed down on the right controller
        //10/31/2018 Makes it so the molecule cannot scale smaller than 10f and no larger than 100f
       
       if (controller.GetPress(SteamVR_Controller.ButtonMask.Grip))
       {
            if (molecule.transform.localScale.x < 6.0f)
            {
                //IF the right grip button is pressed scale the molecule up by half of its current x, y and z positions 
                molecule.transform.localScale += new Vector3(0.2f, 0.2f, 0.2f);
                gripButtonPushed = true;
                //Debug.Log(gripButtonPushed);
            }
       }
        


    }
}

