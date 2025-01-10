using UnityEngine;

public class FanController : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;

    private Rigidbody2D _rbPlayer;

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 10f, layerMask);

        if (hit.collider != null)
        {
            if (_rbPlayer == null)
            {
                _rbPlayer = hit.collider.gameObject.GetComponentInParent<Rigidbody2D>();
            }

            if (_rbPlayer != null)
            {
                _rbPlayer.AddForce(Vector2.up * Random.Range(0.2f, 0.3f), ForceMode2D.Impulse);
            }
        }
    }
}