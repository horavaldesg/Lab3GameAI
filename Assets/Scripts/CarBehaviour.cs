// This script controls the behavior of a car using NavMeshAgent
// and defines three different states: Move, SlowDown, and Stop.

using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CarBehaviour : MonoBehaviour
{
   // Enumeration of different car behaviors.
   public enum CarBehaviourState
   {
      Move,
      SlowDown,
      Stop
   };

   // The current behavior of the car.
   public CarBehaviourState carBehaviourState;

   // The NavMeshAgent component attached to the car object.
   private NavMeshAgent _navMeshAgent;

   // The speed of the car.
   [SerializeField] private float speed;

   // The previous position of the car.
   private Vector3 _previousPos;

   // Initializes the NavMeshAgent component and sets its speed.
   private void Start()
   {
      TryGetComponent(out _navMeshAgent);
      _navMeshAgent.speed = speed;
   }

   // Checks the current behavior of the car and performs the appropriate action.
   private void FixedUpdate()
   {
      CheckBehaviour();
   }

   // Performs the action according to the current behavior of the car.
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

   // Moves the car forward at its current speed.
   private void Move()
   { 
      _navMeshAgent.isStopped = false;
      _navMeshAgent.speed = speed;
      var whereToGo = transform.forward * (Time.deltaTime * _navMeshAgent.speed);
      _navMeshAgent.Move(whereToGo);
   }

   // Slows down the car to half its speed.
   private void SlowDown()
   {
      var whereToGo = transform.forward * (Time.deltaTime * _navMeshAgent.speed / 2);
      _navMeshAgent.Move(whereToGo);   
   }

   // Stops the car.
   private void Stop()
   {
      _navMeshAgent.isStopped = true;
   }

   // Changes the current behavior of the car.
   public void ChangeBehaviour(CarBehaviourState behaviourState)
   {
      carBehaviourState = behaviourState;
   }
}