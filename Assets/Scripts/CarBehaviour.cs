using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CarBehaviour : MonoBehaviour
{
   public enum CarBehaviourState
   {
      Move,
      SlowDown,
      Stop
   };

   
   public CarBehaviourState carBehaviourState;

   private NavMeshAgent _navMeshAgent;

   [SerializeField] private float speed;
   [SerializeField] private Transform goToPos;
   [SerializeField] private float stopDistance;

   private void Start()
   {
      TryGetComponent(out _navMeshAgent);
      _navMeshAgent.speed = speed;
   }

   private void Update()
   {
      CheckBehaviour();
   }

   private void CheckBehaviour()
   {
      switch (carBehaviourState)
      {
         case CarBehaviourState.Move:
            Move();
            break;
         case CarBehaviourState.SlowDown:
            SlowDown();
            break;
         case CarBehaviourState.Stop:
            Stop();
            break;
         default:
            throw new ArgumentOutOfRangeException();
      }
   }
   
   private void Move()
   {
      _navMeshAgent.speed = speed;
      _navMeshAgent.isStopped = false;
      var differenceVector = goToPos.position - transform.position;
      _navMeshAgent.Move(transform.forward);
      //_navMeshAgent.destination = differenceVector.magnitude > stopDistance ? goToPos.position : transform.position;
   }

   private void SlowDown()
   {
      var speed = _navMeshAgent.speed;
   }

   private void Stop()
   {
      _navMeshAgent.isStopped = true;
   }

   public void ChangeBehaviour(CarBehaviourState behaviourState)
   {
      carBehaviourState = behaviourState;
   }
}
