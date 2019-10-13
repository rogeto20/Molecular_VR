using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRCameraController : MonoBehaviour {

	public Transform Target;
	public bool IsLocked = false;

	public float Distance = 20.0f;
	public float MaxDistance = 20.0f;
	public float MinDistance = 3.0f;
	public float ZoomSpeed = 0.95f;

	public float XSpeed = 120.0f;
	// public float YSpeed = 123.0f;

	public const int YMinLimit = -723;
	public const int YMaxLimit = 877;

	private float X = 22.0f;
	private float Y = 33.0f;

	void Start ( ) {
		
	}

	/// <summary>
	/// Called once per frame and the end of the frame
	/// 
	/// Handle all phone upate and rotation here based on
	/// the gyroscope and finger presses
	/// </summary>
	void LateUpdate ( ) {
		if ( IsLocked ) {
			return;
		}

		// Quaternion gyro = Input.gyro.attitude;
		// Vector3 accel = Input.acceleration;

		// transform.localRotation = new Quaternion ( gyro.x, gyro.y, -gyro.z, -gyro.w );

		if ( Target ) {
			Quaternion referenceRotation = Quaternion.identity;
			Quaternion deviceRotation = DeviceRotation.Get ( );
		
			Quaternion pitchQuat = Quaternion.Inverse (
				                       Quaternion.FromToRotation ( referenceRotation * Vector3.right, 
					                       deviceRotation * Vector3.right )
			                       );
			Quaternion yawQuat = Quaternion.Inverse (
				                     Quaternion.FromToRotation ( referenceRotation * Vector3.up, 
					                     deviceRotation * Vector3.up )
			                     );
			Vector3 pitchVect = ( pitchQuat * deviceRotation ).eulerAngles;
			Vector3 yawVect = ( yawQuat * deviceRotation ).eulerAngles;

			Y = pitchVect.x;
			// X = yawVect.y;

			Y = ClampAngle ( Y, YMinLimit, YMaxLimit );


			if ( Input.touchCount == 1 ) {
				Touch touch = Input.GetTouch ( 0 );
				float xTouchPos = touch.position.x;

				if ( Screen.width / 2 < xTouchPos ) {
					// Touching left
					X += XSpeed * Time.deltaTime;
				} else {
					// Touching right
					X -= XSpeed * Time.deltaTime;
				}
			} else if ( Input.touchCount == 2 ) {
				// Handle pinch

				Touch touchZero = Input.GetTouch ( 0 );
				Touch touchOne = Input.GetTouch ( 1 );

				Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
				Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
			
				float prevTouchDeltaMag = ( touchZeroPrevPos - touchOnePrevPos ).magnitude;
				float touchDeltaMag = ( touchZero.position - touchOne.position ).magnitude;

				float deltaMagDiff = prevTouchDeltaMag - touchDeltaMag;

				Distance += ( deltaMagDiff * ZoomSpeed * 0.02f );
			}

			if ( Distance < MinDistance ) {
				Distance = MinDistance;
			} else if ( Distance > MaxDistance ) {
				Distance = MaxDistance;
			}

			// Debug.Log ( X + " " + Y );
			// Debug.Log ( Distance );

			Quaternion rotation = Quaternion.Euler ( Y, X, 0.0f );
			Vector3 position = rotation * new Vector3 ( 0.0f, 0.0f, -Distance ) + Target.position;

			transform.rotation = rotation;
			transform.position = position;
		}
	}

	/// <summary>
	/// Simple clamp function
	/// </summary>
	/// <returns>The angle.</returns>
	/// <param name="p_angle">P angle.</param>
	/// <param name="p_min">P minimum.</param>
	/// <param name="p_max">P max.</param>
	public static float ClampAngle ( float p_angle, float p_min, float p_max ) {
		if ( p_angle < -360.0f ) {
			p_angle += 360.0f;
		}
		if ( p_angle > 360.0f ) {
			p_angle -= 360.0f;
		}
		return Mathf.Clamp ( p_angle, p_min, p_max );
	}

	/// <summary>
	/// Idk why we have this
	/// </summary>
	public void Reset ( ) {
		Debug.Log ( "Reset" );
	}
}
