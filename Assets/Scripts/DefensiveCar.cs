using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefensiveCar : CarBehaviour
{
    protected override void Start()
    {
        base.Start();
        _navMeshAgent.stoppingDistance = 2;
    }

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
        var whereToGo = transform.forward * (Time.deltaTime * _navMeshAgent.speed / 2);
        _navMeshAgent.Move(whereToGo);
    }

    protected override void Stop()
    {
        base.Stop();
        _navMeshAgent.isStopped = true;
    }
}
