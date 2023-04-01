using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualLight : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    [SerializeField] private Material lightOn;
    [SerializeField] private Material lightOff;
    
    private void Awake()
    {
        TryGetComponent(out _meshRenderer);
    }

    public void TurnOnLight()
    {
        _meshRenderer.material = lightOn;
    }

    public void TurnOffLight()
    {
        _meshRenderer.material = lightOff;
    }
}
