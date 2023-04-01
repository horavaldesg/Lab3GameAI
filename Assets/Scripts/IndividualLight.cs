using UnityEngine;

public class IndividualLight : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    [SerializeField] private Material lightOn; // The material used when the light is turned on.
    [SerializeField] private Material lightOff; // The material used when the light is turned off.

    private void Awake()
    {
        TryGetComponent(out _meshRenderer); // Try to get the MeshRenderer component.
    }

    // Turn on the light by setting the material to the lightOn material.
    public void TurnOnLight()
    {
        _meshRenderer.material = lightOn;
    }

    // Turn off the light by setting the material to the lightOff material.
    public void TurnOffLight()
    {
        _meshRenderer.material = lightOff;
    }
}