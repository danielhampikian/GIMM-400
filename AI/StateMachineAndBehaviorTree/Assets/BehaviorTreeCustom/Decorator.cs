using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Decorator : TreeNode {

    public TreeNode child;
    public Decorator(BehaviorTreeController btController) : base(btController) { }

}
