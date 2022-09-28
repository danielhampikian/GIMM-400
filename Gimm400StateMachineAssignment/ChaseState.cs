using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State {

    public ChaseState(StateController stateController) : base(stateController) { }
    
    public override void CheckTransitions()
    {
        if (!stateController.CheckIfInRange("enemy"))
        {
            stateController.SetState(new WanderState(stateController));
            stateController.crouching = true;
        }
    }
    public override void Act()
    {
        if (stateController.enemyToChase != null)
        {
            //Another way to do this purely with vectors and no rigidBodies is
            /*Vector3 enemtPos= stateController.enemyToChase.transform.position
            Vector3 myPos = stateController.transform.position;
            Vector3 directionTowardsTarget = myPos - enemtPos;*/
            if (stateController.CheckIfInRange(stateController.enemyToChase)
                && stateController.aiVision.LookForTargets(true, stateController.targetLocation))
            {
                stateController.SetMovementToTargetAtSpeed(stateController.enemyToChase, stateController.speed);
            }
            else 
                stateController.enemyToChase = null;
        }
    }
    public override void OnStateEnter()
    {
        stateController.hopping = true;
        stateController.ChangeColor(Color.red);
        stateController.speed = stateController.speed * .5f;
        stateController.targetTransform = stateController.enemyToChase.transform;
        stateController.targetLocation = stateController.targetTransform.position;
        stateController.SetMovementToTargetAtSpeed(stateController.enemyToChase, stateController.speed);
    }
}
