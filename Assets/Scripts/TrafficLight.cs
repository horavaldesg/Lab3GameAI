using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLight : MonoBehaviour
{
   [SerializeField] private IndividualLight redLight;
   [SerializeField] private IndividualLight yellowLight;
   [SerializeField] private IndividualLight greenLight;
   [SerializeField] private float lightSwitchTime;
   
   public enum LightColor
   {
      Red = 2,
      Yellow = 1,
      Green = 0
   };

   public LightColor lightColor;
   
   private float _t;
   private int _lightIndex;

   private void Awake()
   {
      _lightIndex = 0;
      lightColor = LightColor.Green;
   }

   private void Start()
   {
      CheckLightColor();
   }

   private void Update()
   {
      _t += Time.deltaTime * 2.5f;

      if (!(_t > lightSwitchTime)) return;
      
      _lightIndex++;
      ChangeLightColor();
   }

   private void ChangeLightColor()
   {
      if (_lightIndex > Enum.GetValues(typeof(LightColor)).Length - 1)
      {
         _lightIndex = 0;
      }


      lightColor = (LightColor)_lightIndex;
      CheckLightColor();
   }

   private void GreenLight()
   {
      greenLight.TurnOnLight();
      yellowLight.TurnOffLight();
      redLight.TurnOffLight();
   }

   private void YellowLight()
   {
      greenLight.TurnOffLight();
      yellowLight.TurnOnLight();
      redLight.TurnOffLight();
   }

   private void RedLight()
   {
      greenLight.TurnOffLight();
      yellowLight.TurnOffLight();
      redLight.TurnOnLight();
   }
   
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
      
      _t = 0;
   }
}
