using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;

public class NetworkId : NetworkBehaviour {

	[SyncVar(hook="OnName")]
	public string pName;

	public event Action OnInitialized;
	private bool _initialized = false;

	public void AddInitializeListener(Action cb){
		if(_initialized)
			cb();
		else
			OnInitialized += cb;
	}

	public override void OnStartLocalPlayer ()
	{
		GetNetId();
		SetId();

		_initialized = true;
		if(OnInitialized != null)
			OnInitialized();
	}

	[Client]
	void GetNetId(){
		CmdTellServerTheId(MakeName());
	}

	string MakeName(){
		if(transform.position.x < 0)
			return "LEFT"+GetComponent<NetworkIdentity>().netId;
		return "RIGHT"+GetComponent<NetworkIdentity>().netId;
	}

	void SetId(){
		if(isLocalPlayer)
			name = MakeName();
		else{
			name = pName;
		}
	}

	void OnName(string n){
		Debug.Log(name+" onName "+n);
		if(!isLocalPlayer){
			name = n;
			pName = n;
			Utils.Log( "OnName set name of remote "+n);
			_initialized = true;
			if(OnInitialized != null)
				OnInitialized();
		}
	}

	void Update(){
		if(name == "" || name == "Player(Clone)")
			SetId();
	}

	[Command]
	void CmdTellServerTheId(string n){
		pName = n;
	}
}
