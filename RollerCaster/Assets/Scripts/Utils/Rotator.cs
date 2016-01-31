using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

	[SerializeField] float RotationSpeed = 10;


	void Update () {

		transform.Rotate(0,0,RotationSpeed * Time.deltaTime);
	}
}
