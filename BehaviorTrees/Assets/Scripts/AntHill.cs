using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntHill : MonoBehaviour
{
    [SerializeField] private GameObject antPrefab;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private Path path;

    void Start()
    {
        path.ShowPath(true);
        path.CalculatePath();
    }

    public void Spawn()
    {
        GameObject ant = Instantiate(antPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
        ant.GetComponent<Ant>().SetAnt(path);
    }
}
