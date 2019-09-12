using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeState : State {


    public FleeState(StateController stateController) : base(stateController) { }

    public override void CheckTransitions()
    {
       
    }
    public override void Act()
    {
       
    }
    public override void OnStateEnter()
    {
        
        stateController.ChangeColor(Color.yellow);
    }
}
