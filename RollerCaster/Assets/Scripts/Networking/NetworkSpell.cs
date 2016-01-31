using UnityEngine;
using System.Collections;
using CnControls;
using System;
using UnityEngine.Networking;

public class NetworkSpell : NetworkBehaviour {

	[SerializeField] float Speed = 1;

	public event Action<NetworkSpell> OnDestroy;

	private string hAxis;
	private string yAxis;
	private bool _enabled;
	private Vector2 hDir;
	private Rigidbody2D body;
	SpriteRenderer sRend;
	private Spell spell;

	public Spell Spell{get{return spell;}}

	public override void OnStartClient (){																												
		Utils.Log("New Spell "+name+" created by "+spell.owner);

		if(isServer)
			return;

		GetComponent<Collider2D>().enabled = false;

	}

	public void EnableControl(SimpleJoystick stick){
		hAxis = stick.HorizontalAxisName;
		yAxis = stick.VerticalAxisName;
		_enabled = true;
	}

	public void DestroySpell(){
		_enabled = false;
		if(OnDestroy != null)
			OnDestroy(this);

		gameObject.SetActive(false);
		Destroy(gameObject, 0.1f);
	}

	void OnTriggerEnter2D(Collider2D coll){
		NetworkSpell nSpell = coll.gameObject.GetComponent<NetworkSpell>();
		if(nSpell == null)
			return;

		if(spell.Defeats(nSpell.spell)){
			sRend.sprite = spell.Sprite;
		}
		else
			DestroySpell();
		
	}

	[ClientRpc]
	public void RpcSetSpell(SpellType type, int level, Side side){
		SetSpell(SpellDB.GetSpell(type, level), side);
	}

	public void SetSpell(Spell s, Side side){
		Utils.Log("SetSpell called on "+(isServer ? (isClient ? "host" : "server") : "client"));
		if(side == Side.LEFT){
			hDir = Vector2.right;
			gameObject.layer = LayerMask.NameToLayer("SpellLeft");
		}
		else{
			hDir = Vector2.left;
			gameObject.layer = LayerMask.NameToLayer("SpellRight");
		}
		spell = s;
		_enabled = true;
		sRend = GetComponentInChildren<SpriteRenderer>();
		body = GetComponent<Rigidbody2D>();
		sRend.sprite = s.Sprite;

		if(!isClient)
			RpcSetSpell(s.type, s.level, side);
	}

	void Update(){
	//	Debug.Log(_enabled+" oo "+isClient+" "+(isServer ? (isClient ? "host" : "server") : "client")+" "+hasAuthority+" "+name);
		if(!_enabled || !hasAuthority)
			return;

		//Debug.Log("aa "+ isClient);
		float y = Input.GetAxis("Vertical");
		Vector2 dir = hDir + y * Vector2.up;
		dir.Normalize();

		transform.position = (Vector2)transform.position + dir * Speed * Time.deltaTime;
	}
}
