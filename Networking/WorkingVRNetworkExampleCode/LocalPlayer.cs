using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using InputTracking = UnityEngine.XR.InputTracking;
using Node = UnityEngine.XR.XRNode;

public class LocalPlayer : NetworkBehaviour {

    public GameObject OvrCam;
    public Camera camL;
    public Camera camR;
    public Vector3 pos;
    public Transform rightHandAnchor;
    public Transform leftHandAnchor;
    public float speed = 5;
    public Animator anim;
    public Transform spawnPos;
    public GameObject prefabInstance;
    public GameObject projectilePrefab;

    void Start () {
        anim = GetComponentInChildren<Animator>();
        pos = transform.position;
    }

    [Command]
    public void CmdFireProjectile()
    {
        if (NetworkServer.active)
        {
            prefabInstance = Instantiate(projectilePrefab, spawnPos.position, spawnPos.transform.rotation);
            prefabInstance.GetComponent<Rigidbody>().velocity = spawnPos.transform.forward * 2;
            NetworkServer.Spawn(prefabInstance);
        }
    }
    public void Fire(float fireRate)
    {
        CmdFireProjectile();
    }
    void Update () {
        if (!isLocalPlayer)
        {
            if(OvrCam != null) { 
            Destroy(OvrCam);
            }
        }
        else
        {
            if (camL != null)
            {
                if (camL.tag != "MainCamera")
                {
                    camL.tag = "MainCamera";
                    camL.enabled = true;
                }
                if (camR.tag != "MainCamera")
                {
                    camR.tag = "MainCamera";
                    camR.enabled = true;
                }
            }

            if (OVRInput.Get(OVRInput.Button.One))
            {
                Fire(1);
            }
            if (OVRInput.Get(OVRInput.Button.Two))
            {
                Fire(1);
            }
            if (OVRInput.Get(OVRInput.Button.Three))
            {
                Fire(1);
            }
            if (OVRInput.Get(OVRInput.Button.Four))
            {
                Fire(1);
            }

            //handle animations
            if (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).x != 0 || OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y != 0)
            {
                anim.SetBool("Idle", false);
                GetComponent<LocalAnimationControl>().CmdUpdateAnim("run");
            }
            else
            {
                anim.SetBool("Idle", true);
                GetComponent<LocalAnimationControl>().CmdUpdateAnim("idle");

            }

            //"hands" need to put in hand models and animations, but this is how to get the position:
            leftHandAnchor.localRotation = InputTracking.GetLocalRotation(Node.LeftHand);
            rightHandAnchor.localRotation = InputTracking.GetLocalRotation(Node.RightHand);
            leftHandAnchor.localPosition = InputTracking.GetLocalPosition(Node.LeftHand);
            rightHandAnchor.localPosition = InputTracking.GetLocalPosition(Node.RightHand);

            //If we want to pull from OVRinput
            Vector2 primaryAxis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

            if (primaryAxis.y > 0.0f)
                pos += (primaryAxis.y * transform.forward * Time.deltaTime);

            if (primaryAxis.y < 0.0f)
                pos += (Mathf.Abs(primaryAxis.y) * -transform.forward * Time.deltaTime);

            if (primaryAxis.x < 0.0f)
                pos += (Mathf.Abs(primaryAxis.x) * -transform.right * Time.deltaTime);

            if (primaryAxis.x > 0.0f)
                pos += (primaryAxis.x * transform.right * Time.deltaTime);
            
            transform.position = pos;

            Vector3 euler = transform.rotation.eulerAngles;
            Vector2 secondaryAxis = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
            euler.y += secondaryAxis.x;
            transform.rotation = Quaternion.Euler(euler);
            transform.localRotation = Quaternion.Euler(euler);

        }
    }
}

