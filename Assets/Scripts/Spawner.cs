using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Random= UnityEngine.Random; 

public class Spawner : MonoBehaviour
{
    // Serialized private array of GameObjects that will be spawned
    [SerializeField] private GameObject[] objectToSpawn;
    
    // Serialized private integer that sets the max amount of objects to spawn
    [SerializeField] private int amountOfObjectsToSpawn;
    
    // Serialized private float that sets the time between spawns
    [SerializeField] private float timeToSpawn;
    
    // Serialized private array of Transforms that act as checkpoints for the cars
    [SerializeField] private Transform[] checkPoints;
     
    // Private float that keeps track of time passed
    private float _t;
    
    // Private float that keeps track of the number of spawned objects
    private float _spawnCount;
    
    private void Update()
    {
        // Adds the amount of time passed since the last frame to _t
        _t += Time.deltaTime;
        // If the time passed is less than timeToSpawn, exit the function
        if (!(_t > timeToSpawn)) return;
        // If the maximum amount of objects to spawn has been reached, exit the function
        if(_spawnCount > amountOfObjectsToSpawn) return;
        // If all conditions are met, spawn a new object
        Spawn();
    }
    
     // Function that spawns a new object
    private void Spawn()
    {
        // Resets the timer
        _t = 0;
        // Adds 1 to the number of spawned objects
        _spawnCount++;
        // Chooses one random object from the array of objects to spawn
        var i = Random.Range(0, objectToSpawn.Length);
         // Instantiates the randomized object at the spawner's position and rotation
        var car = Instantiate(objectToSpawn[i], transform.position, Quaternion.identity);
         // Tries to get the CarBehaviour component from the instantiated car and sets its checkpoints
        car.TryGetComponent(out CarBehaviour carBehaviour);
        carBehaviour.SetCheckPoints(checkPoints);
       // carBehaviour.RandomizeCarType();
    }
}