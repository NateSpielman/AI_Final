using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class ActionMoveToFruit : Node
{
    private Blackboard blackboard;
    private Transform transform;
    private Transform fruit;
    private float speed;

    public ActionMoveToFruit(Blackboard blackboard, Transform transform, string fruit, string speed)
    {
        this.blackboard = blackboard;
        this.transform = transform;
        this.fruit = (Transform)blackboard.GetData(fruit);
        this.speed = (float)blackboard.GetData(speed);
        //Debug.Log(fruit);
    }

    public override NodeStatus Evaluate()
    {
        if (transform.position == fruit.transform.position)
        {
            return NodeStatus.SUCCESS;
        }

        Move();

        status = NodeStatus.RUNNING;
        return status;
    }

    private void Move()
    {
        Vector2 direction = fruit.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.position = Vector2.MoveTowards(this.transform.position, fruit.transform.position, speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    }
}
