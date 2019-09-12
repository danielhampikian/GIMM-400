using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceNode : Compositor {

    public SequenceNode(BehaviorTreeController btController) : base(btController) { }

    public override NodeStatus Tick()
    {
        NodeStatus nodeStatus = NodeStatus.RUNNING;
        foreach (TreeNode child in children)
        {
            while(nodeStatus == NodeStatus.RUNNING)
            {
                nodeStatus = child.Tick(); //keep ticking in the children until something is not running
            }
            if(nodeStatus == NodeStatus.FAILURE) //this should ensure we break out of the sequence with failure before everything gets executed
            {
                break;
            }
        }
        return nodeStatus;
    }
}
