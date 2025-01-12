using System.Collections.Generic;
using Managers;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private List<GameObject> fruitsPool;
    [SerializeField] private CameraShake cam;

    private int _numberHit = 0;
    private int _numberHitDestroy = 3;
    private bool _isHitPlaying = false;
    private Rigidbody2D _rbPlayer;
    private List<GameObject> _fruitsDeploy = new List<GameObject>();

    private void Awake()
    {
        cam = Camera.main.GetComponent<CameraShake>();
        if (fruitsPool.Count <= 0)
        {
            return;
        }

        for (int i = 0; i < 3; i++)
        {
            _fruitsDeploy.Add(fruitsPool[Random.Range(0, fruitsPool.Count)]);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Constants.TriggerPlayerTag))
        {
            if (!_isHitPlaying)
            {
                if (_rbPlayer == null)
                {
                    _rbPlayer = other.GetComponentInParent<Rigidbody2D>();
                }

                if (_rbPlayer != null)
                {
                    _rbPlayer.linearVelocity = new Vector2(_rbPlayer.linearVelocity.x, 10f);
                }

                _numberHit++;
                cam.Shake();
                animator.SetBool("isHit", true);
                _isHitPlaying = true;
                if (_numberHit == _numberHitDestroy)
                {
                    foreach (var fruit in _fruitsDeploy)
                    {
                        // Tạo đối tượng mới
                        GameObject f = Instantiate(fruit,
                            new Vector3(Random.Range(0, 2f), Random.Range(0, 2f), 0f) + transform.position,
                            transform.rotation);
                        Rigidbody2D fRB = f.GetComponent<Rigidbody2D>();
                        Collider2D fCollider = f.GetComponent<Collider2D>();
                        // Kiểm tra và thêm Rigidbody2D nếu cần
                        if (fCollider != null)
                        {
                            fCollider.isTrigger = false;
                        }

                        if (fRB != null)
                        {
                            fRB.gravityScale = 1;
                            fRB.linearVelocity = new Vector2(0, -5f);
                        }
                    }

                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(Constants.TriggerPlayerTag))
        {
            animator.SetBool("isHit", false);
            _isHitPlaying = false;
        }
    }
}