using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightManager : MonoBehaviour
{
    [SerializeField] private TrafficLight[] topTrafficLights;
    [SerializeField] private TrafficLight[] topPedLights;
    [SerializeField] private TrafficLight[] bottomTrafficLights;
    [SerializeField] private TrafficLight[] bottomPedLights;

    
    private void Update()
    {
        CheckLights(topTrafficLights, topPedLights);
        CheckLights(bottomTrafficLights, bottomPedLights);
    }

    private void CheckLights(TrafficLight[] trafficLights, TrafficLight[] pedLights)
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
