using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ===============================
// AUTHOR: Craig Milby
// DATE: 25 October, 2017
// PURPOSE: Rotate Labels keeps all labels on the
// 			molecules rotated towards the camera so
//			the user can always see them.
// ===============================
public class RotateLabels : MonoBehaviour {

	private Transform Origin;
	private MeshRenderer Renderer;

	private Camera Camera;
	private float Radius;

	private Vector3 LastPosition = Vector3.zero;

    public GameObject molecule;

    void Awake ( ) {
		Origin = transform.parent;
		Renderer = transform.parent.GetComponent<MeshRenderer> ( );
	}

	void Start ( ) {
		this.Camera = Camera.main;
		Radius = Renderer.bounds.extents.magnitude;	
	}

	/// <summary>
	/// Lerp a distance between the camer and the center of a molecule
	/// Move out that distance so the text is in front of the molecule
	/// Make sure the rotation looks directly at the camera
	/// </summary>
	void Update ( ) {
        
        if ( LastPosition == Camera.transform.position ) {
			return;
		}

        //molecule = GameObject.FindGameObjectWithTag("Mol");
       // float size = molecule.transform.position.x;
        Radius = Renderer.bounds.extents.magnitude;
        //Debug.Log(size);


        float distance = Vector3.Distance ( Origin.position, Camera.transform.position ); 
		transform.position = Vector3.Lerp ( Origin.position, Camera.transform.position, Mathf.InverseLerp ( 0.0f, distance, Radius) );
		transform.rotation = Quaternion.LookRotation ( Camera.transform.forward );
		LastPosition = Camera.transform.position;



        //foreach (TextMesh t in MoleculeCreateScript.textMeshList)
        //{

        //    //t.transform.LookAt(Camera.main.transform);
        //    transform.rotation = Quaternion.LookRotation(Camera.transform.forward);
        //    //Debug.Log(t.text);
        //}


    }
}
