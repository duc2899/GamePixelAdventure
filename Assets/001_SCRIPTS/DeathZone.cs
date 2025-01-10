using UnityEngine;

public class DeathZone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(CONSTANT.OBSTACLE_TAG))
        {
            Destroy(other.gameObject);
        }
    }
}