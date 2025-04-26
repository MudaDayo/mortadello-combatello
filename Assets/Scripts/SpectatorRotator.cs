using UnityEngine;

public class SpectatorRotator : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 20f;
    
    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
