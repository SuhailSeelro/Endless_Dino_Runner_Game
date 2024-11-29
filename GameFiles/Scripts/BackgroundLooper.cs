using UnityEngine;

public class BackgroundLooper : MonoBehaviour
{
    public float scrollSpeed = 5f; // Speed at which the background moves
    public float backgroundWidth = 20f; // Width of the background image

    private Vector3 startPosition;

    void Start()
    {
        // Record the initial position of the background
        startPosition = transform.position;
    }

    void Update()
    {
        // Move the background to the left
        transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);

        // Check if the background has moved completely off-screen
        if (transform.position.x <= startPosition.x - backgroundWidth)
        {
            // Reposition the background to the right
            transform.position = new Vector3(startPosition.x + backgroundWidth, transform.position.y, transform.position.z);
        }
    }
}
