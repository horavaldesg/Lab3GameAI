using System;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class TrafficLightCheck : MonoBehaviour
{
    // Private fields
    private CarBehaviour _carBehaviour;
    private TrafficLight _trafficLight;
    

    private void Start()
    {
        // Try to get the parent's CarBehaviour component
        transform.parent.TryGetComponent(out _carBehaviour);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CheckPoint"))
        {
            _carBehaviour.SetCheckPoint(other.transform);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // If the other collider isn't tagged with "TrafficLight", return
        switch (other.tag)
        {
            case "TrafficLight":
                CheckTrafficLight(other);
                break;
            case "Car":
                if(_carBehaviour.CurrentCarType() == CarBehaviour.CarType.Panic) return;
               other.gameObject.TryGetComponent(out DefensiveCar defensiveCar);
                if(defensiveCar == null) return;
                defensiveCar.ChangeBehaviour(_carBehaviour.CurrentCarBehaviour());
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _trafficLight = null;
        _carBehaviour.ChangeBehaviour(CarBehaviour.CarBehaviourState.Move);
    }

    private void CheckTrafficLight(Collider other)
    {
        // Try to get the other colliders TrafficLight component
        other.transform.TryGetComponent(out _trafficLight);
        
        // Check the status of the traffic light
        CheckLightStatus(_trafficLight.lightColor);
    }
    
    // Check the status of the traffic light and change the car's behavior accordingly
    private void CheckLightStatus(TrafficLight.LightColor trafficLight)
    {
        // Switch statement to handle the different traffic light colors
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
                // If the traffic light color isn't recognized, throw an error
                throw new ArgumentOutOfRangeException();
        }
    }
}