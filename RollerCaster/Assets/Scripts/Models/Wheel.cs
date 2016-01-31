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

	public Side wheelSide;
	public bool ready = false;

	// When the Wheel is initialized
	void Start() {

		//onReady (0);
//		Randomize ();
//		joystick = GetComponentInChildren<SimpleJoystick>(true);
//		joystick.gameObject.SetActive(false);
	}
		
	public void onReady (Side side) {
		wheelSide = side;
		if (side == Side.LEFT) {
			gameObject.GetComponent<RawImage> ().texture = Resources.Load<Texture> ("texture_roller_frame_pink");
		} else {
			gameObject.GetComponent<RawImage> ().texture = Resources.Load<Texture> ("texture_roller_frame_blue");
		}
		Debug.Log ("Side: " + (int)side);
		Randomize ();
		joystick = GetComponentInChildren<SimpleJoystick>(true);
		joystick.gameObject.SetActive(false);
		ready = true;
	}

	void Update() {
		if (!ready) {
			if (NetworkPlayer.current != null) {
				onReady(NetworkPlayer.current.side);
			} else 
				return;
		}
		if (!available) {
			cooldown -= 1f * Time.deltaTime;
			if (cooldown <= 0) {
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
		EnableWheel();
	}

	public void CastSpell(SpellType type, int level, int id){
		Debug.Log ("Side " + wheelSide + " is casting a spell");
		joystick.gameObject.SetActive(true);
		_isControlling = true;
		NetworkPlayer.current.CastSpell(type, level, id);
		updateSprite ();
	}


	/// <summary>
	/// API exposed to the server to control a spell
	/// </summary>
	/// <param name="spellId">Spell identifier.</param>
	/// <param name="joystickValue">Joystick value.</param>
	public void Move(int spellId, float joystickValue) {
		NetworkPlayer.current.Move(spellId, joystickValue);
	}

	public void OnSelfDestroy() {
		Debug.Log (name + " is destroyed.");
		DisableWheel(4);
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
		if (_isControlling) {
			joystick.gameObject.SetActive(false);
			_isControlling = false;
		}
		available = false;
		cooldown = time;
		updateSprite ();
	}

	public void EnableWheel() {
		available = true;
		updateSprite ();
	}

}