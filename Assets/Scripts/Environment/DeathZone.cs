using Managers;
using UnityEngine;

namespace Environment
{
    public class DeathZone : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(Constants.ObstacleTag))
            {
                Destroy(other.gameObject);
            }
        }
    }
}