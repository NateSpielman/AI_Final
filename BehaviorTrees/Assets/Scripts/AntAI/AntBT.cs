using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class AntBT : BTree
{
    private Node root;
    public Blackboard blackboard;
    public Blackboard GetBlackboard => blackboard;

    private static string SPEED = "Speed";
    private static string WAYPOINTS = "Waypoints";

    protected override Node SetupTree()
    {
        root = new Selector();
        root.SetChildren(new List<Node>
        {
            new ActionMoveAlongPath(transform, blackboard, WAYPOINTS, SPEED)
        });
        root.SetRoot(root);
        return root;
    }
}
