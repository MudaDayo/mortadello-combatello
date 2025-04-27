using UnityEngine;

public class FloatingSpectator : MonoBehaviour
{
    [SerializeField]
    private float floatingSpeed = 2f;

    void Update()
    {
        float newY = Mathf.Sin(Time.time * floatingSpeed) * 0.5f; // Adjust the multiplier for height
        newY += transform.position.y; // Maintain the original Y position
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
