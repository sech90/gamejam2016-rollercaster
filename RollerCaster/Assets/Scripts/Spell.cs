using System;
using System.Collections;

[Serializable]
public class Spell {
	public int id;
	public Type type;
	public int level;
	public int baseSize;
	public int baseSpeed;

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

public enum Type {
	Balance,
	Speed,
	Power
}