using UnityEngine;

public class IdleAnimation : MonoBehaviour
{
    public float squishSpeed = 1.0f;
    public float squishAmount = 0.1f;
    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        float scale = Mathf.Sin(Time.time * squishSpeed) * squishAmount;
        transform.localScale = new Vector3(originalScale.x + scale, originalScale.y - scale, originalScale.z + scale);
    }
}
