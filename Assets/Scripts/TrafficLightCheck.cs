using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightCheck : MonoBehaviour
{
    private CarBehaviour _carBehaviour;

    private TrafficLight _trafficLight;
    
    [SerializeField]
    private TrafficLight.LightColor currLightState;
    
    [SerializeField]private CarBehaviour.CarBehaviourState currCarState;
    
    private void Start()
    {
        transform.parent.TryGetComponent(out _carBehaviour);
    }

    private void OnTriggerStay(Collider other)
    {
        if(!other.CompareTag("TrafficLight"))return;
        other.transform.TryGetComponent(out _trafficLight);
        CheckLightStatus(_trafficLight.lightColor);
    }
    

    private void CheckLightStatus(TrafficLight.LightColor trafficLight)
    {
        currLightState = trafficLight;
        currCarState = _carBehaviour.carBehaviourState;
        switch (trafficLight)
        {
            case TrafficLight.LightColor.Red:
                _carBehaviour.ChangeBehaviour(CarBehaviour.CarBehaviourState.Stop);
                break;
            case TrafficLight.LightColor.Yellow:
                _carBehaviour.ChangeBehaviour(CarBehaviour.CarBehaviourState.SlowDown);
                break;
            case TrafficLight.LightColor.Green:
                _carBehaviour.ChangeBehaviour(CarBehaviour.CarBehaviourState.Move);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
