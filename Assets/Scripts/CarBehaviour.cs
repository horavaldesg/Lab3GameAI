// This script controls the behavior of a car using NavMeshAgent
// and defines three different states: Move, SlowDown, and Stop.
// and three difference car types: Defensive, Panic, and Emergency
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(NavMeshAgent))]
public class CarBehaviour : MonoBehaviour
{
   // Enumeration of different car behaviors.
   public enum CarBehaviourState
   {
      Move,
      SlowDown,
      Stop,
      CheckCheckPoint
   };
   
   // The current behavior of the car.
   public CarBehaviourState carBehaviourState;
   

   // The NavMeshAgent component attached to the car object.
   protected NavMeshAgent _navMeshAgent;

   // The speed of the car.
   [SerializeField] protected float speed;

   // The previous position of the car.
   private Vector3 _previousPos;

   //CheckPoints
   [SerializeField] private Transform[] checkPoints;

   private int _checkPointIndex;

   private Transform _currentCheckPoint;

   public void SetCheckPoint(Transform checkPoint)
   {
      _currentCheckPoint = checkPoint;
      ChangeBehaviour(CarBehaviourState.CheckCheckPoint);
   }

   // Initializes the NavMeshAgent component and sets its speed.
   protected virtual void Start()
   {
      TryGetComponent(out _navMeshAgent);
      _navMeshAgent.speed = speed;
   }


   // Checks the current behavior of the car and performs the appropriate action.
   protected virtual void FixedUpdate()
   {
      CheckBehaviour();
   }
   
  
   protected virtual void CheckBehaviour()
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
         case CarBehaviourState.CheckCheckPoint:
            CheckCheckPoint();
            break;
         default:
            throw new ArgumentOutOfRangeException();
      }
   }

   // Moves the car forward at its current speed.
   protected virtual void Move()
   {
   }

   // Slows down the car to half its speed.
   protected virtual void SlowDown()
   {
   }

   // Stops the car.
   protected virtual void Stop()
   {
   }

   //Checkpoint manager
   protected virtual void CheckCheckPoint()
   {
      //Gets the current checkpoint index the car is on
      _checkPointIndex = Array.IndexOf(checkPoints, _currentCheckPoint);
      //Increases the checkpoint destination index by 1
      _checkPointIndex++;

      //Checks if the index is greater than the length of the checkpoint array
      if (_checkPointIndex > checkPoints.Length - 1)
      {
         _checkPointIndex = 0;
      }

      //Makes navmesh agent move
      _navMeshAgent.isStopped = false;
      _navMeshAgent.speed = speed;
      _navMeshAgent.destination = checkPoints[_checkPointIndex].position;
   }

   // Changes the current behavior of the car.
   public virtual void ChangeBehaviour(CarBehaviourState behaviourState)
   {
      carBehaviourState = behaviourState;
   }
   
   public virtual void SetCheckPoints(Transform[] checkPoints)
   {
      this.checkPoints = checkPoints;
   }
}