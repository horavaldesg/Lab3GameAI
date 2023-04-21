using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PanicCar : CarBehaviour
{
    protected override void Start()
    {
        base.Start();
        _navMeshAgent.stoppingDistance = 0;
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
        ChangeBehaviour(CarBehaviourState.Move);
    }

    protected override void Stop()
    {
        base.Stop();
        var i = Random.Range(0, 1);
        ChangeBehaviour(i == 0 ? CarBehaviourState.Move : CarBehaviourState.Stop);

        //_navMeshAgent.isStopped = i == 1;
    }
}
