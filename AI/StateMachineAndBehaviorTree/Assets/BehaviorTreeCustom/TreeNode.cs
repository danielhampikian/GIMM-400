using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TreeNode {

    protected BehaviorTreeController btController;
    //constructor
    public TreeNode(BehaviorTreeController btController)
    {
        this.btController = btController;
    }
    public abstract NodeStatus Tick();
}
public enum NodeStatus
{
    SUCCESS,
    FAILURE,
    RUNNING
}
