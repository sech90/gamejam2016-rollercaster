using UnityEngine;
using System.Collections;

public class Aura : MonoBehaviour {

	[SerializeField] int ColliderVertices = 100;
	[SerializeField] float RadiusMultiplier = 1;

	private MeshFilter filter;

	void Start(){
		filter = GetComponentInChildren<MeshFilter>();
		CalcBoundary();
	}

	void OnCollisionEnter2D(Collision2D coll){

		NetworkSpell spell = coll.gameObject.GetComponent<NetworkSpell>();
		if(spell == null)
			return;

		Utils.Log("Contact Aura "+name+" spell "+spell.name);

		Vector2 contact;
		if(coll.contacts.Length > 0){
			Spell sp = spell.Spell;
			contact = coll.contacts[0].point;

			Utils.HoleInTheWall(filter,contact.x, 0, contact.y, sp.Damage * RadiusMultiplier);
			CalcBoundary();

		}
		spell.DestroySpell();
	}

	public void CalcBoundary(){
		Mesh m = gameObject.GetComponentInChildren<MeshFilter>().mesh;
		PolygonCollider2D coll = GetComponentInChildren<PolygonCollider2D>();

		coll.points = MeshExtrusion.GetBoundary(m,ColliderVertices);
	}
}
