using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    // Healthbars
    public Slider player1HealthBar;
    public Slider player2HealthBar;

    // Round win counters
    public Image[] player1RoundWins; // Array of images for Player 1
    public Image[] player2RoundWins; // Array of images for Player 2

    // Timer text
    public TextMeshProUGUI timerText;

    // Character names
    public TextMeshProUGUI player1Name;
    public TextMeshProUGUI player2Name;

    // Reference to PlayerManager
    private PlayerManager playerManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Find the PlayerManager instance
        playerManager = FindFirstObjectByType<PlayerManager>();

        if (playerManager == null)
        {
            Debug.LogError("PlayerManager not found in the scene!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Update health sliders based on PlayerManager health values
        if (playerManager != null)
        {
            player1HealthBar.value = playerManager.player1Health;
            player2HealthBar.value = playerManager.player2Health;

            // Update round win images for Player 1
            for (int i = 0; i < player1RoundWins.Length; i++)
            {
                player1RoundWins[i].enabled = i < playerManager.player1Rounds;
            }

            // Update round win images for Player 2
            for (int i = 0; i < player2RoundWins.Length; i++)
            {
                player2RoundWins[i].enabled = i < playerManager.player2Rounds;
            }
        }
    }
}
