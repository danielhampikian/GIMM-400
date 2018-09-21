using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SetupPlayer : NetworkBehaviour {

    public GameObject ovrStuff;
    public bool isVRPlayer;

	// Use this for initialization
	void Start () {
		if (isLocalPlayer && isVRPlayer)
        {
            //do local player setup
            Camera.main.enabled = false;
            GetComponent<SimpleMovement>().enabled = false;
            
        }
        else
        {
            Destroy(ovrStuff);
            ovrStuff.gameObject.SetActive(false);
            GetComponentInChildren<OVRManager>().enabled = false;
            GetComponentInChildren<OVRCameraRig>().enabled = false;
            GetComponentInChildren<CharacterController>().enabled = false;
            GetComponentInChildren<OVRPlayerController>().enabled = false;
            GetComponent<SimpleMovement>().enabled = true;
            Camera.main.enabled = true;
            //Either destroy the game object holding ovr stuff
            //or deactivate all ovr scripts 
        }
	}
}
