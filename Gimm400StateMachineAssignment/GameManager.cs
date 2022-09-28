using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /// <summary>
    ///     You can use this class to update stats when events fire 
    ///     And to access global values like
    ///     the variable  variableq1 using
    ///     GameManager.Instance.variable1 format
    /// </summary>
    ///  


    public int totalAI;
    public int totalAIDeaths = 0;
    public Contender[] aiImplementations;
    public ArrayList aiInstances;
    public int amountAlive;
    public bool gameOver = false;
    public StateController[] aiControllersInGame;
    public GameObject[] aiInGame;
    public StateController sController;
    public float timeLimitForGameplay = 600;
    public static GameManager Instance { get; private set; }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);

    }

    public void FindSpawnPoint()
    {

        foreach (GameObject go in aiInGame)
        {
            Vector3 possibleSpawnPos = new Vector3(Random.Range(-10f, 10f),
                1f, Random.Range(-10f, 10f));
            foreach (GameObject goAgain in aiInstances)
                if (!(Vector3.Distance(goAgain.transform.position, transform.position)
                    > 5)) continue;
                else
                {
                    //distance from all enemies is at least 5
                    gameObject.SetActive(true);
                    transform.position = possibleSpawnPos;
                }
        }
    }
    public int howManyAreDead()
    {
        return totalAI - amountAlive;
    }
    private void Start()
    {
        startGame(totalAI);
    }
    public void startGame(int totalControllers)
    {
        int i = 0;
        //we'll call this method when we start the game or restart to initialize values:
        aiControllersInGame = GameObject.FindObjectsOfType<StateController>();
        foreach (StateController sc in aiControllersInGame)
        {
            aiInGame[i] = sc.self;
            i++;
        }
        
    }
    public void registerDeath(GameObject aiToBeDestroyed)
    {
        gameObject.SetActive(false);
        aiToBeDestroyed.GetComponent<StateController>().deathCount++;
        totalAI--;
        CheckGameOver();
    }
    public void resetValues(GameObject aiThatDied)
    {
        StateController sc = GetComponent<StateController>();
        sc.aiStats._health = sc.aiStats._startingHealth;
        sc.SetState(new WanderState(sc));
        cloneAI(aiThatDied);
    }
    public void cloneAI(GameObject go)
    {
        //could make it more interesting with cloned values,
        //for now we'll just add to the score keeping track of ai deaths
    }
    public bool CheckGameOver()
    {
        StateController aiDeathMin = new StateController();
        StateController aiDeathMax = new StateController();
        aiDeathMin = aiControllersInGame[0];
        aiDeathMax = aiControllersInGame[0];

        if (totalAIDeaths >= 10 || Time.time > timeLimitForGameplay)
        {
            foreach (StateController sc in aiControllersInGame)
            {
                if (sc.deathCount < aiDeathMin.deathCount)
                {
                    aiDeathMin = sc;
                }
                else if (sc.deathCount > aiDeathMax.deathCount)
                {
                    aiDeathMax = sc;
                }
                Debug.Log("Game Over: Implement a game over scene");
                Debug.Log("winner is: " + aiDeathMin.name + " with " +
                    aiDeathMin.deathCount + " deaths and loser is: " + aiDeathMin.name
                    + " wth " + aiDeathMin.deathCount + " deaths.");

                //TODO put the death count and names into strings to display on the gameover losing screen
                return true;
            }
        }
        else
        {
            return false;
        }

        return false;
    }
}