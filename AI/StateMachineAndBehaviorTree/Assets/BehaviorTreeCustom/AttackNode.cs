using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackNode : Decorator {

    Transform destination;

    public AttackNode(BehaviorTreeController btController) : base(btController) { }

    public override NodeStatus Tick()
    {
        Debug.Log("In Attack node tick");
        NodeStatus nodeStatus = NodeStatus.RUNNING;

        if (btController.CheckIfInRange("Player"))
        {
            destination = btController.enemyToChase.transform;
            btController.ai.SetTarget(destination);
            if (btController.allResourcesGathered != true)
            {
                nodeStatus = NodeStatus.FAILURE; //not ready to attack yet
            }
            if (btController.allResourcesGathered && !btController.ai.DestinationReached())
            {
                nodeStatus = NodeStatus.RUNNING;
            }
        }
        else //player is not in range
        {
            nodeStatus = NodeStatus.FAILURE;
        }
        //if we've reached the player destination, let's change color and loose our resources
        if (btController.ai.DestinationReached())
        {
            btController.ChangeColor(Color.cyan);
            btController.resourceNum = 0;
            btController.allResourcesGathered = false;
            nodeStatus = NodeStatus.SUCCESS;
        }
        return nodeStatus;
    }
}
