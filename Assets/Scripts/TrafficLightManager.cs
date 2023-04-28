using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightManager : MonoBehaviour
{
    [SerializeField] private TrafficLight[] trafficLights;
    [SerializeField] private TrafficLight[] pedLights;

    
    private void Update()
    {
        foreach (var trafficLight in trafficLights)
        {
            if (trafficLight.hasPedLight)
            {
                CheckLights();
            }
        }
    }

    private void CheckLights()
    {
        foreach (var t1 in trafficLights)
        {
            foreach (var t in pedLights)
            {
                if (t1.CurrentColor() == TrafficLight.LightColor.Green)
                {
                    t.OverrideColor(TrafficLight.LightColor.Red);
                }
                else if(t1.CurrentColor() == TrafficLight.LightColor.Red)
                {
                    t.OverrideColor(TrafficLight.LightColor.Green);
                }
            }
        }
    }
}
