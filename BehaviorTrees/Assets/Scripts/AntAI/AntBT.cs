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
    private static string SEESFRUIT = "SeesFruit";
    private static string HASFRUIT = "HasFruit";
    private static string FRUIT = "Fruit";

    protected override Node SetupTree()
    {
        root = new Selector();
        root.SetChildren(new List<Node>
        {
            //GET FRUIT
            new Sequence(new List<Node>
            {
                new CheckForFruit(blackboard, HASFRUIT, SEESFRUIT),
                new ActionMoveToFruit(blackboard, transform, FRUIT, SPEED),
                new ActionHarvest(blackboard, HASFRUIT, FRUIT)
            }),

            //PATROL
            new ActionMoveAlongPath(transform, blackboard, WAYPOINTS, SPEED)
        });
        root.SetRoot(root);
        return root;
    }
}
