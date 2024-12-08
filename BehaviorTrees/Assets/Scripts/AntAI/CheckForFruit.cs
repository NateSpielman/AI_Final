using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckForFruit : Node
{
    private Blackboard blackboard;
    private string hasFruitKey;
    private string seesFruitKey;

    public CheckForFruit(Blackboard blackboard, string hasFruit, string seesFruit)
    {
        this.blackboard = blackboard;
        this.hasFruitKey = hasFruit;
        this.seesFruitKey = seesFruit;
    }
    public override NodeStatus Evaluate()
    {
        bool hasFruit = (bool)blackboard.GetData(hasFruitKey);
        bool seesFruit = (bool)blackboard.GetData(seesFruitKey);
        if (!hasFruit)
        {
            status = seesFruit ? NodeStatus.SUCCESS : NodeStatus.FAILURE;
        }
        else
        {
            status = NodeStatus.FAILURE;
        }
        
        return status;
    }
}
