using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

// ===============================
// AUTHOR: Craig Milby
// EDITED BY: Michelle Mismas and Joe Pernick
// DATE: 25 October, 2017
// PURPOSE: This handles the menu for selecting molecules in the VR world. It loads
//			new molecules as well as updating them to the correct height and rotation
//			when loaded. Includes functionality for creating a mirror of the molecule
//          and resets the inner rotation of the Ethane molecule.
// ===============================


/// <summary>
/// Rename this class
/// </summary>


public class VRUIController : MonoBehaviour
{

    //WARNING:next nine commented out variables are old stuff, might not need.
    //public SteamVR_TrackedObject LeftHand;
    //public Transform LeftHandTransform;
    //public SteamVR_TrackedObject RightHand;
    //public Transform RightHandTransform;
    //private UnityEngine.UI.Image Image;
    //public Color NormalColor;
    //public Color PressedColor;
    //public MeshRenderer Renderer;
    //private Boolean mainObj = true;

    //WARNING: Old Method
    //public void ToggleLabels ( bool p_toggle ) {
    //	LabelToggler.Toggle ( p_toggle );
    //}



    //These can change to the variable
    public static GameObject CurrentMolecule;
    private GameObject SecondMolecule; //The one that is not a copy hehe
    //private GameObject CyclohexaneCopy;
    private GameObject ethaneCopy;
    private GameObject temp;

    public float offset = 3f;

    private LabelToggler LabelToggler;

    private GameObject[] moleculeList = new GameObject[2];

    public GameObject molecule;
    Boolean switcher = true;
    Boolean initalSwitch = true;
    public static Boolean loadTwoMolecules = false;

    public GameObject MoleculeHolder;

    public float MinHeight = 1.0f;
    public float MaxHeight = 5.0f;

    private float LastHeight = 0.0f;
    private float LastRotation = 0.0f;

    public VRMolHeightHandler HeightHandler;
    public VRMolRotationHandler RotHandler;

    void Awake()
    {

    }

    void Start()
    {
        LoadMolecule("Methane");
        molecule = GameObject.FindGameObjectWithTag("Mol");
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
    public void LoadMolecule(string p_molecule)
    {

        if (!loadTwoMolecules)
        {
            LoadOneMolecule(p_molecule);
        }
        else if (loadTwoMolecules && !CurrentMolecule.name.Equals(p_molecule))
        {
            LoadSecondMolecule(p_molecule);
          
        }

    }

    private void LoadOneMolecule(string p_molecule)
    {
        //GameObject foundMol = GameObject.Find(p_molecule);
        //foundMol.SetActive(true);

        //CurrentMolecule = foundMol;
        //SetMoleculeHeight(LastHeight);
        //SetMoleculeRotation(LastRotation);


        //moleculeList[0] = foundMol;

        //LabelToggler = CurrentMolecule.GetComponent<LabelToggler>();
        //if (foundMol.name == "Ethane")
        //{
        //    ethaneCopy = Instantiate(CurrentMolecule, new Vector3(CurrentMolecule.transform.position.x, CurrentMolecule.transform.position.y, CurrentMolecule.transform.position.z),
        //        new Quaternion(CurrentMolecule.transform.position.x, CurrentMolecule.transform.position.y, CurrentMolecule.transform.position.z, 0)) as GameObject;

        //    //copy is inactive and not visible
        //    ethaneCopy.SetActive(false);
        //}
        foreach (Transform mol in MoleculeHolder.transform)
        {

            mol.gameObject.SetActive(false);
            //CurrentMolecule.gameObject.SetActive(false);

            //James mentinoed this 
            // mol.gameObject.SetActiveRecursively

            if (mol.name == p_molecule)
            {
                mol.gameObject.SetActive(true);

                CurrentMolecule = mol.gameObject;
                SetMoleculeHeight(LastHeight);
                SetMoleculeRotation(LastRotation);


                moleculeList[0] = mol.gameObject;

                LabelToggler = CurrentMolecule.GetComponent<LabelToggler>();

                //creating a copy of the Ethane molecule for resetting the EthaneWheelRotation movement
                if (mol.name == "Ethane")
                {
                    ethaneCopy = Instantiate(CurrentMolecule, new Vector3(CurrentMolecule.transform.position.x, CurrentMolecule.transform.position.y, CurrentMolecule.transform.position.z),
                        new Quaternion(CurrentMolecule.transform.position.x, CurrentMolecule.transform.position.y, CurrentMolecule.transform.position.z, 0)) as GameObject;

                    //copy is inactive and not visible
                    ethaneCopy.SetActive(false);
                }
            }
        }
    }

    private void LoadSecondMolecule(string p_molecule)
    {

        //Goes through the molecules on the wall
        foreach (Transform mol in MoleculeHolder.transform)
        {
            //mol.gameObject.SetActive(false); //makes molecule inactive and invisible


            if (mol.name == p_molecule)
            {
                Destroy(SecondMolecule);
                if (mol.name == "Cyclohexane")
                {
                    SecondMolecule = Instantiate(Resources.Load("Cyclohexane") as GameObject); //new Vector3(CurrentMolecule.transform.position.x + 3f, CurrentMolecule.transform.position.y, CurrentMolecule.transform.position.z),
                                                                                     //new Quaternion(CurrentMolecule.transform.position.x, CurrentMolecule.transform.position.y, CurrentMolecule.transform.position.z, 0)) as GameObject;
                    SecondMolecule.transform.position = new Vector3(CurrentMolecule.transform.position.x - offset, CurrentMolecule.transform.position.y, CurrentMolecule.transform.position.z);
                    //SecondMolecule.transform.localScale = new Vector3(CurrentMolecule.transform.localScale.x, CurrentMolecule.transform.localScale.y, -1 * CurrentMolecule.transform.localScale.z);

                    VRBezierDrive[] curve = SecondMolecule.GetComponentsInChildren<VRBezierDrive>();
                    for (int i = 0; i < curve.Length; i++)
                    {
                        curve[i].TrackedObject = TrackedObject;
                        curve[i].TrackTransform = TrackedObject.transform;

                    }
                }
                else
                {
                    //makes the molecule visable
                    //mol.gameObject.SetActive(true);
                   
                    //SecondMolecule = mol.gameObject;

                    SecondMolecule = Instantiate(mol.gameObject, new Vector3(CurrentMolecule.transform.position.x - offset, CurrentMolecule.transform.position.y, CurrentMolecule.transform.position.z),
                        CurrentMolecule.transform.rotation) as GameObject;
                    SecondMolecule.SetActive(true);
                    SetMoleculeHeight(LastHeight);
                    SetMoleculeRotation(LastRotation);

                    //SecondMolecule.transform.position = new Vector3(CurrentMolecule.transform.position.x + 5f, CurrentMolecule.transform.position.y, CurrentMolecule.transform.position.z);

                    LabelToggler = SecondMolecule.GetComponent<LabelToggler>();

                    //creating a copy of the Ethane molecule for resetting the EthaneWheelRotation movement
                    if (mol.name == "Ethane")
                    {
                        ethaneCopy = Instantiate(SecondMolecule, new Vector3(CurrentMolecule.transform.position.x - offset, CurrentMolecule.transform.position.y, CurrentMolecule.transform.position.z),
                            new Quaternion(CurrentMolecule.transform.position.x, CurrentMolecule.transform.position.y, CurrentMolecule.transform.position.z, 0)) as GameObject;

                        //copy is inactive and not visible
                        ethaneCopy.SetActive(false);
                    }
                }

            }
        }
        moleculeList[1] = SecondMolecule;
    }


    /// <summary>
    /// Update the height of the molecule for the initial load
    /// based of the old height
    /// </summary>
    /// <param name="p_height">P height.</param>
    public void SetMoleculeHeight(float p_height)
    {
        CurrentMolecule.transform.position = new Vector3(0.0f, Mathf.Lerp(MinHeight, MaxHeight, p_height), 0.0f);
        LastHeight = p_height;
    }

    public SteamVR_TrackedObject TrackedObject;
    public void createMirror()
    {
        if (!loadTwoMolecules)
        {

            if (SecondMolecule == null && CurrentMolecule.name != "Cyclohexane")
            {
                //if there is not a copy of CurrentMolecule already, creates a copy of it
                SecondMolecule = Instantiate(CurrentMolecule, new Vector3(CurrentMolecule.transform.position.x + offset, CurrentMolecule.transform.position.y, CurrentMolecule.transform.position.z),
                new Quaternion(CurrentMolecule.transform.position.x, CurrentMolecule.transform.position.y, CurrentMolecule.transform.position.z, 0)) as GameObject; //rotation of the object
                SecondMolecule.transform.localScale = new Vector3(SecondMolecule.transform.localScale.x, CurrentMolecule.transform.localScale.y, -1 * CurrentMolecule.transform.localScale.z);
                //moleculeList[1] = copy;
            }
            else if (SecondMolecule == null)
            {
                // badBoolean = false;
                //    copy = Instantiate(CurrentMolecule, new Vector3(CurrentMolecule.transform.position.x + 2.5f, CurrentMolecule.transform.position.y, CurrentMolecule.transform.position.z),
                //    new Quaternion(CurrentMolecule.transform.position.x, CurrentMolecule.transform.position.y, CurrentMolecule.transform.position.z, 0)) as GameObject; //rotation of the object
                //    copy.transform.localScale = new Vector3(copy.transform.localScale.x, CurrentMolecule.transform.localScale.y, -1 * CurrentMolecule.transform.localScale.z);

                //    VRBezierDrive[] curve = molecule.GetComponentsInChildren<VRBezierDrive>();
                //    for (int i = 0; i < curve.Length; i++)
                //    {
                //        curve[i].TrackedObject = TrackedObject;
                //        curve[i].TrackTransform = TrackedObject.transform;

                // CyclohexaneCopy = ;
                SecondMolecule = Instantiate(Resources.Load("Cyclohexane") as GameObject); //new Vector3(CurrentMolecule.transform.position.x + 3f, CurrentMolecule.transform.position.y, CurrentMolecule.transform.position.z),
                                                                                 //new Quaternion(CurrentMolecule.transform.position.x, CurrentMolecule.transform.position.y, CurrentMolecule.transform.position.z, 0)) as GameObject;
                SecondMolecule.transform.position = new Vector3(CurrentMolecule.transform.position.x + offset, CurrentMolecule.transform.position.y, CurrentMolecule.transform.position.z);
                SecondMolecule.transform.localScale = new Vector3(CurrentMolecule.transform.localScale.x, CurrentMolecule.transform.localScale.y, -1 * CurrentMolecule.transform.localScale.z);

                VRBezierDrive[] curve = SecondMolecule.GetComponentsInChildren<VRBezierDrive>();
                for (int i = 0; i < curve.Length; i++)
                {
                    curve[i].TrackedObject = TrackedObject;
                    curve[i].TrackTransform = TrackedObject.transform;

                }

                //}
            }
            moleculeList[1] = SecondMolecule;
        }

    }

    public void Load2()
    {
        initalSwitch = true;
        switcher = true;

        if (loadTwoMolecules)
        {
            
            loadTwoMolecules = false;
            destoySecondMolecule();
        }
        else
        {
            
            destroyMirror();
            loadTwoMolecules = true;
        }
    }

    public void destoySecondMolecule()
    {
        Destroy(SecondMolecule);
        if (CurrentMolecule == moleculeList[1])
        {
            CurrentMolecule = moleculeList[0];
        }
    }

    public void destroyMirror()
    {
        if (!loadTwoMolecules)
        {
            Destroy(SecondMolecule);
            destoySecondMolecule();
        }

    }

    public void ResetOrientation()
    {
        Debug.Log("Reset Orientation Called");
        HeightHandler.SetDriveVale(0.0f);
        RotHandler.SetDriveVale(0.0f);
        molecule.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);

        CurrentMolecule.transform.rotation = Quaternion.identity;

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
        Destroy(SecondMolecule);
        destoySecondMolecule();

    }


    /// <summary>
    /// Upadate the rotation of the molecule for the inital load
    /// based on the old rotation
    /// </summary>
    /// <param name="p_rotation">P rotation.</param>
    public void SetMoleculeRotation(float p_rotation)
    {

        CurrentMolecule.transform.rotation = Quaternion.AngleAxis(Mathf.Lerp(0.0f, 360.0f, p_rotation), Vector3.up);

        LastRotation = p_rotation;
    }

    public void ToggleLabels()
    {
        LabelToggler = CurrentMolecule.GetComponent<LabelToggler>();
        LabelToggler.Toggle();
    }

    public void switchMolecule()
    {
        //GameObject whatever = VRButton.getButton();
        //whatever.GetComponentInChildren<Text>().text = "Hello";

        float switchOffset;

        if (loadTwoMolecules)
        {
            switchOffset = -3f;
        }
        else
        {
            switchOffset = 3f;
        }

        if (moleculeList[1] != null)
        {
            if (!switcher)
            {
                CurrentMolecule = temp;
                temp = moleculeList[1];
                CurrentMolecule = moleculeList[0];

                if (initalSwitch)
                {
                    moleculeList[1].transform.position = new Vector3(CurrentMolecule.transform.position.x + switchOffset, CurrentMolecule.transform.position.y, CurrentMolecule.transform.position.z);
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
                    moleculeList[0].transform.position = new Vector3(CurrentMolecule.transform.position.x + switchOffset, CurrentMolecule.transform.position.y, CurrentMolecule.transform.position.z);

                }
                else
                {
                    moleculeList[0].transform.position = new Vector3(CurrentMolecule.transform.position.x, CurrentMolecule.transform.position.y, CurrentMolecule.transform.position.z);

                }
                initalSwitch = false;
                switcher = false;


            }

        }
    }
}

