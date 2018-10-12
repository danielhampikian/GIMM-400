using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float speed = 5;

    void Start () {
		
	}
	
	void Update () {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
	}
}
