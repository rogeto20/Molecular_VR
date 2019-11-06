using UnityEngine;
using System.Collections;
using System;

// ===============================
// AUTHOR: Craig Milby
// EDITED BY: Michelle Mismas and Joe Pernick
// DATE: 25 October, 2017
// PURPOSE: This handles the menu for selecting molecules in the VR world. It loads
//			new molecules as well as updating them to the correct height and rotation
//			when loaded. Includes functionality for creating a mirror of the molecule
//          and resets the inner rotation of the Ethane molecule.
// ===============================
public class VRUIController : MonoBehaviour {

	public SteamVR_TrackedObject LeftHand;
	public Transform LeftHandTransform;

	public SteamVR_TrackedObject RightHand;
	public Transform RightHandTransform;

	public static GameObject CurrentMolecule; //What is going on with this guy?
    private GameObject SecondMolecule;
    private GameObject copy;
    private GameObject ethaneCopy;

    private GameObject temp;


    private LabelToggler LabelToggler;
    
    ////////////////Dom added this
    private GameObject [] moleculeList = new GameObject[2];
    //////////////////////////////////////////////////////

	//Dominic added this
	public GameObject molecule;
    Boolean switcher = true;
    Boolean initalSwitch = true;

	public GameObject MoleculeHolder;
    

    private int count = 0;
    private Boolean mainObj = true;


    public float MinHeight = 1.0f;
	public float MaxHeight = 5.0f;

	private float LastHeight = 0.0f;
    private float LastRotation = 0.0f;

    /// <summary>
    /// Hey Dominic, do you rememeber that thing you wanted to add? If not, try.
    /// </summary>


    public VRMolHeightHandler HeightHandler;
    public VRMolRotationHandler RotHandler;

        

    void Awake ( ) {
  
    }

	void Start ( ) {
		LoadMolecule ( "Methane" ); 
		molecule = GameObject.FindGameObjectWithTag("Mol"); //and this
    }


    public void HandleMoleculeSelectionClick(string p_molecule)
        {
            Destroy(CurrentMolecule);
            LoadMolecule(p_molecule);
        }

    /// <summary>
    /// Load molecule from the provided name
    /// This is called when a button is pressed
    /// </summary>
    /// <param name="p_molecule">P molecule.</param>
    public void LoadMolecule ( string p_molecule ) {
       
		foreach ( Transform mol in MoleculeHolder.transform ) {
			mol.gameObject.SetActive ( false );

			if ( mol.name == p_molecule ) {
				mol.gameObject.SetActive ( true );

				CurrentMolecule = mol.gameObject;
				SetMoleculeHeight ( LastHeight );
				SetMoleculeRotation ( LastRotation );


                moleculeList[0] = mol.gameObject; //Rome added this but will not be blamed if it does not work.

               // moleculeList[0] = CurrentMolecule;

                LabelToggler = CurrentMolecule.GetComponent<LabelToggler>();

                //creating a copy of the Ethane molecule for resetting the EthaneWheelRotation movement
                if(mol.name == "Ethane")
                {
                    ethaneCopy = Instantiate(CurrentMolecule, new Vector3(CurrentMolecule.transform.position.x, CurrentMolecule.transform.position.y, CurrentMolecule.transform.position.z),
                        new Quaternion(CurrentMolecule.transform.position.x, CurrentMolecule.transform.position.y, CurrentMolecule.transform.position.z, 0)) as GameObject;

                    //copy is inactive and not visible
                    ethaneCopy.SetActive(false);
                }
			}
		}

		/*GameObject resource = ( GameObject ) Resources.Load ( p_molecule );
		CurrentMolecule = Instantiate ( resource, new Vector3 ( 0.0f, 1.0f, 0.0f ), Quaternion.identity );
		CurrentMolecule.transform.localScale = new Vector3 ( 0.5f, 0.5f, 0.5f );

		bool labels = LabelToggler ? LabelToggler.DrawLabels : true;
		LabelToggler = CurrentMolecule.GetComponent<LabelToggler> ( );

		LabelToggler.DrawLabels = labels;
		ToggleLabels ( labels );

		VRBezierDrive[] curve = CurrentMolecule.GetComponentsInChildren<VRBezierDrive> ( );
		for ( int i = 0; i < curve.Length; i++ ) {
			curve [ i ].TrackedObject = RightHand;
			curve [ i ].TrackTransform = RightHandTransform;
		}

		SetMoleculeHeight ( LastHeight );
		SetMoleculeRotation ( LastRotation );*/
	}

	/// <summary>
	/// Update the height of the molecule for the initial load
	/// based of the old height
	/// </summary>
	/// <param name="p_height">P height.</param>
	public void SetMoleculeHeight ( float p_height ) {
		CurrentMolecule.transform.position = new Vector3 ( 0.0f, Mathf.Lerp ( MinHeight, MaxHeight, p_height ), 0.0f );
		LastHeight = p_height;
	}

    public void createMirror()
    {
        if (copy == null)
        {
            //if there is not a copy of CurrentMolecule already, creates a copy of it
            copy = Instantiate(CurrentMolecule, new Vector3(CurrentMolecule.transform.position.x + 2.5f, CurrentMolecule.transform.position.y, CurrentMolecule.transform.position.z),
            new Quaternion(CurrentMolecule.transform.position.x, CurrentMolecule.transform.position.y, CurrentMolecule.transform.position.z, 1)) as GameObject; //rotation of the object
        }
        moleculeList[1] = copy;

        //
    }

    /*public void createMirror ( )
    {
        // set molecule two to the first molecules position plus 10 to its x and y values
        Instantiate(CurrentMolecule, new Vector3(CurrentMolecule.transform.position.x + 5.0f, CurrentMolecule.transform.position.y, CurrentMolecule.transform.position.z),
            new Quaternion(CurrentMolecule.transform.position.x, CurrentMolecule.transform.position.y, CurrentMolecule.transform.position.z, 1));
    }
     */
    public void destroyMirror()
    {
        Destroy(copy);

        if (CurrentMolecule == moleculeList[0])
        {
            CurrentMolecule = moleculeList[1];
        }

        if (CurrentMolecule == moleculeList[1])
        {
            CurrentMolecule = moleculeList[0];
        }
    }

    public void ResetOrientation ( ) {
        Debug.Log("Reset Orientation Called");
        HeightHandler.SetDriveVale(0.0f);
        RotHandler.SetDriveVale(0.0f);
		molecule.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f); 

        CurrentMolecule.transform.rotation = Quaternion.identity;

        //////
        //Resets the Ethane to its original internal rotation, specific to the EthaneWheelRotation script
        if (CurrentMolecule.transform.name == "Ethane")
        {
            //activate the Ethane copy
            ethaneCopy.SetActive(true);
            //destroy the current Ethane molecule
            Destroy(CurrentMolecule);

            //instantiate a clone of the Ethane copy as the Current Molecule
            CurrentMolecule = Instantiate(ethaneCopy, new Vector3(ethaneCopy.transform.position.x, ethaneCopy.transform.position.y, ethaneCopy.transform.position.z),
                        new Quaternion(ethaneCopy.transform.position.x, ethaneCopy.transform.position.y, ethaneCopy.transform.position.z, 0)) as GameObject;
            

            //rename the molecule to remove the (Clone) tag
            CurrentMolecule.transform.name = "Ethane";
            ethaneCopy.SetActive(false);
        }

        //Dominic - I am considering making reset a hard reset, that is, it also destorys the mirror.
        Destroy(copy);

    }

    
	/// <summary>
	/// Upadate the rotation of the molecule for the inital load
	/// based on the old rotation
	/// </summary>
	/// <param name="p_rotation">P rotation.</param>
	public void SetMoleculeRotation ( float p_rotation ) {
        /* Vector3 rot = CurrentMolecule.transform.eulerAngles;

         if (RotHandler != null)
         {
             RotHandler.SetDriveVale(Mathf.InverseLerp(0.0f, 360.0f, rot.y));
         }

         CurrentMolecule.transform.eulerAngles = new Vector3 ( rot.x, Mathf.Lerp ( 0.0f, 360.0f, p_rotation ), rot.y );
         */

        CurrentMolecule.transform.rotation = Quaternion.AngleAxis(Mathf.Lerp ( 0.0f, 360.0f, p_rotation), Vector3.up);

        LastRotation = p_rotation;
	}

	public void ToggleLabels ( ) {
        LabelToggler = CurrentMolecule.GetComponent<LabelToggler>();
        LabelToggler.Toggle ( );
	}

    public void switchMolecule(){

       

        if (!switcher)
        {
            CurrentMolecule = temp;
            temp = moleculeList[1];
            CurrentMolecule = moleculeList[0];
       
            if (initalSwitch)
            {
                moleculeList[1].transform.position = new Vector3(CurrentMolecule.transform.position.x + 2.2f, CurrentMolecule.transform.position.y, CurrentMolecule.transform.position.z);
                switcher = true;
            }
            else
            {
                moleculeList[1].transform.position = new Vector3(CurrentMolecule.transform.position.x, CurrentMolecule.transform.position.y, CurrentMolecule.transform.position.z);
                switcher = true;
            }
            initalSwitch = false;
            

        }
        else if (switcher)
        {
    
            CurrentMolecule = temp;
            temp = moleculeList[0];
            CurrentMolecule = moleculeList[1];
            if (initalSwitch)
            {
                moleculeList[0].transform.position = new Vector3(CurrentMolecule.transform.position.x + 2.2f, CurrentMolecule.transform.position.y, CurrentMolecule.transform.position.z);
        
            }
            else
            {
                moleculeList[0].transform.position = new Vector3(CurrentMolecule.transform.position.x, CurrentMolecule.transform.position.y, CurrentMolecule.transform.position.z);
    
            }
            initalSwitch = false;
            switcher = false;

            
        }

   


    }
    //public void ToggleLabels ( bool p_toggle ) {
	//	LabelToggler.Toggle ( p_toggle );
	//}
}

