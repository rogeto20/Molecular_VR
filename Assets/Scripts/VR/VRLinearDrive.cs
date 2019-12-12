using UnityEngine;
using System.Collections;

// ===============================
// AUTHOR: Craig Milby
// DATE: 25 October, 2017
// PURPOSE: Linear drive provides an interpolation between [0, 1] given
//			an arbitrary start and end point.
// ===============================
public class VRLinearDrive : MonoBehaviour {

	public SteamVR_TrackedObject TrackedObject;

	private SteamVR_Controller.Device HandController {
		get {
			return SteamVR_Controller.Input ( ( int ) TrackedObject.index );
		}
	}

	public Transform StartPosition;
	public Transform EndPosition;
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

    public void SetInitialPosition ( )
    {
        UpdateLinearMapping(StartPosition);
    }

    public void SetDriveValue ( float p_value )
    {
        LinearMapping.value = p_value;
        UpdateLinearMapping(transform);
    }

	/// <summary>
	/// Call sometime before start of first frame
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
			UpdateLinearMapping ( transform );
		}
	}

    //called every frame
	void Update ( ) {
		HandHoverUpdate ( );

		if ( MaintainMomentum && MappingChangeRate != 0.0f ) {
			MappingChangeRate = Mathf.Lerp ( MappingChangeRate, 0.0f, MomentumDampenRate * Time.deltaTime );
			LinearMapping.value = Mathf.Clamp01 ( LinearMapping.value + ( MappingChangeRate * Time.deltaTime ) );

			if ( RepositionGameObject ) {
				transform.position = Vector3.Lerp ( StartPosition.position, EndPosition.position, LinearMapping.value );
			}
		}
	}

	/// <summary>
	/// Update the linear mapping value based on the transforn
	/// of an arbitrary object
	/// </summary>
	/// <param name="p_trans">P trans.</param>
	private void UpdateLinearMapping ( Transform p_trans ) {
		PrevMapping = LinearMapping.value;
		LinearMapping.value = Mathf.Clamp01 ( InitialMappingOffset + CalculateLinearMapping ( p_trans ) );

		MappingChangeSamples [ SampleCount % MappingChangeSamples.Length ] = ( 1.0f / Time.deltaTime ) * ( LinearMapping.value - PrevMapping );
		SampleCount++;

		if ( RepositionGameObject ) {
			transform.position = Vector3.Lerp ( StartPosition.position, EndPosition.position, LinearMapping.value );
		}
	}

	/// <summary>
	/// Calculate the value [0, 1] based on the
	/// position value of an arbitrary transform
	/// </summary>
	/// <returns>The linear mapping.</returns>
	/// <param name="p_trans">P trans.</param>
	private float CalculateLinearMapping ( Transform p_trans ) {
		Vector3 direction = EndPosition.position - StartPosition.position;
		float length = direction.magnitude;
		direction.Normalize ( );

		Vector3 displace = p_trans.position - StartPosition.position;
		return Vector3.Dot ( displace, direction ) / length;
	}

	/// <summary>
	/// Handle when the controller is hovering over
	/// a linear drive component
	/// </summary>
	private void HandHoverUpdate ( ) {
		if ( HandController.GetHairTriggerDown ( ) && Hoverable.IsHovering ) {
			InitialMappingOffset = LinearMapping.value - CalculateLinearMapping ( TrackTransform );
			SampleCount = 0;
			MappingChangeRate = 0.0f;
			WasHovering = true;
		}

		if ( HandController.GetHairTriggerUp ( ) ) {
			CalculateMappingChangeRate ( );
			WasHovering = false;
		}
	
		if ( HandController.GetHairTrigger ( ) && WasHovering ) {
			UpdateLinearMapping ( TrackTransform );
		}
	}

	/// <summary>
	/// Continue to move object for small slowdown
	/// when enabled
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

