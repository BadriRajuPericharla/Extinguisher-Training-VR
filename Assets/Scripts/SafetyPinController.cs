using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SafetyPinController : MonoBehaviour
{
    [SerializeField] private XRGrabInteractable grabInteractable;
    [SerializeField] private float pullNeeded = 0.03f;
    [SerializeField] private float maxPull = 0.15f;

    private Rigidbody rb;
    private Transform extinguisher;    
    private Vector3 pinStartLocalPosition;
    private Vector3 pullAxis;
    private Vector3 handStartLocal;
    private bool gotHandStart = false;
    private bool pinFree = false;
    public bool isPulled = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        grabInteractable.trackRotation = false;
        grabInteractable.trackPosition = true;
    }

    private void Start()
    {
        extinguisher = transform.parent;
        pinStartLocalPosition = transform.localPosition;
        pullAxis = -(transform.localRotation * Vector3.forward);
    }

    private void Update()
    {
        if (isPulled) return;

        if (!grabInteractable.isSelected)
        {
            if (pinFree)
            {
                pinFree = false;
                grabInteractable.trackPosition = false;
                transform.localPosition = pinStartLocalPosition;
            }
            gotHandStart = false;
            return;
        }

        Transform hand = grabInteractable.firstInteractorSelecting.transform;
        Vector3 handLocal = extinguisher.InverseTransformPoint(hand.position);

        if (!gotHandStart)
        {
            handStartLocal = handLocal;
            gotHandStart = true;
        }

        if (!pinFree)
        {
            float handPulled = Vector3.Dot(handLocal - handStartLocal, pullAxis);
            if (handPulled >= pullNeeded)
            {
                pinFree = true;
                grabInteractable.trackPosition = true;
            }
            return;
        }

        float pinMoved = Vector3.Dot(transform.localPosition - pinStartLocalPosition, pullAxis);
        if (pinMoved < 0f)
        {
            transform.localPosition = pinStartLocalPosition;
            return;
        }
        if (pinMoved >= maxPull)
        {
            DropPin();
        }
    }

    private void DropPin()
    {
        isPulled = true;
        grabInteractable.trackPosition = true;
        grabInteractable.interactionManager.SelectExit(grabInteractable.firstInteractorSelecting, grabInteractable);
        grabInteractable.enabled = false;
        transform.SetParent(null);
        rb.isKinematic = false;
        rb.useGravity = true;
        Debug.Log("Safety pin pulled!");
    }
}