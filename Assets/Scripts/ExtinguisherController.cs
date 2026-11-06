using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ExtinguisherController : MonoBehaviour
{
    [SerializeField] private SafetyPinController pinController;
    [SerializeField] private GameObject foamParticleEffect;
    [SerializeField] private XRGrabInteractable xrGrab;
    [SerializeField] private InputActionProperty pinchButton;
    [SerializeField] private FireController fireController;
    [SerializeField] private Animator pinchAnimator;
    [SerializeField] private float rayLength = 10f;

    private void Update()
    {
        bool grabbed = xrGrab.isSelected;
        bool pinPulled = pinController.isPulled;
        bool pinching = pinchButton.action.IsPressed();
        if (pinching)
        {
            pinchAnimator.SetBool("IsFire", true);
        }
        else
        {
            pinchAnimator.SetBool("IsFire", false);
        }

        if (grabbed && pinPulled && pinching)
        {
            foamParticleEffect.SetActive(true);
            ShootRay();
            Debug.Log("Particle on");
        }
        else
        {
            foamParticleEffect.SetActive(false);
        }
    }
    private void ShootRay()
    {

        Vector3 start = foamParticleEffect.transform.position;
        Vector3 direction = foamParticleEffect.transform.up;
        Debug.DrawRay(start, direction * rayLength, Color.red);
        if (Physics.Raycast(start, direction, out RaycastHit hit, rayLength))
        {
            
            if (hit.collider.CompareTag("Fire"))
            {
                fireController.Shrink();
            }
        }
    }
}