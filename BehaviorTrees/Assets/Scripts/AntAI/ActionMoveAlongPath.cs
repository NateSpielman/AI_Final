using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class ActionMoveAlongPath : Node
{
    private Transform transform;
    private Vector3[] waypoints;
    private int waypointIndex = 0;
    private float speed;
    private Blackboard blackboard;

    public ActionMoveAlongPath(Transform transform, Blackboard blackboard, string waypointsKey, string speedKey)
    {
        this.transform = transform;
        this.waypoints = (Vector3[])blackboard.GetData(waypointsKey);
        this.speed = (float)blackboard.GetData(speedKey);
    }

    public override NodeStatus Evaluate()
    {
        Move();

        status = NodeStatus.RUNNING;
        return status;
    }

    private void Move()
    {
       if (waypointIndex <= waypoints.Length - 1)
       {
            //Move to next waypoint
            Vector2 direction = waypoints[waypointIndex] - transform.position;
            direction.Normalize();
            transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex], speed * Time.deltaTime);

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotationGoal = Quaternion.Euler(new Vector3(0, 0, angle));
            transform.rotation = Quaternion.Slerp(transform.rotation, rotationGoal, speed * Time.deltaTime);

            if (transform.position == waypoints[waypointIndex])
            {
                waypointIndex++;
            }
       }

    }
}
