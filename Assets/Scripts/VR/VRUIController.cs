using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

// ===============================
// AUTHOR: Craig Milby
// EDITED BY: Michelle Mismas and Joe Pernick
// EDITED BY: Amanda Schifferle, Dominic Rayl, Matthew O'Donnell and Rome Ogeto
// DATE: 25 December, 2019
// PURPOSE: This handles the menu for selecting molecules in the VR world. It loads
//			new molecules as well as updating them to the correct height and rotation
//			when loaded. Includes functionality for creating a mirror of the molecule
//          and resets the inner rotation of the Ethane molecule.
// ===============================

public class VRUIController : MonoBehaviour
{
    public static GameObject CurrentMolecule; // Main object being interacted with
    private GameObject SecondMolecule; //The second molecule loaded either by creating a mirror or loading a second molecule
    private GameObject ethaneCopy; //This molecule is used to orient the labels for the Ethan molecule
    private GameObject temp;//Objet used when switching molecules

    public float offset = 3f;//Distance by which molecules are seperated from the current molecule

    private LabelToggler LabelToggler; //Toggles the labels

    private GameObject[] moleculeList = new GameObject[2]; //List that holds the two molecules to facilitate switching

    public GameObject molecule;
    Boolean switcher = true; //Boolean that determines if moleculeList[0] or moleculeList[1] is being switched to current 
    Boolean initalSwitch = true; //does the inital offset of the molecule
    public static Boolean loadTwoMolecules = false; //Boolean that tells if a user wants a second molecule to be loaded up

    public GameObject MoleculeHolder;

    public float MinHeight = 1.0f;
    public float MaxHeight = 5.0f;

    private float LastHeight = 0.0f;
    private float LastRotation = 0.0f;
    public SteamVR_TrackedObject TrackedObject;

    public VRMolHeightHandler HeightHandler;
    public VRMolRotationHandler RotHandler;

    void Awake()
    {

    }

    /// <summary>
    /// Loads the default molecule and sets object 'molecule' to the Methane object
    /// </summary>
    void Start()
    {
        LoadMolecule("Methane");
        molecule = GameObject.FindGameObjectWithTag("Mol");
    }

    ///This Region handles all the loading up of a molecule
    #region
    /// <summary>
    /// Handles selection of a new molecule by removing the current molecule and spawning the selected molecule.
    /// </summary>
    /// <param name="p_molecule">the name of the molecule being handled</param>
    public void HandleMoleculeSelectionClick(string p_molecule)
    {
        Destroy(CurrentMolecule);
        LoadMolecule(p_molecule);
    }

    /// <summary>
    /// Load molecule from the provided name
    /// This is called when a molecule button in the scene is pressed
    /// </summary>
    /// <param name="p_molecule">the name of the molecule being handled</param>
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

    /// <summary>
    /// Load molecule from the provided name. This is called when a molecule is selected.
    /// </summary>
    /// <param name="p_molecule">the name of the molecule being handled</param>
    private void LoadOneMolecule(string p_molecule)
    {
       
        foreach (Transform mol in MoleculeHolder.transform)
        {

            mol.gameObject.SetActive(false);
    

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

    /// <summary>
    /// Loads a second molecule from the provided name. This is called when a molecule is selected
    /// and the LoadSecondMolecule is true. The molecule loaded cannot be interacted with when
    /// first initialized into the program. And the second molecule is offset by -2.5 on the x-axis
    /// </summary>
    /// <param name="p_molecule">the name of the molecule being handled</param>
    private void LoadSecondMolecule(string p_molecule)
    {

        //Goes through the molecules on the wall
        foreach (Transform mol in MoleculeHolder.transform)
        {
            
            if (mol.name == p_molecule)
            {
                Destroy(SecondMolecule);
                if (mol.name == "Cyclohexane")
                {
                    SecondMolecule = Instantiate(Resources.Load("Cyclohexane") as GameObject); 
                    SecondMolecule.transform.position = new Vector3(CurrentMolecule.transform.position.x - offset, CurrentMolecule.transform.position.y, CurrentMolecule.transform.position.z);
                    

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
    /// Changes the initial switcher and switcher to true
    /// Toggles the LoadTwoMolecules boolean between true and false
    /// </summary>
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

            destoySecondMolecule();
            loadTwoMolecules = true;
        }
    }

    /// <summary>
    /// Update the height of the molecule for the initial load
    /// based of the old height
    /// </summary>
    /// <param name="p_height">the height of the molecule being handled</param>
    public void SetMoleculeHeight(float p_height)
    {
        CurrentMolecule.transform.position = new Vector3(0.0f, Mathf.Lerp(MinHeight, MaxHeight, p_height), 0.0f);
        LastHeight = p_height;
    }
    #endregion

    ///This region handles all the mirror functionality
    #region
    /// <summary>
    /// Creates a mirrored copy of the current molecule offset by 2.5 on the x-axis.
    /// </summary>
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
               
            }
            else if (SecondMolecule == null)
            {
               
                SecondMolecule = Instantiate(Resources.Load("Cyclohexane") as GameObject); 
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

    /// <summary>
    /// Removes the second loaded molecule from the game space—if it exists
    /// </summary>
    public void destoySecondMolecule()
    {
        Destroy(SecondMolecule);
        if (CurrentMolecule == moleculeList[1])
        {
            CurrentMolecule = moleculeList[0];
        }
    }

    /// <summary>
    /// Switches which molecule on the screen is currently being manipulated.
    /// </summary>
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

    #endregion

    ///This region has extra stuff used in making the project functional
    #region
    /// <summary>
    /// Resets the orientation of the molecule to its original settings. Affects EthaneWheelRotaion.cs.
    /// </summary>
    public void ResetOrientation()
    {
        Debug.Log("Reset Orientation Called");

        HeightHandler.SetDriveVale(0.0f);
        RotHandler.SetDriveVale(0.0f);
        molecule.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        destoySecondMolecule();

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

    /// <summary>
    /// Use the LabelToggler to toggle the labels on the current molecule
    /// </summary>
    public void ToggleLabels()
    {
        LabelToggler = CurrentMolecule.GetComponent<LabelToggler>();
        LabelToggler.Toggle();
    }
    #endregion
}

