using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SafetyPinController : MonoBehaviour
{
    [SerializeField] private XRGrabInteractable grabInteractable;
    [SerializeField] private float pullNeeded = 0.05f;
    [SerializeField] private float maxPull = 0.3f;
    [SerializeField] private bool reverseDirection = false;
    private Rigidbody rb;
    private Vector3 pinStartPosition;
    private Vector3 pullAxis;
    private Vector3 handStart;
    private bool gotHandStart = false;
    private bool pinFree = false;
    public bool isPulled = false;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        grabInteractable.trackRotation = false;
        grabInteractable.trackPosition = false;
    }

    private void Start()
    {
        pinStartPosition = transform.position;
        pullAxis = reverseDirection ? -transform.forward : transform.forward;
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
                transform.position = pinStartPosition;
            }
            gotHandStart = false;
            return;
        }
        Transform hand = grabInteractable.firstInteractorSelecting.transform;
        if (!gotHandStart)
        {
            handStart = hand.position;
            gotHandStart = true;
        }
        if (!pinFree)
        {
            float handPulled = Vector3.Dot(hand.position - handStart, pullAxis);
            if (handPulled >= pullNeeded)
            {
                pinFree = true;
                grabInteractable.trackPosition = true;
            }
            return;
        }
        float pinMoved = Vector3.Dot(transform.position - pinStartPosition, pullAxis);
        if (pinMoved < 0f)
        {
            transform.position = pinStartPosition;
            return;
        }
        if (pinMoved >= maxPull)
        {
            DropPin();
        }
    }

    private void DropPin()
    {
        AudioManager.instance.PlayPinDropSfx();
        isPulled = true;
        grabInteractable.interactionManager.SelectExit(grabInteractable.firstInteractorSelecting, grabInteractable);
        grabInteractable.enabled = false;
        transform.SetParent(null);
        rb.isKinematic = false;
        rb.useGravity = true;
        Debug.Log("Safety pin pulled!");
    }
}