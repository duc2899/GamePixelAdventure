using System.Collections;
using UnityEngine;

public class FallingPlatformController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Collider2D col;
    [SerializeField] private float bounceAmount = 0.2f;
    [SerializeField] private float bounceSpeed = 0.1f;

    private Vector3 _originalPosition;

    private void Start()
    {
        _originalPosition = transform.position;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Bắt đầu hiệu ứng nhún
        StartCoroutine(BounceEffect());

        // Sau đó bắt đầu hiệu ứng rơi
        StartCoroutine(DelayChangeBodyType(1.5f));
    }

    private IEnumerator BounceEffect()
    {
        float elapsedTime = 0f;


        while (elapsedTime < bounceSpeed)
        {
            transform.position = Vector3.Lerp(_originalPosition,
                _originalPosition - new Vector3(0, bounceAmount, 0),
                elapsedTime / bounceSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }


        elapsedTime = 0f;


        while (elapsedTime < bounceSpeed)
        {
            transform.position = Vector3.Lerp(_originalPosition - new Vector3(0, bounceAmount, 0),
                _originalPosition,
                elapsedTime / bounceSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = _originalPosition;
    }

    IEnumerator DelayChangeBodyType(float delay)
    {
        yield return new WaitForSeconds(delay);
        rb.bodyType = RigidbodyType2D.Dynamic;
        col.isTrigger = true;
    }
}