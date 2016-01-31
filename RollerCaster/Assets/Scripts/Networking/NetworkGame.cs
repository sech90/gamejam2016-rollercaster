using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkGame : NetworkBehaviour {

	public override void OnStartServer (){
		GameObject wheels = GameObject.FindWithTag("Wheels");
		wheels.SetActive(true);
		Destroy(wheels);
		Utils.Log("Server is started");

		GameObject.FindWithTag("Wheels").SetActive(true);
	}
}
