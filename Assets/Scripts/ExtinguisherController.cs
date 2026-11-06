using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ExtinguisherController : MonoBehaviour
{
    [SerializeField] private SafetyPinController pinController;
    [SerializeField] private GameObject foamParticleEffect;
    [SerializeField] private XRGrabInteractable xrGrab;
    [SerializeField] private InputActionProperty pinchButton;
    private void Update()
    {
        bool grabbed = xrGrab.isSelected;
        bool pinPulled = pinController.isPulled;
        bool pinching = pinchButton.action.IsPressed();

        if (grabbed && pinPulled && pinching)
        {
            foamParticleEffect.SetActive(true);
        }
        else
        {
            foamParticleEffect.SetActive(false);
        }
    }
}