using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GamePlay
{
    public class FallingPlatformController : MonoBehaviour
    {
        private const float ShakeAmount = 0.3f;
        private const float ShakeSpeed = 3f;
        private const float BounceAmount = 0.2f;
        private const float BounceSpeed = 0.1f;
        private const float TimeDelayChangeBodyType = 1.5f;

        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private Collider2D collider2DFalling;

        private Vector3 _originalPosition;
        private bool _isTouchingFallingPlatform;
        private bool _hasBounced;

        private void Start()
        {
            _originalPosition = transform.position;
        }


        private void Update()
        {
            var shake = Mathf.Sin(Time.time * ShakeSpeed) * ShakeAmount;
            transform.position = new Vector3(_originalPosition.x + shake, transform.position.y, transform.position.z);
        }

        private void OnCollisionEnter2D()
        {
            if (_hasBounced) return;

            _isTouchingFallingPlatform = true;

            if (!_isTouchingFallingPlatform) return;

            _hasBounced = true;
            StartCoroutine(BounceEffect());
            StartCoroutine(DelayChangeBodyType(TimeDelayChangeBodyType));
        }

        private void OnCollisionExit2D()
        {
            _isTouchingFallingPlatform = false;
        }

        private IEnumerator BounceEffect()
        {
            var elapsedTime = 0f;
            var currentPosition = transform.position;
            var downPosition = currentPosition - Vector3.up * BounceAmount;

            while (elapsedTime < BounceSpeed)
            {
                transform.position = Vector3.Lerp(currentPosition, downPosition, elapsedTime / BounceSpeed);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.position = downPosition;

            elapsedTime = 0f;

            while (elapsedTime < BounceSpeed)
            {
                transform.position = Vector3.Lerp(downPosition, currentPosition, elapsedTime / BounceSpeed);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.position = currentPosition;
        }

        private IEnumerator DelayChangeBodyType(float delay)
        {
            yield return new WaitForSeconds(delay);

            rb.bodyType = RigidbodyType2D.Dynamic;
            collider2DFalling.isTrigger = true;
        }
    }
}