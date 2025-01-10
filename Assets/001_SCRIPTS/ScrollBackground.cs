using UnityEngine;

public class ScrollBackground : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float resetPositionY;
    [SerializeField] private float startPositionY;

    void Update()
    {
        transform.position += new Vector3(0, -speed * Time.deltaTime, 0);
        if (transform.position.y <= resetPositionY)
        {
            transform.position = new Vector3(transform.position.x, startPositionY, transform.position.z);
        }
    }
}