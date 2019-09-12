using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Compositor :TreeNode {

    public TreeNode[] children;
    public Compositor(BehaviorTreeController btController) : base(btController) {
    }
}
