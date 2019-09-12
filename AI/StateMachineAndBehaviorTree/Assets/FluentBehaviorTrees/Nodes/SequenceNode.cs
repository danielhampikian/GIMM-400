using System;
using System.Collections.Generic;

namespace FluentBehaviourTree
{
    /// <summary>
    /// Runs child nodes in sequence, until one fails.
    /// </summary>
    public class SequenceNode : IParentBehaviourTreeNode
    {
        /// <summary>
        /// Name of the node.
        /// </summary>
        private string name;

        /// <summary>
        /// List of child nodes.
        /// </summary>
        private List<IBehaviourTreeNode> children = new List<IBehaviourTreeNode>(); //todo: this could be optimized as a baked array.

        public SequenceNode(string name)
        {
            this.name = name;
        }

        public BehaviourTreeStatus Tick(TimeData time)
        {
            for (int i = 0; i < children.Count; i++)
            {
                var childStatus = children[i].Tick(time);
                if (childStatus != BehaviourTreeStatus.Success)
                {
                    if (childStatus == BehaviourTreeStatus.Running)
                    {
                        
                        i--;
                        break;
                    }
                    if(childStatus == BehaviourTreeStatus.Failure)
                    return childStatus;//this should break us out of the loop
                }
            }
            //otherwise we go all the way through the loop
            return BehaviourTreeStatus.Success;
        }

        /// <summary>
        /// Add a child to the sequence.
        /// </summary>
        public void AddChild(IBehaviourTreeNode child)
        {
            children.Add(child);
        }
    }
}
