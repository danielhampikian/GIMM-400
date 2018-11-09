using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ProjectileNetwork : NetworkBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "grabbable")
        {
            CmdDestroy(gameObject, collision.gameObject);
        }
    }
    [Command]
    public void CmdDestroy(GameObject projectile, GameObject collider)
    {
        if (NetworkServer.active)
        { 
            NetworkServer.Destroy(projectile);
            NetworkServer.Destroy(collider);
        }
    }
}
