using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : MonoBehaviour {

    public float speed = 4;
    Vector3 pos;
	void Start () {
        pos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        pos.x += Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        pos.z += Input.GetAxis("Vertical") * speed * Time.deltaTime;
        transform.position = pos;
    }
}
