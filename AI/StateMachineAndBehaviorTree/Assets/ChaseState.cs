using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State {

    Transform destination;

    public ChaseState(StateController stateController) : base(stateController) { }

    public override void CheckTransitions()
    {
        if (!stateController.CheckIfInRange("Player"))
        {
            stateController.SetState(new PatrolState(stateController));
        }
    }
    public override void Act()
    {
        if(stateController.enemyToChase != null)
        {
            destination = stateController.enemyToChase.transform;
            stateController.ai.SetTarget(destination);
        }
    }
    public override void OnStateEnter()
    {
        stateController.ChangeColor(Color.red);
        stateController.ai.agent.speed = .5f;
    }
}
