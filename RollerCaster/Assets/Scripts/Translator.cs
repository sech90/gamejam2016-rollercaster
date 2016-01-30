using UnityEngine;
using System.Collections;

public class Translator : MonoBehaviour {
	public float xspeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (new Vector3 (xspeed, 0, 0));
	}
}
