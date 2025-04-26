using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{

    public GameObject characterPrefab1;
    public GameObject characterPrefab2;
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

        if (playerManager.player1Character == 0)
        {
            player1Character = Instantiate(characterPrefab1, new Vector3(-3f, 0, 6), Quaternion.identity);
        }
        else if (playerManager.player1Character == 1)
        {
            player1Character = Instantiate(characterPrefab2, new Vector3(-3f, 0, 6), Quaternion.identity);
        }
        player1Character.tag = "P1"; // Assign tag to player1

        if (playerManager.player2Character == 0)
        {
            player2Character = Instantiate(characterPrefab1, new Vector3(3f, 0, 6), Quaternion.identity);
            player2Character.transform.localScale = new Vector3(-1, 1, 1); // Flip the character
        }
        else if (playerManager.player2Character == 1)
        {
            player2Character = Instantiate(characterPrefab2, new Vector3(3f, 0, 6), Quaternion.identity);
            player2Character.transform.localScale = new Vector3(-1, 1, 1); // Flip the character
        }
        player2Character.tag = "P2"; // Assign tag to player2
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
