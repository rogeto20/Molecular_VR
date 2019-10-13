using System.Collections.Generic;

using UnityEngine;

// ===============================
// AUTHOR: Craig Milby
// DATE: 25 October, 2017
// PURPOSE: SceneManager allows the program to pass paramters
// 			between scene loads.
// ===============================
public static class SceneManager {

	private static Dictionary<string, string> parameters;

	/// <summary>
	/// Load a scene and pass a dictionary as parameters
	/// </summary>
	/// <param name="p_sceneName">P scene name.</param>
	/// <param name="p_parameters">P parameters.</param>
	public static void Load ( string p_sceneName, Dictionary<string, string> p_parameters = null ) {
		if ( p_parameters != null ) {
			SceneManager.parameters = p_parameters;
		}

		UnityEngine.SceneManagement.SceneManager.LoadScene ( p_sceneName );
	}

	/// <summary>
	/// Load the specified scene with an option to be a VR scene.
	/// </summary>
	/// <param name="p_sceneName">P scene name.</param>
	/// <param name="p_vr">If set to <c>true</c> p vr.</param>
	public static void Load ( string p_sceneName, bool p_vr = false ) {
		if ( p_vr ) {
			SteamVR_LoadLevel.Begin ( p_sceneName );
		} else {
			UnityEngine.SceneManagement.SceneManager.LoadScene ( p_sceneName );
		}
	}

	/// <summary>
	/// Load a scene using a key value pair as strings
	/// </summary>
	/// <param name="p_sceneName">P scene name.</param>
	/// <param name="p_paramKey">P parameter key.</param>
	/// <param name="p_paramValue">P parameter value.</param>
	public static void Load ( string p_sceneName, string p_paramKey, string p_paramValue ) {
		SceneManager.parameters = new Dictionary<string, string> ( );
		SceneManager.parameters.Add ( p_paramKey, p_paramValue );
		UnityEngine.SceneManagement.SceneManager.LoadScene ( p_sceneName );
	}

	/// <summary>
	/// Get all parameters from the passed scene
	/// </summary>
	/// <returns>The scene parameters.</returns>
	public static Dictionary<string, string> GetSceneParameters ( ) {
		return parameters;
	}

	/// <summary>
	/// Get one paramter based on the key
	/// </summary>
	/// <returns>The parameter.</returns>
	/// <param name="p_paramKey">P parameter key.</param>
	public static string GetParameter ( string p_paramKey ) {
		if ( parameters == null ) {
			return "";
		}
		return parameters [ p_paramKey ];
	}

	/// <summary>
	/// Set a single parameter
	/// </summary>
	/// <param name="p_paramKey">P parameter key.</param>
	/// <param name="p_paramValue">P parameter value.</param>
	public static void SetParameter ( string p_paramKey, string p_paramValue ) {
		if ( parameters == null ) {
			SceneManager.parameters = new Dictionary<string, string> ( );
		}

		SceneManager.parameters.Add ( p_paramKey, p_paramValue );
	}

}
