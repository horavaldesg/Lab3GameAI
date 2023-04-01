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
   private Vector3 _previousPos;
   
   private void Start()
   {
      TryGetComponent(out _navMeshAgent);
      _navMeshAgent.speed = speed;
   }

   private void FixedUpdate()
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
      _navMeshAgent.isStopped = false;
      _navMeshAgent.speed = speed;
      var differenceVector = goToPos.position - transform.position;
      var whereToGo = transform.forward * (Time.deltaTime * _navMeshAgent.speed);
      _navMeshAgent.Move(whereToGo);
      // _navMeshAgent.Move(transform.forward);
      //_navMeshAgent.destination = differenceVector.magnitude > stopDistance ? goToPos.position : transform.position;
   }

   private void SlowDown()
   {
      var whereToGo = transform.forward * (Time.deltaTime * _navMeshAgent.speed / 2);
      _navMeshAgent.Move(whereToGo);   
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
