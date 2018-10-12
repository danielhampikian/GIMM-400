using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FireProjectile : NetworkBehaviour {

    public GameObject projectilePrefab;
    GameObject instantiatedProjectile;
    public Transform projectileLaunchPoint;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.F) && (isLocalPlayer)){
            CmdFire();
        }
	}
    [Command]
    void CmdFire()
    {
        instantiatedProjectile = Instantiate(projectilePrefab, projectileLaunchPoint.position, transform.rotation);
        if (NetworkServer.active)
        {
            NetworkServer.Spawn(instantiatedProjectile);
           
        }

        
    }
}
