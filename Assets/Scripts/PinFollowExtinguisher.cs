using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PinFollowExtinguisher : MonoBehaviour
{
    [SerializeField] private Transform extinguisher;              
    [SerializeField] private XRGrabInteractable grabInteractable;  
    [SerializeField] private SafetyPinController pinController;    
    private Vector3 startLocalPosition;
    private Quaternion startLocalRotation;
    private void Start()
    {
        startLocalPosition = extinguisher.InverseTransformPoint(transform.position);
        startLocalRotation = Quaternion.Inverse(extinguisher.rotation) * transform.rotation;
    }
    private void LateUpdate()
    {
        if (pinController.isPulled) return;
        if (grabInteractable.trackPosition) return;
        transform.position = extinguisher.TransformPoint(startLocalPosition);
        transform.rotation = extinguisher.rotation * startLocalRotation;
    }
}