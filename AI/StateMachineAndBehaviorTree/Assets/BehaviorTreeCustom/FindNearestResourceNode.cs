using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindNearestResourceNode : Decorator {
    
    public FindNearestResourceNode(BehaviorTreeController btController) : base(btController) { }

    Transform destination;
    public override NodeStatus Tick()
    {
        NodeStatus nodeStatus = NodeStatus.RUNNING;
        Debug.Log("In Find nearest resource node tick");
        while (btController.allResourcesGathered != true)
        {
            if (destination == null || btController.ai.DestinationReached())
            {
                destination = btController.GetNextResource();
                btController.ai.SetTarget(destination);
            }
        }
        return NodeStatus.SUCCESS;
    }
}
