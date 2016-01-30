using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

[Serializable]
public class Wheel : MonoBehaviour {
	public Spell spell;
	public bool available;

	public Sprite[] speedSprites;
	public Sprite[] powerSprites;
	public Sprite[] balanceSprites;

	// When the Wheel is initialized
	void Start() {
		Randomize ();
	}

	/// <summary>
	/// Get the next random spell.
	/// </summary>
	public void Randomize() {
		spell = new Spell ((SpellType)UnityEngine.Random.Range(0, 3), 1);
		Debug.Log ("New spell: " + spell.type);
		updateSprite ();
	}

	/// <summary>
	/// Stack the two spells of the type onto each other.
	/// </summary>
	/// <param name="next">The spell to stack on the top.</param>
	/// <returns>True if the stacking is successful</returns>
	public bool Stack(Spell next) {
		if (spell.type != next.type || spell.level + next.level > 3) {
			Debug.Log ("Stack failed");
			updateSprite ();
			return false;
		} else {
			Debug.Log ("Stack success");
			spell.level += next.level;
			updateSprite ();
			return true;
		}
	}

	public void updateSprite() {
		switch (spell.type) {
		case SpellType.Speed:
			gameObject.GetComponentInChildren<Image> ().sprite = speedSprites [spell.level - 1];
			break;
		case SpellType.Power:
			gameObject.GetComponentInChildren<Image> ().sprite = powerSprites [spell.level - 1];
			break;
		case SpellType.Balance:
			gameObject.GetComponentInChildren<Image> ().sprite = balanceSprites [spell.level - 1];
			break;
		}
	}

}