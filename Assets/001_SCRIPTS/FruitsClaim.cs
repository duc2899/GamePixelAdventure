using System;
using UnityEngine;

public class FruitsClaim : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Collider2D colliderF;
    [SerializeField] private Rigidbody2D rigidbodyF;

    private bool _isClaimed = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(CONSTANT.PLAYER_TAG) && !_isClaimed)
        {
            _isClaimed = true;
            animator.SetTrigger("isClaimed");

            // Vô hiệu hóa collider để tránh va chạm lặp
            if (colliderF != null)
            {
                colliderF.enabled = false;
            }

            Destroy(gameObject, 0.5f);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(CONSTANT.GROUND_TAG))
        {
            if (rigidbodyF != null)
            {
                rigidbodyF.bodyType = RigidbodyType2D.Static;
            }

            if (colliderF != null)
            {
                colliderF.isTrigger = true;
            }
        }
    }
}