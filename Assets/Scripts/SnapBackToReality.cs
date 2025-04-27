using UnityEngine;

public class SnapBackToReality : MonoBehaviour
{
    public Transform targetTransform;
    public float maxDistance = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject targetObject = GameObject.FindGameObjectWithTag("zero");
        if (targetObject != null)
        {
            targetTransform = targetObject.transform;
        }
        else
        {
            Debug.LogWarning("No GameObject with tag 'zero' found in the scene.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, targetTransform.position) > maxDistance)
        {
            transform.position = targetTransform.position;
        }
    }
}
