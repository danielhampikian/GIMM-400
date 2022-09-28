using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneState : State {


    public CloneState(StateController stateController) : base(stateController) { }

    public bool hasCloned = false;
    
    public override void CheckTransitions()
    {
        if (stateController.CheckIfInRange("enemy"))
        {
            stateController.SetState(new EscapeState(stateController));
        }
        if (hasCloned) {
            stateController.SetState(new NPCWander(stateController));
        }

    }
    public override void Act()
    {
        if (!hasCloned)
        {
            GameObject newAI = GameObject.Instantiate(stateController.self, stateController.transform);
            newAI.transform.SetParent(null);
            hasCloned = true;


        }
    }
    public override void OnStateEnter()
    {
        stateController.destination = stateController.GetNextNavPoint();
        if (stateController.ai.agent != null)
        {
            stateController.ai.agent.speed = .2f;
        }
        stateController.ai.SetTarget(stateController.destination);
        stateController.ChangeColor(Color.cyan);
    }

}

