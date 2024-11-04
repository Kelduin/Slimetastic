using UnityEngine;

public class LoseZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Slime slime = collision.gameObject.GetComponent<Slime>();
        if (slime != null)
        {
            // Check if slime is still immune
            if (slime.IsImmune())
            {
                // Ignore collision if slime is still immune
                return;
            }

            // Trigger game over
            GameManager.Instance.GameOver();
        }
    }
}
