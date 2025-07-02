using UnityEngine;

public class UpAndDown : MonoBehaviour
{
    public Transform position;
    public float speed;
    public float distance;
    public Vector3 initialPosition;

    void FixedUpdate()
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