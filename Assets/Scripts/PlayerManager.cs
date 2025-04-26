using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }
    [SerializeField] private GameObject playerPrefab1;
    [SerializeField] private GameObject playerPrefab2;

    [SerializeField] private float player1Health = 100f;
    [SerializeField] private float player2Health = 100f;

    [SerializeField] private float player1Rounds = 0f;
    [SerializeField] private float player2Rounds = 0f;

    [SerializeField] private float player1Wins = 0f;
    [SerializeField] private float player2Wins = 0f;

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
