using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 targetPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var difference = targetPos - transform.position;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref difference, 0.2f);
    }
}
