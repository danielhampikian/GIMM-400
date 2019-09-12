using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorController : MonoBehaviour {

    public bool hasTarget;
    public GameObject[] resources;
    public GameObject[] navpoints; //we'll have them make their own nav points
    public GameObject enemyToChase;
    public int navPointNum;
    public int resourceNum;
    public bool allResourcesGathered;
    public bool needsResource;
    public float remainingDistance;
    public Transform destination;
    public UnityStandardAssets.Characters.ThirdPerson.AICharacterControl ai;
    public Renderer[] childrenRend;
    public GameObject[] enemies;
    public float detectionRange = 5;

    public Transform GetNextResource()
    {
        if (resourceNum == resources.Length-1)
        {
            allResourcesGathered = true;
            needsResource = false;
            return resources[resourceNum - 1].transform;
        }
        Debug.Log("resource num assigned = " + resourceNum);
        if (needsResource)
        {
            resourceNum++;
            needsResource = false;
        }
        return resources[resourceNum].transform;
    }

    public void ChangeColor(Color color)
    {
        foreach (Renderer r in childrenRend)
        {
            foreach (Material m in r.materials)
            {
                m.color = color;
            }
        }
    }
    public bool CheckIfInRange(string tag)
    {
        enemies = GameObject.FindGameObjectsWithTag(tag);
        if (enemies != null)
        {
            foreach (GameObject g in enemies)
            {
                if (Vector3.Distance(g.transform.position, transform.position) < detectionRange)
                {
                    enemyToChase = g;
                    return true;
                }
            }
        }
        return false;
    }
    void Start()
    {
        resources = GameObject.FindGameObjectsWithTag("resource");
        ai = GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>();
        childrenRend = GetComponentsInChildren<Renderer>();
        allResourcesGathered = false;
        hasTarget = false;
    }
}
