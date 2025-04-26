using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    // Time in seconds before the GameObject is destroyed
    public float timeToDestroy = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        // Schedule the destruction of the GameObject
        Destroy(gameObject, timeToDestroy);
    }
}
