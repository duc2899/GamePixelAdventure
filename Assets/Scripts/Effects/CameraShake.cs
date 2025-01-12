using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraShake : MonoBehaviour
{
    public float duration = 0.5f;
    public float magnitude = 0.1f;

    private Vector3 _originalPosition;

    private void Start()
    {
        _originalPosition = transform.localPosition;
    }

    public void Shake()
    {
        StartCoroutine(ShakeCoroutine());
    }

    private IEnumerator ShakeCoroutine()
    {
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float offsetX = Random.Range(-1f, 1f) * magnitude;
            float offsetY = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(_originalPosition.x + offsetX, _originalPosition.y + offsetY,
                _originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }


        transform.localPosition = _originalPosition;
    }
}