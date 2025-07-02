using System;
using UnityEngine;

public class MovingPlatformUpAndDown : MonoBehaviour
{
    public float distance = 5f;
    public float speed = 2f;

    private Vector3 initialPosition;

    // Start is called before the first frame update
    public virtual void Start()
    {
        
        
            initialPosition = transform.position;
        
    }

    // FixedUpdate is called once per frame (physics updates)
    public virtual void FixedUpdate()
    {
        float platformMovingY = Mathf.PingPong(Time.time * speed, distance);

        transform.position = new Vector3(initialPosition.x, initialPosition.y + platformMovingY, initialPosition.z);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(this.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}