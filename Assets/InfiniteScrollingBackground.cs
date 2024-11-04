using UnityEngine;

public class InfiniteScrollingBackground : MonoBehaviour
{
    public Transform[] skyLayers;             // Reference to the sky layers
    public Transform[] starsLayers;           // Reference to the stars layers
    public Transform[] cloudsLayers;          // Reference to the clouds layers
    public Transform[] closerCloudsLayers;    // Reference to the closer clouds layers

    public float skyScrollSpeed = 0.1f;       // Speed of scrolling for the sky
    public float starsScrollSpeed = 0.5f;     // Speed of scrolling for the stars
    public float cloudsScrollSpeed = 0.75f;   // Speed of scrolling for the clouds
    public float closerCloudsScrollSpeed = 1f; // Speed of scrolling for the closer clouds

    public float resetThreshold = 12.62f;     // Effective width (camera width / 2)

    private void Update()
    {
        // Move all layers with their specific speeds
        MoveLayers(skyLayers, skyScrollSpeed);
        MoveLayers(starsLayers, starsScrollSpeed);
        MoveLayers(cloudsLayers, cloudsScrollSpeed);
        MoveLayers(closerCloudsLayers, closerCloudsScrollSpeed);
    }

    private void MoveLayers(Transform[] layers, float speed)
    {
        foreach (Transform layer in layers)
        {
            layer.position += Vector3.right * speed * Time.deltaTime;
            ResetLayerPosition(layer);
        }
    }

    private void ResetLayerPosition(Transform layer)
    {
        if (layer.position.x > resetThreshold)
        {
            // Reset position to the left of the other layers
            layer.position = new Vector3(-resetThreshold, layer.position.y, layer.position.z);
        }
    }
}
