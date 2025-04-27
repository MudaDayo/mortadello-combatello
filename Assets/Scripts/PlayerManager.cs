using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }
    [SerializeField] private GameObject playerPrefab1;
    [SerializeField] private GameObject playerPrefab2;

    public PlayerSpawner playerSpawner;

    public UIManager uiManager;
    public int maxRounds = 3;
    public GameObject player1;
    public GameObject player2;

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

        if(player1Health <= 0 || player2Health <= 0)
        {
            Destroy(player1);
            Destroy(player2);
        
            playerSpawner.SpawnPlayers(); // Respawn players after death
        }

        if (playerTag == "P1")
        {
            player2Rounds++;
        }
        else if (playerTag == "P2")
        {
            player1Rounds++;
        }

            player1Health = 100;
            player2Health = 100;


            if (player1Rounds >= maxRounds)
            {
                if(player1Character == 0)
                {
                    uiManager.CappucinoWins();
                }
                else if(player1Character == 1)
                {
                    uiManager.VacaSaturnoWins();
                }

                player1Wins++;
                player1Rounds = 0;
                player2Rounds = 0;
                
            }
            else if (player2Rounds >= maxRounds)
            {
                if(player2Character == 0)
                {
                    uiManager.CappucinoWins();
                }
                else if(player2Character == 1)
                {
                    uiManager.VacaSaturnoWins();
                }

                player2Wins++;
                player1Rounds = 0;
                player2Rounds = 0;
            }
    }

    // Add any player-related properties or methods here
}   
