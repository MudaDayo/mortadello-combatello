using UnityEngine;

public class PingPongSpectator : MonoBehaviour
{
    [SerializeField]
    private float speed = 2f;
    
    [SerializeField]
    private float range = 3f;
    
    private float originalX;
    
    // Start is called before the first frame update
    void Start()
    {
        originalX = transform.position.x; // Store the original X position
    }

    // Update is called once per frame
    void Update()
    {
        // float newX = originalX + Mathf.PingPong(Time.time * speed, range); // Calculate the new X position
        // use sine
        float newX = originalX + Mathf.Sin(Time.time * speed) * range; // Calculate the new X position using sine
        transform.position = new Vector3(newX, transform.position.y, transform.position.z); // Update the position
    }
}
