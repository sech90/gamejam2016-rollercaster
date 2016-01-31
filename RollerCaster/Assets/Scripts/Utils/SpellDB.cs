using UnityEngine;
using System.Collections;
using System;

public class SpellDB : MonoBehaviour {

	public Spell PowerSpell;
	public Spell SpeedSpell;
	public Spell BalanceSpell;

	public static SpellDB _instance;

	void Awake(){
		_instance = this;
	}

	public static Spell GetSpell(SpellType type, int level){

		Spell s = new Spell(type, level);
		Spell refSpell;

		switch(type){
		case SpellType.Power:
			refSpell = _instance.PowerSpell;
			break;
		case SpellType.Balance:
			refSpell = _instance.BalanceSpell;
			break;
		case SpellType.Speed:
			refSpell = _instance.SpeedSpell;
			break;
			default:
			refSpell = _instance.BalanceSpell;
			break;
		}

		s.baseSize = refSpell.baseSize;
		s.baseSpeed = refSpell.baseSpeed;
		s.baseDamage = refSpell.baseDamage;
		s.sprites = refSpell.sprites;
		s.prefab = refSpell.prefab;

		return s;
	}

	public static Spell GetRandom(){
		Array values = Enum.GetValues(typeof(SpellType));
		SpellType random = (SpellType)values.GetValue(UnityEngine.Random.Range(0,values.Length));

		return GetSpell(random, 1);
	}
}
