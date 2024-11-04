using UnityEngine;

public class InfiniteScrollingSky : MonoBehaviour
{
    public Transform skyLayer1; // Reference to your first sky layer
    public Transform skyLayer2; // Reference to your second sky layer
    public float scrollSpeed = 2f; // Speed at which the sky layers scroll
    public float resetThreshold = 12.78804f; // Threshold for resetting position (effective width)

    private void Update()
    {
        // Move both sky layers to the right
        skyLayer1.position += Vector3.right * scrollSpeed * Time.deltaTime;
        skyLayer2.position += Vector3.right * scrollSpeed * Time.deltaTime;

        // Check if skyLayer1 has moved out of view
        if (skyLayer1.position.x > resetThreshold)
        {
            // Reset skyLayer1 position to the left of skyLayer2
            skyLayer1.position = new Vector3(skyLayer2.position.x - 12.78804f, skyLayer1.position.y, skyLayer1.position.z);
        }

        // Check if skyLayer2 has moved out of view
        if (skyLayer2.position.x > resetThreshold)
        {
            // Reset skyLayer2 position to the left of skyLayer1
            skyLayer2.position = new Vector3(skyLayer1.position.x - 12.78804f, skyLayer2.position.y, skyLayer2.position.z);
        }
    }
}
