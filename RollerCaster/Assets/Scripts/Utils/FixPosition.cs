using UnityEngine;
using System.Collections;

public class FixPosition : MonoBehaviour {

	Vector3 pos;
	Quaternion rot;

	void Start () {
		rot = transform.rotation;
		pos = transform.position;
	}

	void Update () {
		if(pos != transform.position)
			transform.position = pos;

		if(rot != transform.rotation)
			transform.rotation = rot;
	}
}
