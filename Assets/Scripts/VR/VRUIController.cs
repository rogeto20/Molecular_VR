using UnityEngine;
using System.Collections;

// ===============================
// AUTHOR: Craig Milby
// DATE: 25 October, 2017
// PURPOSE: This hamdles the menu for selecting molecules in the VR world. It loads
//			new molecules as well as updating them to the correct height and rotation
//			when loaded.
// ===============================
public class VRUIController : MonoBehaviour {

	public SteamVR_TrackedObject LeftHand;
	public Transform LeftHandTransform;

	public SteamVR_TrackedObject RightHand;
	public Transform RightHandTransform;

	private GameObject CurrentMolecule;
	private LabelToggler LabelToggler;

	public GameObject MoleculeHolder;

	public float MinHeight = 1.0f;
	public float MaxHeight = 5.0f;

	private float LastHeight = 0.0f;
	private float LastRotation = 0.0f;

    public VRMolHeightHandler HeightHandler;
    public VRMolRotationHandler RotHandler;

    void Awake ( ) {
		
	}

	void Start ( ) {
		LoadMolecule ( "Methane" );
	}

	public void HandleMoleculeSelectionClick ( string p_molecule ) {
		Destroy ( CurrentMolecule );
		LoadMolecule ( p_molecule );
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

                LabelToggler = CurrentMolecule.GetComponent<LabelToggler>();
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

    public void ResetOrientation ( ) {
        HeightHandler.SetDriveVale(0.0f);
        RotHandler.SetDriveVale(0.0f);

        CurrentMolecule.transform.rotation = Quaternion.identity;
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
		LabelToggler.Toggle ( );
	}

	public void ToggleLabels ( bool p_toggle ) {
		LabelToggler.Toggle ( p_toggle );
	}
}

