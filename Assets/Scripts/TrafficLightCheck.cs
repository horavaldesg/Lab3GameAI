using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightCheck : MonoBehaviour
{
    private CarBehaviour _carBehaviour;

    private void Start()
    {
        transform.parent.TryGetComponent(out _carBehaviour);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("TrafficLight"))return;
        other.transform.TryGetComponent(out TrafficLight trafficLight);
        switch (trafficLight.lightColor)
        {
            case TrafficLight.LightColor.Red:
                _carBehaviour.ChangeBehaviour(CarBehaviour.CarBehaviourState.Stop);
                break;
            case TrafficLight.LightColor.Yellow:
                //ChangeBehaviour(CarBehaviourState.SlowDown);
                break;
            case TrafficLight.LightColor.Green:
                _carBehaviour.ChangeBehaviour(CarBehaviour.CarBehaviourState.Move);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
