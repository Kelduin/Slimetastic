using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Slime : MonoBehaviour
{
    public int tier;  // Tier of the slime, e.g., 1 to 4
    private bool isImmune = true; // Immunity flag
    private bool hasMerged = false; // Prevent multiple merges

    private void Start()
    {
        // Set a 0.5-second immunity on spawn
        Invoke("DisableImmunity", 0.05f);
    }

    private void DisableImmunity()
    {
        isImmune = false;
    }

    public bool IsImmune()
    {
        return isImmune; // Expose immunity state
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Slime otherSlime = collision.gameObject.GetComponent<Slime>();

        // Handle merging only if otherSlime exists, is the same tier, and has not merged yet
        if (otherSlime != null && otherSlime.tier == tier && !hasMerged && !otherSlime.hasMerged)
        {
            MergeSlimes(otherSlime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the slime touches the spawning zone after immunity wears off
        if (other.CompareTag("SpawningZone") && !isImmune)
        {
            GameManager.Instance.GameOver();
        }
    }
    private IEnumerator EnableMerging()
    {
        yield return new WaitForSeconds(0.1f);
        hasMerged = false;
    }

    private void MergeSlimes(Slime other)
    {
        hasMerged = true;
        other.hasMerged = true;

        // Destroy the current and other slime to prevent further merging
        Destroy(other.gameObject);
        Destroy(this.gameObject);
        AudioManager.Instance.PlayMergeSound();

        if (tier < 9) // Change from tier < 10 to tier < 9 to avoid index out of range
        {
            // Instantiate the new slime as one tier above the current one
            GameObject newSlime = Instantiate(
                GameManager.Instance.slimePrefabs[tier + 1], // Correctly instantiate next tier slime
                transform.position,
                Quaternion.identity
            );
            GameManager.Instance.AddScore(tier + 1); // Add points based on the tier just merged (consider using tier + 1 if 1-indexed for scoring)

            // Delay merging for the new slime to avoid instant re-merge
            Slime newSlimeComponent = newSlime.GetComponent<Slime>();
            newSlimeComponent.StartCoroutine(newSlimeComponent.EnableMerging());
        }
    }


}
