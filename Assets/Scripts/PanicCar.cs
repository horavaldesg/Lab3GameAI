using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PanicCar : CarBehaviour
{
    protected override void Start()
    {
        base.Start();
        SetCarType(CarType.Panic);
        _navMeshAgent.stoppingDistance = 0;
    }


    protected override void SlowDown()
    {
        base.SlowDown();
        ChangeBehaviour(CarBehaviourState.Move);
    }

    protected override void ChooseRandomMesh()
    {
        carMeshes[0].SetActive(true);
    }

    protected override void Stop()
    {
        base.Stop();
        var i = Random.Range(0, 1);
        ChangeBehaviour(i == 0 ? CarBehaviourState.Move : CarBehaviourState.Stop);

        //_navMeshAgent.isStopped = i == 1;
    }
}
