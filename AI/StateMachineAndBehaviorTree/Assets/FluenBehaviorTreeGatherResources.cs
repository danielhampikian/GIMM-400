using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FluentBehaviourTree;

public class FluenBehaviorTreeGatherResources : MonoBehaviour {

    IBehaviourTreeNode tree;
    Transform target;
    BehaviorController behaviorController;

    private IBehaviourTreeNode CreateGatherTreeSequence()
    {
        var builder = new BehaviourTreeBuilder();
        return builder.Sequence("gatherResources")
            .Do("changeColor", t => {
                behaviorController.ChangeColor(Color.magenta);
                return BehaviourTreeStatus.Success;
        })
                   
            .Do("findNearestResource", t =>
            {
                if (behaviorController.needsResource)
                {
                    target = behaviorController.GetNextResource();
                    behaviorController.ai.target = target;
                    behaviorController.hasTarget = true;
                    behaviorController.needsResource = false;
                    return BehaviourTreeStatus.Success;
                }
                else return BehaviourTreeStatus.Success;
            })
             .Do("goToResource", t =>
             {

                 while (behaviorController.hasTarget && !behaviorController.ai.DestinationReached() && behaviorController.ai.target.tag == "resource")
                 {
                     return BehaviourTreeStatus.Failure; //presumably we pause here
                 }
                 behaviorController.hasTarget = false;
                 behaviorController.needsResource = true;
                 return BehaviourTreeStatus.Success;

             })
            .Do("checkIfAllResourcesAreGathered", t => {
                if (behaviorController.allResourcesGathered && behaviorController.ai.DestinationReached())
                    return BehaviourTreeStatus.Success;
                else {
                    Debug.Log("All resouces found");
                        return BehaviourTreeStatus.Failure;
                    }
        })
            .End()
            .Build();
    }
	void Start () {
        behaviorController = GetComponent<BehaviorController>();
        var builder = new BehaviourTreeBuilder();
        tree = builder.Sequence("gatherResources").Splice(CreateGatherTreeSequence())
        .End()
            .Build();
	}
	
	// Update is called once per frame
	void Update () {
        tree.Tick(new TimeData(Time.deltaTime));
	}
}
