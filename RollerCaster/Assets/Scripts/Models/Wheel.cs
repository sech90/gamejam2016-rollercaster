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
	public Sprite joystickSprite;

	private bool available = true;
	private bool _isControlling = false;
	private float cooldown;

	private SimpleJoystick joystick;


	// When the Wheel is initialized
	void Start() {
		onReady (0);
		Randomize ();
		joystick = GetComponentInChildren<SimpleJoystick>(true);
		//joystick.gameObject.SetActive(false);
	}

	public void onReady (int side) {
		if (side == 0) {
			gameObject.GetComponent<RawImage> ().texture = Resources.Load<Texture> ("texture_roller_frame_pink");
		} else {
			gameObject.GetComponent<RawImage> ().texture = Resources.Load<Texture> ("texture_roller_frame_blue");
		}
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
		
	public void CastSpell(SpellType spellType, int level, int spellId) {
		Debug.Log ("CastSpell() is called.");

		// Online communication

		joystick.gameObject.SetActive (true);
		_isControlling = true;
		updateSprite ();
	}

	/// <summary>
	/// API exposed to the server to control a spell
	/// </summary>
	/// <param name="spellId">Spell identifier.</param>
	/// <param name="joystickValue">Joystick value.</param>
	public void Move(int spellId, float joystickValue) {
		Debug.Log ("Spell " + spellId + " value " + joystickValue);
		// NETWORK PLAYER
	}

	public void OnSelfDestroyed() {
		DisableWheel (4);
	}

	public void OnSpellDestroyed(Spell s) {
		if(s.id != spell.id)
			return;
		else DisableWheel (4);
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
		if (_isControlling) {
			gameObject.GetComponentInChildren<Image> ().sprite = joystickSprite;
		} else if (available) {
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