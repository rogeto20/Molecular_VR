using UnityEngine;
using System.Collections;

// ===============================
// AUTHOR: Craig Milby
// DATE: 25 October, 2017
// PURPOSE: VR button is like a normal button, but in VR. It has functionality
//			to call Unity Events as well as change colors depending on state.
// ===============================
public class VRButton : MonoBehaviour {

	public GameObject Parent;

	public Color NormalColor;
	public Color HighlightedColor;
	public Color PressedColor;

	public UnityEngine.Events.UnityEvent ButtonClick;

	private UnityEngine.UI.Image Image;
	public MeshRenderer Renderer;

	public VRHapticPulse Pulse;
	// private VRUIController Controller;

	void Awake ( ) {
		Image = Parent.GetComponent < UnityEngine.UI.Image > ( );

		if ( Image ) {
			Image.color = NormalColor;
		}

		if ( Renderer ) {
			Renderer.material.color = NormalColor;
		}

        
	}

	/*
	void Start ( ) {
		Controller = GameObject.FindObjectOfType < VRUIController > ( );
	}*/

	/// <summary>
	/// Do this when the controller starts to hover
	/// </summary>
	public void ButtonHoverStart ( ) {
        if (Image)
        {
            Image.color = HighlightedColor;
        }

        if (Renderer)
        {
            Renderer.material.color = HighlightedColor;
        }

        /*if ( Pulse ) {
			Pulse.Pulse ( );
		}*/
    }

	/// <summary>
	/// Do this when the controller ends hovering
	/// </summary>
	public void ButtonHoverEnd ( ) {

		if ( Image ) {
			Image.color = NormalColor;
		}

		if ( Renderer ) {
			Renderer.material.color = NormalColor;
		}
	}

	/// <summary>
	/// Do this when the controller is hovering
	/// and a trigger is pressed
	/// </summary>
	public void ButtonPressed ( ) {

        //if (Image.GetComponent<Text>.Equals("Load2?"))
        //{
        //    Image.color = HighlightedColor;
        //}


   


		ButtonClick.Invoke ( );
        // Controller.SendMessage ( "ButtonPressed" );

        if (Pulse)
        {
            Pulse.Pulse();
        }

        StartCoroutine ( _ButtonPressColorTransition ( ) );
	}

	/// <summary>
	/// Transition button colors when the button is pressed
	/// </summary>
	/// <returns>The press color transition.</returns>
	private IEnumerator _ButtonPressColorTransition ( ) {
		if ( Image ) {
			Image.color = PressedColor;
		}

		if ( Renderer ) {
			Renderer.material.color = PressedColor;
		}

        yield return new WaitForSeconds ( 0.1f );

		if ( Image ) {
			Image.color = HighlightedColor;
		}

		if ( Renderer ) {
			Renderer.material.color = HighlightedColor;
		}
	}
}

