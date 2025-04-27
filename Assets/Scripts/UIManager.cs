using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    // Healthbars
    public Slider player1HealthBar;
    public Slider player2HealthBar;

    public GameObject replayPanelCappucino;
    public GameObject replayPanelVacaSaturno;

    public TextMeshProUGUI player1WinsText;
    public TextMeshProUGUI player2WinsText;

    void UpdateWinsText()
    {
        if (playerManager != null)
        {
            player1WinsText.text = playerManager.player1Wins > 0 ? "WINS: " + playerManager.player1Wins.ToString() : string.Empty;
            player2WinsText.text = playerManager.player2Wins > 0 ? "WINS: " + playerManager.player2Wins.ToString() : string.Empty;
        }
    }

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

        playerManager.uiManager = this; // Set the reference to this UIManager instance in PlayerManager

        if (playerManager == null)
        {
            Debug.LogError("PlayerManager not found in the scene!");
        }else{
            // Set character names based on playerManager values
            player1Name.text = playerManager.player1Character == 0 ? "CAPPUCINO ASASSINO" : "LA VACA SATURNO SATURNITA";
            player2Name.text = playerManager.player2Character == 0 ? "CAPPUCINO ASASSINO" : "LA VACA SATURNO SATURNITA";
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateWinsText();
        // Update health sliders based on PlayerManager health values
        if (playerManager != null)
        {
            player1HealthBar.value = playerManager.player1Health / 100f;
            player2HealthBar.value = playerManager.player2Health / 100f;

            // Ensure round win arrays are properly initialized
            if (player1RoundWins.Length != playerManager.maxRounds || player2RoundWins.Length != playerManager.maxRounds)
            {
                Debug.LogError("Round win arrays do not match the maximum number of rounds in PlayerManager!");
                return;
            }

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

    public void CappucinoWins()
    {
        replayPanelCappucino.SetActive(true);
        Time.timeScale = 0.1f; // Pause the game
    }

    public void VacaSaturnoWins()
    {
        replayPanelVacaSaturno.SetActive(true);
        Time.timeScale = 0f; // Pause the game
    }
}
