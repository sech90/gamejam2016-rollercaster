using System;
using System.Collections;

[Serializable]
public class Spell {
	public int id;
	public SpellType type;
	public int level;
	public int baseSize;
	public int baseSpeed;
	public float damage;

	public Wizard owner;

	public Spell (SpellType type, int level)
	{
		this.type = type;
		this.level = level;
	}

	/// <summary>
	/// Resolve the outcome of a collision with another spell.
	/// </summary>
	/// <param name="other">The spell being collided with</param>
	/// <returns>True if the spell survives</returns>
	public bool Resolve(Spell other) {
		if (level <= other.level) {
			return false;
		} else {
			level -= other.level;
			return true;
		}
	}

}

public enum SpellType {
	Balance,
	Speed,
	Power
}