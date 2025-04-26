using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    private PlayerManager playerManager;

    void Start()
    {
        playerManager = FindFirstObjectByType<PlayerManager>();

        if (playerManager == null)
        {
            Debug.LogError("PlayerManager not found in the scene!");
        }
    }
}
