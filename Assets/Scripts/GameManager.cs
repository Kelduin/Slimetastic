using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject[] slimePrefabs;
    public TMP_Text scoreText;
    public GameObject gameOverPanel;
    public Transform spawningZone;                // Reference to SpawningZone
    public SpriteRenderer showcaseSlimeRenderer;  // SpriteRenderer for the showcase slime
    public TMP_Text highScoreText;
    public Image nextSlimeDisplay;
    private int score;
    private bool isGameOver = false;
    private int nextSlimeTier;
    private int upcomingSlimeTier;
    private float spawnZoneRadius = 1.2f;         // Adjust based on your box width
    private int highScore;
    private Vector3[] slimeScales = {
        new Vector3(3f, 3f, 1), // Scale for tier 1
        new Vector3(4f, 4f, 1), // Scale for tier 2
        new Vector3(5f, 5f, 1), // Scale for tier 3
        
           

    };

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        gameOverPanel.SetActive(false);
        SetNextSlime(); // Display the first showcase slime at start
        highScore = PlayerPrefs.GetInt("HighScore", 0); // Load high score
        highScoreText.text = "High Score: " + highScore;
        SetUpcomingSlime();
    }

    void Update()
    {
        if (!isGameOver)
        {
            Vector3 dropPosition = Vector3.zero;
            bool inputActive = false;

            // Track finger or mouse position continuously
            if (Input.touchCount > 0)
            {
                dropPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                dropPosition.z = 0;
                inputActive = true;
            }
            else
            {
                dropPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                dropPosition.z = 0;
                inputActive = true;
            }

            if (inputActive)
            {
                // Constrain position within SpawningZone radius
                Vector3 constrainedPosition = spawningZone.position;
                constrainedPosition.x = Mathf.Clamp(dropPosition.x, spawningZone.position.x - spawnZoneRadius, spawningZone.position.x + spawnZoneRadius);
                constrainedPosition.y = spawningZone.position.y; // Keep Y-level at SpawningZone height

                // Move the showcase slime to follow mouse/finger position
                showcaseSlimeRenderer.transform.position = constrainedPosition;
            }

            // Spawn slime when the touch or mouse is released
            if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) || Input.GetMouseButtonUp(0))
            {
                SpawnSlime(showcaseSlimeRenderer.transform.position);

            }
        }
    }

    private void SetNextSlime()
    {
        // Set the showcase slime based on the previously stored upcoming slime
        nextSlimeTier = upcomingSlimeTier;

        showcaseSlimeRenderer.sprite = slimePrefabs[nextSlimeTier].GetComponent<SpriteRenderer>().sprite;
        showcaseSlimeRenderer.transform.localScale = slimeScales[nextSlimeTier];

        // Position the showcase slime at the center of the SpawningZone initially
        Vector3 initialShowcasePosition = spawningZone.position;
        initialShowcasePosition.y = spawningZone.position.y;
        showcaseSlimeRenderer.transform.position = initialShowcasePosition;

        // Select a new upcoming slime for the next preview
        SetUpcomingSlime();
    }

    private void SetUpcomingSlime()
    {
        // Set the upcoming slime tier and update the preview display
        upcomingSlimeTier = GetRandomTier();
        nextSlimeDisplay.sprite = slimePrefabs[upcomingSlimeTier].GetComponent<SpriteRenderer>().sprite;
    }

    private int GetRandomTier()
    {
        int[] tierWeights = { 3, 2, 1, 0 }; // Weighted probabilities for tiers 1 to 4
        int totalWeight = 0;

        foreach (int weight in tierWeights)
            totalWeight += weight;

        int randomValue = Random.Range(0, totalWeight);
        int cumulativeWeight = 0;

        for (int tier = 0; tier <= 3; tier++) // Iterate only from tier 1 to tier 4
        {
            cumulativeWeight += tierWeights[tier - 0]; // Adjust for array index (0-based)
            if (randomValue < cumulativeWeight)
                return tier; // Return the selected tier (1 to 4)
        }

        return 0; // Fallback to tier 1
    }
    public void TryAgain()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void Menu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        Application.Quit();
    }
    public void SpawnSlime(Vector3 dropPosition)

    {
        if (!isGameOver)
        {
            // Instantiate the next slime at the specified drop position
            Instantiate(slimePrefabs[nextSlimeTier], dropPosition, Quaternion.identity);
            AudioManager.Instance.PlayDropSound();

            // Update the showcase slime and preview for the next spawn
            SetNextSlime();
        }
    }

    public void AddScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        isGameOver = true;
        gameOverPanel.SetActive(true);
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore); // Save the new high score
            PlayerPrefs.Save();
        }

        highScoreText.text = "High Score: " + highScore;
    }
}