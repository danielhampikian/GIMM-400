using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCWander : State {

    public NPCWander(StateController stateController) : base(stateController) { }
    int maxPoints = 10;
    int numPointsMade = 0;
    public override void CheckTransitions()
    {
      
        if (stateController.CheckIfInRange("enemy"))
        {
            stateController.SetState(new EscapeState(stateController));
        }
        if (numPointsMade > maxPoints)
        {
            stateController.SetState(new CloneState(stateController));
        }
    }
    public override void Act()
    {
        if (stateController.destination == null || stateController.ai.DestinationReached())
        {
            stateController.destination.position = stateController.GetRandomPoint();
            stateController.AddNavPoint(stateController.destination.position);
            stateController.ai.SetTarget(stateController.destination);
            numPointsMade++;
        }
    }
    public override void OnStateEnter()
    {
        
        maxPoints = Random.Range(15,19);
        stateController.ChangeColor(Color.HSVToRGB(273, 78, 99));
        stateController.ai.agent.speed = 4f;
    }
}
