using UnityEngine;

public class FireController : MonoBehaviour
{
    [SerializeField] private float shrinkSpeed = 0.2f;
    [SerializeField] private UiManager uiManager;
    private Vector3 startScale;
    private float size = 1f;
    public bool shrinked = false;

    private void Start()
    {
        startScale = transform.localScale;
    }

    public void Shrink()
    {
        size -= shrinkSpeed * Time.deltaTime;

        if (size <= 0.1f)
        {
            shrinked = true;
            gameObject.SetActive(false);
            uiManager.instructionTxt.text = "Well done! You successfully extinguished the fire. Would you like to try again?";
            uiManager.retryButton.gameObject.SetActive(true);
            return;
        }
        transform.localScale = startScale * size;
    }
}