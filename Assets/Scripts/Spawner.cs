using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Random= UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] objectToSpawn;
    [SerializeField] private int amountOfObjectsToSpawn;
    [SerializeField] private float timeToSpawn;
    [SerializeField] private Transform[] checkPoints;
    
    
    private float _t;
    private float _spawnCount;

    private void Update()
    {
        _t += Time.deltaTime;
        if (!(_t > timeToSpawn)) return;
        if(_spawnCount > amountOfObjectsToSpawn) return;
        Spawn();
    }

    private void Spawn()
    {
        _t = 0;
        _spawnCount++;
        var i = Random.Range(0, objectToSpawn.Length);
        
        var car = Instantiate(objectToSpawn[i], transform.position, Quaternion.identity);

        car.TryGetComponent(out CarBehaviour carBehaviour);
        carBehaviour.SetCheckPoints(checkPoints);
       // carBehaviour.RandomizeCarType();
    }
}
