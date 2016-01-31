using UnityEngine;
using System.Collections;
using CnControls;

public class TestJoystick : MonoBehaviour {


	
	// Update is called once per frame
	void Update () {
		float y1 = CnInputManager.GetAxis ("v1");
		float y2 = CnInputManager.GetAxis ("v2");
		float y3 = CnInputManager.GetAxis ("v3");
		float y4 = CnInputManager.GetAxis ("v4");

		Debug.Log (y1 + " / " + y2 +" / " + y3 + " / " + y4);
	}
}
