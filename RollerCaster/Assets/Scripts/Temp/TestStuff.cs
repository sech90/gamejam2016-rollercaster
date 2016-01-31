using UnityEngine;
using System.Collections;
using CnControls;

public class TestStuff : MonoBehaviour {

	public int Vertices = 100;
	private MeshFilter filter;





	public void Print(string mex){
		Debug.Log(mex);
	}

	public void PrintInput(){
		float x = CnInputManager.GetAxis("Horizontal");
		float y = CnInputManager.GetAxis("Vertical");

		Debug.Log(x+" "+y);
	}
}
