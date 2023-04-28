using System;
using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    public enum RoadSide
    {
        Right,
        Left,
        Ped
    }

    public RoadSide roadSide;
    
    // Variables that store the individual lights and the time to switch between them
    [SerializeField] private IndividualLight redLight;
    [SerializeField] private IndividualLight yellowLight;
    [SerializeField] private IndividualLight greenLight;
    [SerializeField] private float lightSwitchTime;

    public bool hasPedLight;
    
    // Enumeration that represents the different colors of the traffic light
    public enum LightColor
    {
        Red = 2,
        Yellow = 1,
        Green = 0
    };

    // Current color of the traffic light
    public LightColor lightColor;

    // Private variables to keep track of time and the index of the current light color
    private float _t;
    private int _lightIndex;

    private void Awake()
    {
        _lightIndex = 0;
        lightColor = LightColor.Green;
    }

    private void Start()
    {
        // Checks the current light color at the start of the program
        CheckLightColor();
        SetLayer(GetSideLayer());
    }
    
    private int GetSideLayer()
    {
        return roadSide switch
        {
            RoadSide.Left => 10,
            RoadSide.Right => 11,
            RoadSide.Ped => 12,
            _ => 0
        };
    }
    
    private void SetLayer(int layer)
    {
        transform.gameObject.layer = layer;
    }


    private void Update()
    {
        // Increases the time variable by the time since the last frame multiplied by 2.5
        _t += Time.deltaTime * 2.5f;

        // If the time is greater than the light switch time, change the light color
        if (!(_t > lightSwitchTime)) return;

        _lightIndex++;
        ChangeLightColor();
    }

    // Changes the light color to the next one
    private void ChangeLightColor()
    {
        // Resets the light index if it exceeds the maximum value of the LightColor enumeration
        if (_lightIndex > Enum.GetValues(typeof(LightColor)).Length - 1)
        {
            _lightIndex = 0;
        }

        // Sets the current light color based on the current index and checks the light color
        lightColor = (LightColor)_lightIndex;
        CheckLightColor();
    }

    public void OverrideColor(LightColor lightColor)
    {
        this.lightColor = lightColor;
    }

    // Turns on the green light and turns off the yellow and red lights
    private void GreenLight()
    {
        greenLight.TurnOnLight();
        yellowLight.TurnOffLight();
        redLight.TurnOffLight();
    }

    // Turns off the green and red lights and turns on the yellow light
    private void YellowLight()
    {
        greenLight.TurnOffLight();
        yellowLight.TurnOnLight();
        redLight.TurnOffLight();
    }

    // Turns off the green and yellow lights and turns on the red light
    private void RedLight()
    {
        greenLight.TurnOffLight();
        yellowLight.TurnOffLight();
        redLight.TurnOnLight();
    }

    // Checks the current light color and calls the appropriate function to change the lights
    private void CheckLightColor()
    {
        switch (lightColor)
        {
            case LightColor.Green:
                GreenLight();
                break;
            case LightColor.Yellow:
                YellowLight();
                break;
            case LightColor.Red:
                RedLight();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        // Resets the time variable to 0
        _t = 0;
    }

    public LightColor CurrentColor()
    {
        return lightColor;
    }
}
