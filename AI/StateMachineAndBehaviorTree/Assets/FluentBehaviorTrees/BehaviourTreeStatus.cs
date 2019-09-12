using System;
using System.Collections.Generic;

namespace FluentBehaviourTree
{
    /// <summary>
    /// The return type when invoking behaviour tree nodes.
    /// </summary>
    public enum BehaviourTreeStatus
    {
        Success,
        Failure,
        Running
    }
}
