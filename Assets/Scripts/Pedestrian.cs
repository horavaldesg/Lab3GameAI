using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
public class Pedestrian : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    
    public enum MovementType
    {
        Move,
        SpeedUp,
        Stop
    }

    public MovementType movementType;


     public Transform[] checkpoints;
    private static readonly int Walking = Animator.StringToHash("Walking");
    private static readonly int Idle = Animator.StringToHash("Idle");
    [HideInInspector] public TrafficLight[] trafficLights;
    
    private int CurrentCheckpoint { get; set; }

    public int debubCheckpoint;
    [SerializeField] private float speed;

    
    private void Start()
    {
        gameObject.layer = 13;
        TryGetComponent(out _navMeshAgent);
        TryGetComponent(out _animator);
    }

    private void Update()
    {
        debubCheckpoint = CurrentCheckpoint;
        CheckLights();
        CheckBehaviour();
    }

    private void CheckBehaviour()
    {
        switch (movementType)
        {
            case MovementType.Move:
                Move();
                break;
            case MovementType.SpeedUp:
                SpeedUp();
                break;
            case MovementType.Stop:
                Stop();
                break;
        }
    }

    private void Move()
    {
        _animator.SetBool(Walking, true);
        _animator.SetBool(Idle, false);
        _navMeshAgent.isStopped = false;
        _navMeshAgent.speed = speed;
        _navMeshAgent.destination = checkpoints[CurrentCheckpoint].position;
    }

    private void SpeedUp()
    {
        _animator.SetBool(Walking, true);
        _animator.SetBool(Idle, false);
        _navMeshAgent.isStopped = false;
        _navMeshAgent.speed = speed * 2;
        _navMeshAgent.destination = checkpoints[CurrentCheckpoint].position;
    }

    private void Stop()
    {
        _animator.SetBool(Walking, false);
        _animator.SetBool(Idle, true);
        _navMeshAgent.isStopped = true;
    }

    private void CheckLights()
    {
        switch (trafficLights[CurrentCheckpoint].lightColor)
        {
            case TrafficLight.LightColor.Green:
                ChangeState(MovementType.Move);
                break;
            case TrafficLight.LightColor.Yellow:
                //ChangeState(MovementType.SpeedUp);
                break;
            case TrafficLight.LightColor.Red:
                ChangeState(MovementType.Stop);
                break;
        }
    }

    private void ChangeState(MovementType movementType)
    {
        this.movementType = movementType;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("PedCheckPoint")) return;
        var currentCheckPoint = other.transform;
        //Gets the current checkpoint index the car is on
        CurrentCheckpoint = Array.IndexOf(checkpoints, currentCheckPoint);
        //Increases the checkpoint destination index by 1
        CurrentCheckpoint++;

        //Checks if the index is greater than the length of the checkpoint array
        if (CurrentCheckpoint > checkpoints.Length - 1)
        {
            CurrentCheckpoint = 0;
        }
    }
}

