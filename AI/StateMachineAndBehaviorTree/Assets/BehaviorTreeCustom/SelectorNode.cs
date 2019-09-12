using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorNode : Compositor {

    NodeStatus[] nodeStats;
    bool allNodesFailed = false;

    public SelectorNode(BehaviorTreeController btController) : base(btController) {}

    public override NodeStatus Tick()
    {
        nodeStats = new NodeStatus[children.Length];
        NodeStatus nodeStatus = NodeStatus.RUNNING;
        while (!allNodesFailed)
        {
            RunAllNodes(nodeStatus);
        }
        if (allNodesFailed)
        {
            nodeStatus = NodeStatus.FAILURE;
        }
        return nodeStatus;
    }

    private void RunAllNodes(NodeStatus nodeStatus)
    {
        int i = 0;
        while (i < children.Length)
        {
            while (nodeStatus == NodeStatus.RUNNING)
            {
                nodeStatus = children[i].Tick(); //keep ticking in the children until something is not running
            }

            if (nodeStatus == NodeStatus.FAILURE)
            {
                nodeStats[i] = nodeStatus;
            }
        }
        //we're through all nodes, now we need to either loop or if all failed return failure
        foreach(NodeStatus ns in nodeStats)
        {
            if(ns == NodeStatus.SUCCESS)
            {
                RunAllNodes(nodeStatus); //run this function again until we get a failure on all nodes
            }
        }
        // if we get out of this for loop then we've got all failures
        allNodesFailed = true;
    }
}

