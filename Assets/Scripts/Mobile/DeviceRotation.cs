using UnityEngine;
using System.Collections;

// ===============================
// AUTHOR: Craig Milby
// DATE: 25 October, 2017
// PURPOSE: Device rotation is a wrappper class for
// 			phone gyroscopes
// ===============================
public static class DeviceRotation {

	private static bool GyroInitialized = false;

	/// <summary>
	/// Make sure out platform even supports gyroscopes
	/// 
	/// Old phones or computers will not
	/// </summary>
	/// <value><c>true</c> if has gyroscope; otherwise, <c>false</c>.</value>
	public static bool HasGyroscope {
		get {
			return SystemInfo.supportsGyroscope;
		}		
	}

	/// <summary>
	/// Get the gyroscope rotation
	/// </summary>
	public static Quaternion Get ( ) {
		if ( !GyroInitialized ) {
			InitGyro ( );
		}

		return HasGyroscope ? ReadGyroscopeRotation ( ) : Quaternion.identity;
	}

	/// <summary>
	/// Initialize the gyroscope for iOS or Android platofrms
	/// </summary>
	private static void InitGyro ( ) {
		if ( HasGyroscope ) {
			Input.gyro.enabled = true;
			Input.gyro.updateInterval = 0.0167f;
		}
		GyroInitialized = true;
	}

	/// <summary>
	/// Read the rotation and handle correct orientation for both
	/// iOS and Android platforms
	/// </summary>
	/// <returns>The gyroscope rotation.</returns>
	private static Quaternion ReadGyroscopeRotation ( ) {
		return new Quaternion ( 0.5f, 0.5f, -0.5f, 0.5f ) * Input.gyro.attitude * new Quaternion ( 0, 0, 1, 0 );
	}
}

