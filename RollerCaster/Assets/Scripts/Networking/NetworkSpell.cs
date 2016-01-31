using UnityEngine;
using System.Collections;
using CnControls;

public class NetworkSpell : MonoBehaviour {


	private string hAxis;
	private string yAxis;
	private bool _enabled;

	public void EnableControl(SimpleJoystick stick){
		hAxis = stick.HorizontalAxisName;
		yAxis = stick.VerticalAxisName;
		_enabled = true;
	}

	public void SetSpell(Spell s){
		
	}




	/*void Update(){
		if(!_enabled)
			return;


	}*/

}
