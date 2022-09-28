using UnityEngine;

public class EscapeState : State
{

    public EscapeState(StateController stateController) : base(stateController) { }

    public override void CheckTransitions()
{
    if (!stateController.CheckIfInRange("enemy"))
    {
        stateController.SetState(new NPCWander(stateController));
    }
}
public override void Act()
{
        if (stateController.CheckIfInRange("enemy"))
        {
            stateController.destination = stateController.CheckIfInRange(stateController.enemyToChase);
            stateController.navMeshAgent.SetDestination(stateController.destination.position);
        }
}
    public override void OnStateEnter()
    {
        stateController.ChangeColor(new Color(.2f, .95f, .97f));
        stateController.ai.agent.speed = 3f;
        stateController.destination = stateController.CheckIfInRange(stateController.enemyToChase);
        stateController.navMeshAgent.SetDestination(stateController.destination.position);
    }
}
