using UnityEngine;
using System.Collections;
using CnControls;

public class TestStuff : MonoBehaviour {

	public int Vertices = 100;
	private MeshFilter filter;

	void Start(){
		CalcBoundary();
		filter = GetComponentInChildren<MeshFilter>();
		//Debug.Log("Done "+Time.time);
	}

	void OnCollisionEnter2D(Collision2D coll){


		NetworkSpell spell = coll.gameObject.GetComponent<NetworkSpell>();
		if(spell == null)
			return;

		Vector2 contact;
		if(coll.contacts.Length > 0){
			Spell sp = SpellDB.GetSpell(SpellType.Balance, 1);
			contact = coll.contacts[0].point;
			Debug.Log(name+" collided with "+coll.gameObject.name+" in point "+contact);

			Utils.HoleInTheWall(filter,contact.x, 0, contact.y, sp.Damage);
			CalcBoundary();
		}
	}

	public void CalcBoundary(){
		Mesh m = gameObject.GetComponentInChildren<MeshFilter>().mesh;
		PolygonCollider2D coll = GetComponentInChildren<PolygonCollider2D>();

		coll.points = MeshExtrusion.GetBoundary(m,Vertices);
	}

	public void Print(string mex){
		Debug.Log(mex);
	}

	public void PrintInput(){
		float x = CnInputManager.GetAxis("Horizontal");
		float y = CnInputManager.GetAxis("Vertical");

		Debug.Log(x+" "+y);
	}
}
