using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    //Composite node
    public class Selector : Node
    {
        public Selector() : base() { }
        public Selector(List<Node> children) : base(children) { }

        public override NodeStatus Evaluate()
        {
            //Sort through children and return running or success at the first child that meets that status
            //Otherwise return failure
            foreach (Node node in children)
            {
                switch (node.Evaluate())
                {
                    case NodeStatus.FAILURE:
                        continue;
                    case NodeStatus.RUNNING:
                        status = NodeStatus.RUNNING;
                        return status;
                    case NodeStatus.SUCCESS:
                        status = NodeStatus.SUCCESS;
                        return status;
                    default:
                        continue;
                }
            }

            status = NodeStatus.FAILURE;
            return status;
        }
    }
}
