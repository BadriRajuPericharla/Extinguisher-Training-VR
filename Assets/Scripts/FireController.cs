using UnityEngine;

public class FireController : MonoBehaviour
{
    [SerializeField] private float shrinkSpeed = 0.3f;
    private Vector3 startScale;
    private float size = 1f;

    private void Start()
    {
        startScale = transform.localScale;
    }

    public void Shrink()
    {
        size -= shrinkSpeed * Time.deltaTime;

        if (size <= 0.1f)
        {
            gameObject.SetActive(false);
            return;
        }
        transform.localScale = startScale * size;
    }
}