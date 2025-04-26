using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }
    [SerializeField] private GameObject playerPrefab1;
    [SerializeField] private GameObject playerPrefab2;

    public float player1Health = 100f;
    public float player2Health = 100f;

    public float player1Rounds = 0f;
    public float player2Rounds = 0f;

    public float player1Wins = 0f;
    public float player2Wins = 0f;

    //player 1 is assassino and player 2 is la vaca
    public float player1Character = 0f;
    public float player2Character = 0f;

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

    // Add any player-related properties or methods here
}   
