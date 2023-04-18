using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PanicCar : CarBehaviour
{
    protected override void Move()
    {
        base.Move();
        _navMeshAgent.isStopped = false;
        _navMeshAgent.speed = speed;
        var whereToGo = transform.forward * (Time.deltaTime * _navMeshAgent.speed);
        _navMeshAgent.Move(whereToGo);
    }

    protected override void SlowDown()
    {
        base.SlowDown();
        var whereToGo = transform.forward * (Time.deltaTime * _navMeshAgent.speed * 2);
        _navMeshAgent.Move(whereToGo);
    }

    protected override void Stop()
    {
        base.Stop();
        var i = Random.Range(0, 1);
        
        _navMeshAgent.isStopped = i == 1;
    }
}
