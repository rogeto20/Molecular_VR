using UnityEngine;
using UnityEngine.UI;

public class MultilineInputField : MonoBehaviour {

	private InputField Input;

	void Awake ( ) {
		Input = GetComponent<InputField> ( );
		Input.lineType = InputField.LineType.MultiLineNewline;
		// Input.multiLine = true;
	}

	void Update ( ) {
		
	}
}
