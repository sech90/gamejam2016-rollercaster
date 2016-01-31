using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using CnControls;

[Serializable]
public class Wheel : MonoBehaviour {
	public Spell spell;
	public bool IsAvailable{get{return available;}}
	public Sprite blockedSprite;

	private bool available = true;
	private bool isControlMode = false;
	private float cooldown;

	private SimpleJoystick joystick;
	private bool _isControlling = false;



	// When the Wheel is initialized
	void Start() {
		Randomize ();
		joystick = GetComponentInChildren<SimpleJoystick>(true);
		joystick.gameObject.SetActive(false);
	}


	void Update() {
		if (!available) {
			cooldown -= 1f * Time.deltaTime;
			if (cooldown <= 0) {
				available = true;
				Randomize ();
			}
		}

		if (_isControlling) {
			float y = CnInputManager.GetAxis (joystick.VerticalAxisName);
			Move (spell.id, y);
		}
	}

	/// <summary>
	/// Get the next random spell.
	/// </summary>
	public void Randomize() {
		int id_aux = spell.id;
		spell = SpellDB.GetRandom();
		spell.id = id_aux;
//		Debug.Log ("New spell: " + spell.type);
		updateSprite ();
		available = true;
	}


	public void CastSpell(){
		joystick.gameObject.SetActive(true);

		_isControlling = true;
	}

	/// <summary>
	/// API exposed to the server to control a spell
	/// </summary>
	/// <param name="spellId">Spell identifier.</param>
	/// <param name="joystickValue">Joystick value.</param>
	public void Move(int spellId, float joystickValue) {
		Debug.Log ("Spell " + spellId + " value " + joystickValue);
	}

	public void OnSpellDestroyed(Spell s) {
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
			return false;
		} else {
			Debug.Log ("Stack success");
			updateSprite ();
			return true;
		}
	}

	public void updateSprite() {
		if (available) {
			gameObject.GetComponentInChildren<Image> ().sprite = spell.Sprite;
		} else {
			gameObject.GetComponentInChildren<Image> ().sprite = blockedSprite;
		}
	}

	public void DisableWheel(float time) {
		available = false;
		cooldown = time;
		updateSprite ();
	}

}