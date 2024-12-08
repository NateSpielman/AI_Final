using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class ActionHarvest : Node
{
    private Blackboard blackboard;
    private string hasFruitKey;
    private Transform fruit;

    public ActionHarvest(Blackboard blackboard, string hasFruit, string fruit)
    {
        this.blackboard = blackboard;
        this.hasFruitKey = hasFruit;
        this.fruit = (Transform)blackboard.GetData(fruit);
    }

    public override NodeStatus Evaluate()
    {
        if(fruit == null)
        {
            return NodeStatus.FAILURE;
        }

        blackboard.SetData(hasFruitKey, true);
        fruit.gameObject.SetActive(false);
        
        return NodeStatus.SUCCESS;
    }
}
