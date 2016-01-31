using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using CnControls;

[Serializable]
public class Wheel : MonoBehaviour {
	public Spell spell;
	public bool IsAvailable{get{return available;}}

	private SimpleJoystick joystick;
	private bool available;


	// When the Wheel is initialized
	void Start() {
		Randomize ();
		joystick = GetComponentInChildren<SimpleJoystick>();
		//joystick.gameObject.SetActive(false);
	}

	/// <summary>
	/// Get the next random spell.
	/// </summary>
	public void Randomize() {
		spell = SpellDB.GetRandom();
		Debug.Log ("New spell: " + spell.type);
		updateSprite ();
		available = true;
	}

	public void CastSpell(){
		
	}

	public void OnSpellDestroyed(Spell s){
		if(s.id != spell.id)
			return;


	}

	/// <summary>
	/// Stack the two spells of the type onto each other.
	/// </summary>
	/// <param name="next">The spell to stack on the top.</param>
	/// <returns>True if the stacking is successful</returns>
	public bool Stack(Spell next) {
		if (!spell.Stack(next)) {
			Debug.Log ("Stack failed");
			updateSprite ();
			return false;
		} else {
			Debug.Log ("Stack success");
			updateSprite ();
			return true;
		}
	}

	public void updateSprite() {
		
		gameObject.GetComponentInChildren<Image> ().sprite = spell.Sprite;

	}

}