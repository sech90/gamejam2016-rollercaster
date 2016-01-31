using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class Spell {
	public int id;
	public SpellType type;
	public int level = 1;
	public int baseSize = 1;
	public int baseSpeed = 1;
	public int baseDamage = 1;

	public Side side;

	public int Size{get{return baseSize * level;}}
	public int Speed{get{return baseSpeed * level;}}
	public int Damage{get{return baseDamage * level;}}
	public Sprite Sprite{get{return sprites[level-1];}}

	public GameObject prefab;
	public Sprite[] sprites;

	public NetworkPlayer owner;

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
	public bool Defeats(Spell other) {
		if (level <= other.level) {
			return false;
		} else {
			level -= other.level;
			return true;
		}
	}

	public bool Stack(Spell other){
		if(type != other.type || level + other.level > 3)
			return false;

		level += other.level;
		return true;
	}

}

public enum SpellType {
	Speed,
	Power,
	Balance
}