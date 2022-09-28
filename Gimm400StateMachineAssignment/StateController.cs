using System.Collections;
using UnityEngine;

//add one state to this ai, make it involve vision
//a visual feild angle that detects when something is in that visual field)


public class StateController : MonoBehaviour {

    static StateController Instance { get; set; }
    public bool hopping;
    public bool hopStarted;
    public bool crouching;
    public bool crouchStarted;
    public Vector3 startingPos;
    public Vector3 midwayPos;
    public Vector3 midwayRot;
    public Vector3 startingRot;
    public State startState;
    public GameObject navPointsParent;
    public State currentState;
    public GameObject[] navPoints;
    public GameObject enemyToChase;
    public int navPointNum;
    public float remainingDistance;
    public Transform targetTransform;                                    // target to aim for
    public Vector3 targetLocation;
    public Rigidbody rb;
    public Vector3 velocity;
    public GameObject head;
    public float speed;
    public float angularSpeed;
    public float angularSpeedMoving;
    public Renderer[] childrenRend;
    public ArrayList enemies;
    public Vision aiVision;
    public float detectionRange = 5;
    public GameObject proj;
    public float projSpeed = 10;
    public GameObject wanderP;
    public GameObject newNavPoint;
    public GameObject obstacles;
    public GameObject self;
    public float[] timers;
    public int timersIdx;
    public float timeRemaining;
    public ImplementedInterfaceAI aiStats;
    public int numOfAIPlaying;
    public GameManager gm;
    [SerializeField] float m_RunCycleLegOffset = 0.2f; //specific to the character in sample assets, will need to be modified to work with others
    public Animator anim;
    public int deathCount;

    private StateController GetStateController()
    {
        return this;
    }
    void Start()
    {
        timersIdx = 0;
        timers[0] = Time.timeSinceLevelLoad;  //the overall timer for the game so that if the AI battle for more then 10 minutes without a winner (10 total deaths and one team with the highest ration of kills to deaths) we will force a win calculation (least deaths * 1/most kills )wins.
        speed = aiStats._Speed;
        angularSpeed = aiStats._Speed * 30;
        angularSpeedMoving = angularSpeed * 2;
        aiStats = GetComponent<ImplementedInterfaceAI>();
        navPoints = GameObject.FindGameObjectsWithTag("navpoint");
        childrenRend = GetComponentsInChildren<Renderer>();
        aiVision = new Vision(this);
        currentState = new ChaseState(this);
        //SetState(currentState);
        gm = GameManager.Instance;
        enemies = new ArrayList(GameObject.FindGameObjectsWithTag("ai"));
        numOfAIPlaying = enemies.Count;
        deathCount = 0;
        enemies.Remove(gameObject); //take ourselves out of the arrray list

    }



    public void Move(Vector3 move)
    {
        if (Vector3.Distance(transform.position,
            enemyToChase.transform.position) >= 1)
        {
            if (move.magnitude > 1f) move.Normalize();
            move = transform.InverseTransformDirection(move);
            move = Vector3.ProjectOnPlane(move, Vector3.down);
            angularSpeed = Mathf.Atan2(move.x, move.z) * angularSpeed;
            speed = move.z * speed;
            UpdateAnimator(move); //this should take care of it otherwise we can use the other move method 
            //MoveByRB();
        }
    }

    private void UpdateAnimator(Vector3 move)
    {
        anim.SetFloat("Forward", speed, 0.1f, Time.deltaTime);
        anim.SetFloat("Turn", angularSpeed * 10, 0.1f, Time.deltaTime);
        //helpMoveRB();
    }

    IEnumerator ExcitedHop()
    {
        transform.position += new Vector3(0, .2f, 0f);

        hopping = true;
        midwayPos = startingPos + new Vector3(0, 5, 0);
        if (transform.position.y < midwayPos.y)
        {
            rb.useGravity = false;
            midwayPos = startingPos + (Vector3.up * 2);
            transform.position = Vector3.Lerp(startingPos, midwayPos, Time.deltaTime * speed);
            rb.useGravity = true;
            if (transform.rotation.y < midwayRot.y)
            {
                transform.rotation = Quaternion.Slerp(Quaternion.Euler(startingRot),
                   Quaternion.Euler(midwayRot), Time.deltaTime * angularSpeed);
            }
            if (transform.position.y > startingPos.y)
            {
                yield return new WaitForFixedUpdate();
            }
            else
            {
                hopping = false;
            }
        }


        void helpMoveRB()
        {
            Quaternion q = transform.rotation;
            if (CheckIfInRange("enemy") &&
                aiVision.LookForTargets(true, enemyToChase.transform.position))
            {
                head.transform.LookAt(enemyToChase.transform);
            }
            else if (CheckIfInRange("navPoint"))
                head.transform.LookAt(navPoints[navPointNum].transform);

            Vector3 rot = transform.rotation.eulerAngles;
            rot = Vector3.Slerp(transform.rotation.eulerAngles,
                head.transform.rotation.eulerAngles, Time.deltaTime * angularSpeed);
            transform.rotation = Quaternion.Euler(rot);
            rb.velocity = transform.forward * speed * Time.deltaTime;

        }

        void SetMovementToTargetAtSpeed(GameObject targetEnemy, float speed)
        {
            // we'll set the location somewhere near the target if we can't quiite see it but it's
            // in range simulating hearing and proprioseption other senses.
            if (!(aiVision.LookForTargets(true, targetEnemy.transform.position)))

                targetLocation = targetEnemy.transform.position + (Random.insideUnitSphere * 3);



        }

        void Update()
        {
            //in class refactor this as switch enum based state machine
            if ((!hopping && hopStarted) & !crouching)
            {
                startingPos = transform.position;
                midwayPos = startingPos;
                midwayPos.y += 5;
                startingRot = transform.rotation.eulerAngles;
                midwayRot = startingRot;
                midwayRot.y += 180;
                hopStarted = false;
                ExcitedHop();
                hopping = true;

            }

            else if (crouching && !hopping)
            {
                //DepressedCrouch();
            }


            //The main update loop for the state machine:
            currentState.CheckTransitions();
            currentState.Act();

            //The main update loop for measuring time based conditions
            timeRemaining -= Time.deltaTime;
            if (timeRemaining < 0)
            {
                timeRemaining = getNewTimeLimit(3, 6);
                //event timer whatever state they're in let them know

            }


        }
        void Die()
        {

            GameManager.Instance.registerDeath(gameObject); //we need to let the one game manager know who died then for all 
                                                            //the enemies list we need to remove the dead enemy
            StartCoroutine(DieRoutine());

        }
        IEnumerator DieRoutine()
        {
            //GetComponent<ParticleSystem>().Play();
            //GetComponent<AudioSource>().Play();
            Debug.Log("Die routine called");
            yield return new WaitForSeconds(3f); //wait for the object to finish registering it's deah

            if (tag == "npcStandardAI")
            {
                Destroy(gameObject);
                Debug.Log("Die routine finished");
            }
            yield return new WaitForEndOfFrame();

        }
    }
    
    public bool DoneWithNavPatrol()
    {
        return (navPointNum == navPoints.Length - 1);
    }
    public Transform GetNextNavPoint()
    {
        navPointNum = (navPointNum + 1) % navPoints.Length;
        return navPoints[navPointNum].transform;
    }
    public Transform GetWanderPoint()
    {
        //This could be done more efficeintly without introducing a empty game object
        Vector3 wanderPoint = new Vector3(Random.Range(-2f, 2f), 1.5f, Random.Range(-2f, 2f));
        GameObject go = Instantiate(wanderP, wanderPoint, Quaternion.identity);
        return go.transform;
    }
    public Vector3 GetRandomPoint()
    {
        Vector3 ran = new Vector3(Random.Range(-detectionRange, detectionRange), 1.5f,
            Random.Range(-detectionRange, detectionRange));
        return ran - transform.position;
    }
    public Vector3 GetRunawayTarget()
    {
        return -((enemyToChase.transform.position - transform.position) + GetRandomPoint());
    }

    public void AddNavPoint(Vector3 pos)
    {
        GameObject go = Instantiate(newNavPoint, pos, Quaternion.identity);
        go.transform.SetParent(navPointsParent.transform);
        navPoints = GameObject.FindGameObjectsWithTag("navpoint");


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
    public Transform CheckIfInRange(GameObject g)
    {
        if (Vector3.Distance(g.transform.position, transform.position) < detectionRange)
        {
            enemyToChase = g;
            return g.transform;
        }

        return transform;
    }


    public void SetState(State state)
    {
        if(currentState != null)
        {
            currentState.OnStateExit();
        }

        currentState = state;
        gameObject.name = "AI agent in state " + state.GetType().Name;

        if(currentState != null)
        {
            currentState.OnStateEnter();
        }
    }


    public  float getNewTimeLimit(float min, float max)
    {
        return Random.Range(min, max);
    }

    private void OnAnimatorMove()
    {
        // we implement this function to override the default root motion.
        // this allows us to modify the positional speed before it's applied.
        if (Time.deltaTime > 0)
        {
            Vector3 v = (anim.deltaPosition) / Time.deltaTime;

            // we preserve the existing y part of the current velocity.
            v.y = rb.velocity.y;
            rb.velocity = v;

        }
    }



    public void DepressedCrouch()
    {
    }


    public void helpMoveRB()
    {
    }
}
