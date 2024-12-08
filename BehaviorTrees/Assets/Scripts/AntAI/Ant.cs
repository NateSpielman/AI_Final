using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using Unity.VisualScripting;

public class Ant : MonoBehaviour
{
    //PATHING
    private Path path;
    private Vector3[] waypoints;
    private int waypointIndex = 0;
    [SerializeField] private float speed;
    private float distanceTo;

    private Blackboard blackboard;

    private static string SPEED = "Speed";
    private static string WAYPOINTS = "Waypoints";

    public void SetAnt(Path path)
    {
        this.path = path;
        waypoints = new Vector3[path.GetNumPoints()];
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = path.GetPointPos(i);
        }

        blackboard = new Blackboard();

        blackboard.SetData(SPEED, speed);
        blackboard.SetData(WAYPOINTS, waypoints);

        this.AddComponent<AntBT>().blackboard = blackboard;
    }
}
