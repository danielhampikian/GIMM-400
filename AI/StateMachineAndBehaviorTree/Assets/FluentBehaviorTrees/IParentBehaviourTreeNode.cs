using System;
using System.Collections.Generic;

namespace FluentBehaviourTree
{
    /// <summary>
    /// Interface for behaviour tree nodes.
    /// </summary>
    public interface IParentBehaviourTreeNode : IBehaviourTreeNode
    {
        /// <summary>
        /// Add a child to the parent node.
        /// </summary>
        void AddChild(IBehaviourTreeNode child);
    }
}
