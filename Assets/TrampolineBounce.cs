using UnityEngine;

public class TrampolineBounce : MonoBehaviour
{
    public float bounceForce = 10f; // Adjust this value for the desired bounce height

    private void OnCollisionStay2D(Collision2D collision)
    {
        Slime slime = collision.gameObject.GetComponent<Slime>();
        if (slime != null) // Check if the colliding object is a slime
        {
            Rigidbody2D slimeRigidbody = slime.GetComponent<Rigidbody2D>();
            if (slimeRigidbody != null)
            {
                // Ensure the slime is not already bouncing upwards before applying force
                if (slimeRigidbody.velocity.y <= 0)
                {
                    // Apply an upward force to the slime's Rigidbody2D
                    slimeRigidbody.velocity = new Vector2(slimeRigidbody.velocity.x, bounceForce);
                }
            }
        }
    }
}
