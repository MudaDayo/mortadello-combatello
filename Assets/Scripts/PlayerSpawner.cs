using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject player1Character;
    public GameObject player2Character;
      private PlayerManager playerManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerManager = FindFirstObjectByType<PlayerManager>();

        if (playerManager == null)
        {
            Debug.LogError("PlayerManager not found in the scene!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
