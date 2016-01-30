using UnityEngine;
using System.Collections;

public class SpellController : MonoBehaviour {
	public Spell spell;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// On collision with another spell
	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log (name + " fired");
		if (!other.gameObject)
			return;
		SpellController sc = other.gameObject.GetComponent<SpellController> ();
		if (!sc)
			return;
		Debug.Log ("pitching " + spell + " against " + sc.spell);
		if (!spell.Resolve (sc.spell)) {
			Destroy (gameObject);
		}
	}

	// Called when the spell game object is destroyed
	void OnDestroy() {
		Debug.Log ("Spell destroyed!");
	}
		
}
