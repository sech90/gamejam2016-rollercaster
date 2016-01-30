using System;
using System.Collections;

public class Wheel {
	public Spell spell;
	public bool available;
	public event Action onStateChange;

	/// <summary>
	/// Get the next random spell.
	/// </summary>
	public void Randomize() {
		spell = new Spell ((SpellType)_rand.Next (3), 1);
		if (onStateChange != null) {
			onStateChange ();
		}
	}

	/// <summary>
	/// Stack the two spells of the type onto each other.
	/// </summary>
	/// <param name="next">The spell to stack on the top.</param>
	/// <returns>True if the stacking is successful</returns>
	public bool Stack(Spell next) {
		if (spell.type != next.type) {
			return false;
		} else {
			spell.level++;
			return true;
		}
	}

	private Random _rand = new Random();
}