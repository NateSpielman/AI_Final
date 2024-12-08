using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    //Composite node
    public class Sequence : Node
    {
        public Sequence() : base() { }
        public Sequence(List<Node> children) : base(children) { }

        public override NodeStatus Evaluate()
        {
            //Runs through all children, returns failure if a single child fails
            //Return running if a single child is running
            //Return success if all children succeed
            bool hasChildRunning = false;

            foreach (Node node in children)
            {
                switch (node.Evaluate())
                {
                    case NodeStatus.FAILURE:
                        status = NodeStatus.FAILURE;
                        return status;
                    case NodeStatus.RUNNING:
                        hasChildRunning = true;
                        continue;
                    case NodeStatus.SUCCESS:
                        continue;
                    default:
                        status = NodeStatus.SUCCESS;
                        return status;
                }
            }

            status = hasChildRunning ? NodeStatus.RUNNING : NodeStatus.SUCCESS;
            return status;
        }
    }
}
