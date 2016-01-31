using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using CnControls;

public class WheelUI : MonoBehaviour {

	public event Action<Spell> OnCast;

	private Spell spell;
	public Image img;
	private SimpleJoystick stick;

	void Start(){
		img = transform.GetChild(1).GetComponent<Image>();
		stick = GetComponentInChildren<SimpleJoystick>();

		stick.gameObject.SetActive(false);
		Roll();
	}

	public void Cast(){
		if(OnCast != null)
			OnCast(spell);

		Color c = img.color;
		c.a = 0.5f;
		img.color = c;

		stick.gameObject.SetActive(true);
	}
		
	public void Roll(){
		stick.gameObject.SetActive(false);
		img.color = Color.white;

		spell = SpellDB.GetRandom();
		img.sprite = spell.Sprite;
		Debug.Log("Getting "+spell.type+" sprite "+spell.Sprite.name);
	}

}
