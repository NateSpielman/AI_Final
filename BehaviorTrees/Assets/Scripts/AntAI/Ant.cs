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
    [SerializeField] private float speed;
    [SerializeField] private SpriteRenderer fruitSprite;

    private bool hasFruit = false;
    public Transform fruit;

    private Blackboard blackboard;

    private static string SPEED = "Speed";
    private static string WAYPOINTS = "Waypoints";
    private static string WAYPOINTINDEX = "WaypointIndex";
    private static string SEESFRUIT = "SeesFruit";
    private static string HASFRUIT = "HasFruit";
    private static string FRUIT = "Fruit";

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
        blackboard.SetData(SEESFRUIT, false);
        blackboard.SetData(HASFRUIT, hasFruit);
        blackboard.SetData(FRUIT, fruit);

        this.AddComponent<AntBT>().blackboard = blackboard;
    }

    public void Update()
    {
        if ((bool)blackboard.GetData(HASFRUIT) == true)
        {
            fruitSprite.enabled = true;
        }
        else
        {
            fruitSprite.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Fruit")
        {
            blackboard.SetData(SEESFRUIT, true);
            if(fruit != collision.gameObject.transform)
            {
                fruit = collision.gameObject.transform;
                blackboard.SetData(FRUIT, fruit);
                GetComponent<AntBT>().blackboard = blackboard;
                GetComponent<AntBT>().Setup();
            }
        }
    }
}
