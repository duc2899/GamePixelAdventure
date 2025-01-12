using System;
using System.Collections;
using UnityEngine;

public class BreakPiece : MonoBehaviour
{
    [SerializeField] private float blinkDuration = 2f;
    [SerializeField] private SpriteRenderer pieceRenderer;
    [SerializeField] private Rigidbody2D rb;

    public void Initialize(Vector2 force, float torque)
    {
        if (rb == null)
            rb = gameObject.AddComponent<Rigidbody2D>();

        rb.AddForce(force);
        rb.AddTorque(torque);

        StartCoroutine(BlinkAndDestroy());
    }

    private IEnumerator BlinkAndDestroy()
    {
        float elapsedTime = 0;
        bool isVisible = true;
        while (elapsedTime < blinkDuration)
        {
           
            isVisible = !isVisible;
            if (pieceRenderer != null)
                pieceRenderer.enabled = isVisible;
            elapsedTime += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }

        Destroy(gameObject);
    }
}