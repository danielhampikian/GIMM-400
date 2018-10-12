using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SetupLocalPlayer : NetworkBehaviour {

	// Use this for initialization
	void Start () {
        if(!isLocalPlayer){
            GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;
            GetComponent<FireProjectile>().enabled = false;
            GetComponentInChildren<Camera>().enabled = false;

        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
