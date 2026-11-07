
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public TextMeshProUGUI instructionTxt;
    [SerializeField] private ExtinguisherController extinguisherController;
    [SerializeField] private FireController fireController;
    public Button retryButton;

    private void Start()
    {
        retryButton.onClick.AddListener(RetryBtn);
        instructionTxt.text = "Hey! There is a fire. Take the extinguisher and put it out.";
    }

    private void Update()
    {
        if (!extinguisherController.grabbed && !fireController.shrinked)
        {
            instructionTxt.text = "Take the fire extinguisher.";
        }
        else if (extinguisherController.grabbed && !extinguisherController.pinPulled && !fireController.shrinked)
        {
            instructionTxt.text = "Nice! You have the extinguisher. Now pull the safety pin.";
        }
        else if (extinguisherController.grabbed && !extinguisherController.pinPulled && extinguisherController.pinching && !fireController.shrinked)
        {
            instructionTxt.text = "You haven't removed the safety pin! The extinguisher won't work until you pull it out.";
        }
        else if (extinguisherController.grabbed && extinguisherController.pinPulled && !extinguisherController.pinching && !fireController.shrinked)
        {
            instructionTxt.text = "Excellent! Move near the fire, aim the extinguisher and squeeze the handle.";
        }
        else if (extinguisherController.grabbed && extinguisherController.pinPulled && extinguisherController.pinching && !fireController.shrinked)
        {
            if (extinguisherController.aimed)
            {
                instructionTxt.text = "Great! Keep aiming at the fire and spray.";
            }
            else
            {
                instructionTxt.text = "You're either too far away or not aiming at the fire. Move closer and point the extinguisher directly at it!";
            }
            
        }
    }
    public void RetryBtn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
