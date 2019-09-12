using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorTreeController : MonoBehaviour {

    public State currentState;
    public GameObject[] navPoints;
    public GameObject[] resources;
    public GameObject enemyToChase;
    public int navPointNum;
    public int resourceNum;
    public bool allResourcesGathered;
    public float remainingDistance;
    public Transform destination;
    public UnityStandardAssets.Characters.ThirdPerson.AICharacterControl ai;
    public Renderer[] childrenRend;
    public GameObject[] enemies;
    public float detectionRange = 5;
    //behavior tree root:
    public SelectorNode rootNode;

    public Transform GetNextNavPoint()
    {
        navPointNum = (navPointNum + 1) % navPoints.Length;
        return navPoints[navPointNum].transform;
    }

    public Transform GetNextResource()
    {
        if (resourceNum < resources.Length)
        {
            resourceNum++;
        }
        else
        {
            allResourcesGathered = true;
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
        navPoints = GameObject.FindGameObjectsWithTag("navpoint");
        resources = GameObject.FindGameObjectsWithTag("resource");
        ai = GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>();
        childrenRend = GetComponentsInChildren<Renderer>();
        allResourcesGathered = false;

        //setup behavior tree:
        rootNode = new SelectorNode(this);
        rootNode.children = new TreeNode[]
        {
            new AttackNode(this), //we'll run this first so we're always ready to attack if we have our resources
            new FindNearestResourceNode(this)
        };
    }

    // Update is called once per frame
    void Update () {
        rootNode.Tick();
	}
}
