using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }
    [SerializeField] private GameObject playerPrefab1;
    [SerializeField] private GameObject playerPrefab2;
    public float player1Health = 100f;
    public float player2Health = 100f;

    public int player1Rounds = 0;
    public int player2Rounds = 0;

    public int player1Wins = 0;
    public int player2Wins = 0;

    //player 1 is assassino and player 2 is la vaca
    public int player1Character = 0;
    public int player2Character = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void OnPlayerDeath(string playerTag)
    {
        if (playerTag == "P1")
        {
            player1Health = 0;
            player2Wins++;
        }
        else if (playerTag == "P2")
        {
            player2Health = 0;
            player1Wins++;
        }
    }

    // Add any player-related properties or methods here
}   
