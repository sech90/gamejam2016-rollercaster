using UnityEngine;
using System.Collections;
using System;

public class InnerAura : MonoBehaviour {

	public event Action OnCoreHit;

	void OnCollisionEnter2D(Collision2D coll){

		NetworkSpell spell = coll.gameObject.GetComponent<NetworkSpell>();
		if(spell == null)
			return;

		if(OnCoreHit != null)
			OnCoreHit();
	}

}
