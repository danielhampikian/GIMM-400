using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAIMove : MonoBehaviour {

    public float speed = 4;
    public float detectRadius = 2f;
    public int index;
    public Transform target;
    public GameObject[] navpoints;

	void Start () {
        navpoints = GameObject.FindGameObjectsWithTag("navpoint");
	}
	
	// Update is called once per frame
	void Update () {
        if (Vector3.Distance(transform.position, target.position) > detectRadius)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime);
        }
        else
        {
            Debug.Log("Need new target...");
        }
	}
}