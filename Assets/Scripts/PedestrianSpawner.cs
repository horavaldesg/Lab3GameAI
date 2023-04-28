using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianSpawner : MonoBehaviour
{
    [SerializeField] private GameObject pedestrian;
    [SerializeField] private Transform[] checkPoints;
    [SerializeField] private TrafficLight[] trafficLights;

    [SerializeField] private int amountOfObjectsToSpawn;

    [SerializeField] private float timeToSpawn;
    
    // Private float that keeps track of time passed
    private float _t;
    
    // Private float that keeps track of the number of spawned objects
    private float _spawnCount;

    private float _randomTime;
    
    private void Start()
    {
       Spawn();
    }

    private void Update()
    {
        // Adds the amount of time passed since the last frame to _t
        _t += Time.deltaTime;
        // If the time passed is less than timeToSpawn, exit the function
        if (!(_t > _randomTime)) return;
        // If the maximum amount of objects to spawn has been reached, exit the function
        if(_spawnCount >= amountOfObjectsToSpawn) return;
        // If all conditions are met, spawn a new object
        Spawn();
    }

    private float RandomizeTime()
    {
        return Random.Range(0, timeToSpawn);
    }


    private void Spawn()
    {
        _t = 0;
        _randomTime = RandomizeTime();
        _spawnCount++;
        var ped = Instantiate(pedestrian, transform.position, Quaternion.identity);
        ped.TryGetComponent(out Pedestrian pedestrianComp);
        pedestrianComp.checkpoints = checkPoints;
        pedestrianComp.trafficLights = trafficLights;
    }
}
