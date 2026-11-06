
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] private GameObject instructionsPanel;
    [SerializeField] private TextMeshProUGUI instructionTxt;
    [SerializeField] private ExtinguisherController extinguisherController;

    private void Start()
    {
        instructionsPanel.SetActive(true);
        instructionTxt.text = "Hey! There is a fire. Take the extinguisher and put it out.";
    }

    private void Update()
    {
        if (!extinguisherController.grabbed)
        {
            instructionTxt.text = "Take the fire extinguisher.";
        }
        else if (extinguisherController.grabbed && !extinguisherController.pinPulled)
        {
            instructionTxt.text = "Nice! You have the extinguisher. Now pull the safety pin.";
        }
        else if (extinguisherController.grabbed && extinguisherController.pinPulled && !extinguisherController.pinching)
        {
            instructionTxt.text = "Excellent! Move near the fire, aim the extinguisher and squeeze the handle.";
        }
        else if (extinguisherController.grabbed && extinguisherController.pinPulled && extinguisherController.pinching)
        {
            instructionTxt.text = "Great! Keep aiming at the fire and spray.";
        }
    }
}
