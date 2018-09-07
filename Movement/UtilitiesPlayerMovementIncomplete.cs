using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilitiesPlayerMovementIncomplete : MonoBehaviour {

	public enum MoveType { VectorDirect, RotationDirect, Translate, Lerp, RigidBodyArrows, RigidBodyMouse}

    public MoveType moveType;

    public float speed = 10f;
    public float rotSpeed = 100f;
    public float lerpRotationSpeed = 2f;
    public float stoppingDistance = 1f;

    public LayerMask layerMask;
    public Vector3 goal;
    public Quaternion rot;

    Rigidbody rb;
    
	void Start () {
        goal = transform.position;
        rot = transform.rotation;
        rb = GetComponent<Rigidbody>();
	}
	
	void Update () {
        Vector3 position = transform.position;
		switch (moveType)
        {
            case MoveType.VectorDirect:
            //TODOL write code that get's input from player and assigns it to position.x and position.z with speed modifier
            //then modifiy the transforms position with the local position Vector variable
                break;
            case MoveType.RotationDirect:
            //TODO: write code where the horizontal axis from player input modifies the rotation of the transform with
            //the Quaternion.Euluer function.  Then modify the transorms rotation and position with speed and rotation speed
                break;
            case MoveType.Translate:
            //TODO: implement get mouse position below to get the rot variable from user input of a click that is used in Quaternion.Lerp
                getMousePosition();
                transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * rotSpeed);
                Vector3 direction = goal - transform.position;
                if(direction.magnitude > stoppingDistance)
                {
                    transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
                }
                break;
            case MoveType.Lerp:
                getMousePosition();
                transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * rotSpeed);
                if(Vector3.Distance(transform.position, goal) > stoppingDistance)
                {
                    transform.position = Vector3.Lerp(transform.position, goal, Time.deltaTime);
                }
                break;
        }
    }
    private void FixedUpdate()
    {
        switch (moveType)
        {
            case MoveType.RigidBodyArrows:
                goal = transform.forward;
                float translation = Input.GetAxis("Vertical") * speed;
                float rotation = Input.GetAxis("Horizontal") * rotSpeed;
                translation *= Time.deltaTime;
                rotation *= Time.deltaTime;
                Quaternion turn = Quaternion.Euler(0f, rotation, 0f);
               //TODO implement movement with rigidbody MovePosition and MoveRotation using translation and turn variables
                break;
            case MoveType.RigidBodyMouse:
                getMousePosition();
                Quaternion lerpRot = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * lerpRotationSpeed);
                //TODO: using stopping distance and Vector3.Distance, use MoveRotation and MovePosition and lerpRot and goal variables to move the rigid body towards the mouse position
                rb.MoveRotation(lerpRot);
                if(Vector3.Distance(transform.position, goal) > stoppingDistance)
                {
                    Vector3 lerpTarget = Vector3.Lerp(transform.position, goal, Time.deltaTime * speed);
                    rb.MovePosition(lerpTarget + transform.forward * speed * Time.deltaTime);
                }
                break;
        }
    }
    public void getMousePosition()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit, 10000, layerMask, QueryTriggerInteraction.Ignore))
            {
                goal = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                //Set rot global variable using the transform.position and the goal somehow (try Vector arithmetic and the Quaternion.LookRotation function)
            }
        }
    }
}
