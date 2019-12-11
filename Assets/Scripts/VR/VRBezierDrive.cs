using UnityEngine;
using System.Collections;

// ===============================
// AUTHOR: Craig Milby
// DATE: 25 October, 2017
// PURPOSE: VRBezierCurve allows the user to drag objects along a
// 			bezier curve and then interpolates the value on that
// 			curve between [0, 1]
// ===============================
public class VRBezierDrive : MonoBehaviour {

	public SteamVR_TrackedObject TrackedObject;

	private SteamVR_Controller.Device HandController {
		get {
			return SteamVR_Controller.Input ( ( int ) TrackedObject.index );
		}
	}

	public BezierCurve Curve;
	public Transform TrackTransform;

	public VRLinearMapping LinearMapping;
	public VRHoverable Hoverable;

	public bool RepositionGameObject = true;
	public bool MaintainMomentum = true;
	public float MomentumDampenRate = 5.0f;

	private float InitialMappingOffset;
	private int NumMappingChangeSamples = 5;
	private float[] MappingChangeSamples;
	private float PrevMapping = 0.0f;
	private float MappingChangeRate;
	private int SampleCount = 0;

	[HideInInspector]
	public bool WasHovering = false;

	void Awake ( ) {
		MappingChangeSamples = new float [ NumMappingChangeSamples ];
	}

	/// <summary>
	/// Called sometime before the first frame
	/// </summary>
	void Start ( ) {
		if ( LinearMapping == null ) {
			LinearMapping = GetComponent<VRLinearMapping> ( );
		}

		if ( LinearMapping == null ) {
			LinearMapping = gameObject.AddComponent<VRLinearMapping> ( );
		}

		InitialMappingOffset = LinearMapping.value;

		if ( RepositionGameObject ) {
			transform.position = Curve.GetPoint ( LinearMapping.value );
		}
	}

	/// <summary>
	/// Called every frame
	/// </summary>
	void Update ( ) {
		HandHoverUpdate ( );
	}

	/// <summary>
	/// Update the position based on the mapping value
	/// </summary>
	public void UpdatePosition ( ) {
		transform.position = Curve.GetPoint ( LinearMapping.value );
	}

	/// <summary>
	/// Update the linear mapping value based on the position
	/// of an arbitrary transform (point)
	/// </summary>
	/// <param name="p_trans">P trans.</param>
	private void UpdateMapping ( Transform p_trans ) {
		PrevMapping = LinearMapping.value;
		LinearMapping.value = Mathf.Clamp01 ( InitialMappingOffset + CalculateMapping ( p_trans ) );

		MappingChangeSamples [ SampleCount % MappingChangeSamples.Length ] = ( 1.0f / Time.deltaTime ) * ( LinearMapping.value - PrevMapping );
		SampleCount++;

		if ( RepositionGameObject ) {
			transform.position = Curve.GetPoint ( LinearMapping.value );
		}
	}

	/// <summary>
	/// Do the actual math to calculate the bezier mapping
	/// </summary>
	/// <returns>The mapping.</returns>
	/// <param name="p_trans">P trans.</param>
	private float CalculateMapping ( Transform p_trans ) {
		Vector3 direction = Curve.Points [ 2 ] - Curve.Points [ 0 ];
		float length = direction.magnitude;
		direction.Normalize ( );

		Vector3 displace = p_trans.position - Curve.Points [ 0 ];
		return Vector3.Dot ( displace, direction ) / ( length * 0.6f );
	}

	/// <summary>
	/// Updates when the controller is hovering
	/// </summary>
	private void HandHoverUpdate ( ) {
		if ( HandController == null ) {
            Debug.Log("Controller Not Found");
			return;
		}

		if ( HandController.GetHairTriggerDown ( ) && Hoverable.IsHovering ) {
			InitialMappingOffset = LinearMapping.value - CalculateMapping ( TrackTransform );
			SampleCount = 0;
			MappingChangeRate = 0.0f;
			WasHovering = true;
		}

		if ( HandController.GetHairTriggerUp ( ) ) {
			CalculateMappingChangeRate ( );
			WasHovering = false;
		}

		if ( HandController.GetHairTrigger ( ) && WasHovering ) {
			UpdateMapping ( TrackTransform );
		}
	}

	/// <summary>
	/// Continue to move the object over time depending on 
	/// if that's what we want
	/// </summary>
	private void CalculateMappingChangeRate ( ) {
		MappingChangeRate = 0.0f;
		int mappingSamplesCount = Mathf.Min ( SampleCount, MappingChangeSamples.Length );
		if ( mappingSamplesCount != 0 ) {
			for ( int i = 0; i < mappingSamplesCount; i++ ) {
				MappingChangeRate += MappingChangeSamples [ i ];
			}
			MappingChangeRate /= mappingSamplesCount;
		}
	}
}

