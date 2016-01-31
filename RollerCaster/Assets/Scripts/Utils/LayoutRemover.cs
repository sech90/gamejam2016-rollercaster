using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LayoutRemover : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke ("removeLayout", 0.1f);
	}

	private void removeLayout () {
		GetComponent<HorizontalLayoutGroup> ().enabled = false;
	}
}
