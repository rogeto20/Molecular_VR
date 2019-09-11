using UnityEngine;
using System.Collections;

// ===============================
// AUTHOR: Craig Milby
// DATE: 25 October, 2017
// PURPOSE: Teleport does what it sounds like, it teleports the user around the
//			world since the world is bigger than the room you are in.
// ===============================
public class VRTeleport : MonoBehaviour {

	// Keep track of Camera and Head so we can teleport them
	// to the correct locations.
	public Transform CameraTransform;
	public Transform HeadTransform;

	// The prefab for the valid laser and reticle where it is pointing
	public GameObject LaserPrefab;
	public GameObject ReticlePrefab;

	// Use this if the player is pointing to an invalid
	// location (out of the world, on the molecule, etc... )
	public GameObject InvalidLaserPrefab;
	public GameObject InvalidReticlePrefab;

	// Masks for raycast so we know valid teleport locaitons
	public LayerMask TeleportMask;
	public LayerMask NonTeleportMask;

	// Instanced prefabs
	private GameObject Laser;
	private GameObject Reticle;
	private GameObject InvalidLaser;
	private GameObject InvalidReticle;

	private RaycastHit RayHit;

	private bool Teleport = false;

	private SteamVR_TrackedObject TrackedObject;

	private SteamVR_Controller.Device HandController {
		get {
			return SteamVR_Controller.Input ( ( int ) TrackedObject.index );
		}
	}

	void Awake ( ) {
		TrackedObject = GetComponent < SteamVR_TrackedObject> ( );
	}

	void Start ( ) {
		Laser = Instantiate ( LaserPrefab );
		Reticle = Instantiate ( ReticlePrefab );
		InvalidLaser = Instantiate ( InvalidLaserPrefab );
		InvalidReticle = Instantiate ( InvalidReticlePrefab );

		RayHit = new RaycastHit ( );
	}

	void Update ( ) {
		if ( HandController.GetPress ( SteamVR_Controller.ButtonMask.Touchpad ) ) {
			Teleport = false;

			if ( Physics.Raycast ( TrackedObject.transform.position, transform.forward, out RayHit, 100.0f ) ) {
				Teleport = !LayerMatchTest ( NonTeleportMask, RayHit.collider.gameObject );

				if ( Teleport ) {
					DisplayLaser ( true );
					DisplayInvalidLaser ( false );
				} else {
					DisplayLaser ( false );
					DisplayInvalidLaser ( true );
				}

				PointLaser ( RayHit.point, RayHit.distance );
			} else {
				InvalidLaser.SetActive ( true );
				DisplayLaser ( false );

				PointLaser ( transform.TransformPoint ( Vector3.forward * 100.0f ), 100.0f );
			}
		} else {
			DisplayLaser ( false );
			DisplayInvalidLaser ( false );

			if ( HandController.GetPressUp ( SteamVR_Controller.ButtonMask.Touchpad ) && Teleport ) {
				TeleportToNewPosition ( RayHit.point );
				DisplayLaser ( false );
			}
		}
	}

	private void DisplayLaser ( bool p_show ) {
		Laser.SetActive ( p_show );
		Reticle.SetActive ( p_show );
	}

	private void DisplayInvalidLaser ( bool p_show ) {
		InvalidLaser.SetActive ( p_show );
		InvalidReticle.SetActive ( p_show );
	}

	// In this we point the valid and invalid lasers to the same place.
	// We will hide one and show the other later
	private void PointLaser ( Vector3 p_hitPoint, float p_distance ) {
		Laser.transform.position = Vector3.Lerp ( TrackedObject.transform.position, p_hitPoint, 0.5f );
		Laser.transform.LookAt ( p_hitPoint );
		Laser.transform.localScale = new Vector3 ( Laser.transform.localScale.x, Laser.transform.localScale.y, p_distance );

		InvalidLaser.transform.position = Laser.transform.position;
		InvalidLaser.transform.LookAt ( p_hitPoint );
		InvalidLaser.transform.localScale = Laser.transform.localScale;

		Reticle.transform.position = p_hitPoint;
		InvalidReticle.transform.position = p_hitPoint;
	}

	// Teleport camera and head to correct locations
	private void TeleportToNewPosition ( Vector3 p_hitPoint ) {
		Vector3 difference = CameraTransform.position - HeadTransform.position;
		difference.y = 0.0f;
		CameraTransform.position = p_hitPoint + difference;
	}

	// Layer test for raycast
	private static bool LayerMatchTest ( LayerMask p_layers, GameObject p_obj ) {
		return ( ( 1 << p_obj.layer ) & p_layers ) != 0;
	}
}

