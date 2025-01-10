using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private ParticleSystem smokeEffectRun;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private LayerMask layer;

    private Vector2 _movement;
    private bool _isTouchingGround = true;
    private bool _isDoubleJump = false;
    private bool _isCling = false;
    private float _raycastDistance = 1.5f;

    private void Awake()
    {
        smokeEffectRun.Stop();
    }

    // Update is called once per frame
    private void Update()
    {
        CheckGrounded();
        Jumping();
        ClingWall();
        WallJump();
        ShowSmokeEffectRun();
        CheckJumping();
    }

    private void FixedUpdate()
    {
        Moving();
        rb.linearVelocity = _movement;
        animator.SetFloat("MoveX", rb.linearVelocity.x);
        animator.SetFloat("MoveY", rb.linearVelocity.y);
    }

    private void Moving()
    {
        _movement = Vector2.zero;
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        _movement.x = moveHorizontal * speed;
        _movement.y = rb.linearVelocity.y;

        if (moveHorizontal > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // Hướng phải
        }
        else if (moveHorizontal < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Hướng trái
        }
    }

    private void ShowSmokeEffectRun()
    {
        if (rb.linearVelocity.magnitude > 0.1f)
        {
            if (!smokeEffectRun.isPlaying && !_isTouchingGround)
                smokeEffectRun.Play();
        }
        else
        {
            if (smokeEffectRun.isPlaying)
                smokeEffectRun.Stop();
        }
    }

    private void Jumping()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isTouchingGround)
        {
            rb.linearVelocity = Vector2.up * jumpForce;
            _isTouchingGround = false;
            _isDoubleJump = false;

            animator.SetTrigger("Jump"); // Kích hoạt hoạt ảnh nhảy
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !_isDoubleJump && !_isTouchingGround)
        {
            rb.linearVelocity = Vector2.up * jumpForce;
            _isDoubleJump = true;

            animator.SetTrigger("DoubleJump"); // Kích hoạt hoạt ảnh double jump
        }
    }

    private void CheckJumping()
    {
        if (!_isTouchingGround && rb.linearVelocity.y > 0)
        {
            animator.SetBool("IsJumping", true);
            _isTouchingGround = false;
        }
        else if (!_isTouchingGround && rb.linearVelocity.y <= 0)
        {
            animator.SetBool("IsJumping", false);
        }
        else
        {
            animator.SetBool("IsJumping", false);
        }
    }

    private void ClingWall()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, 1f, layer);
        Debug.DrawRay(transform.position, Vector2.right * transform.localScale.x, Color.red);

        if (hit.collider != null && !_isTouchingGround)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -1f);
            rb.gravityScale = 0;
            _isCling = true;

            animator.SetBool("IsCling", true);
            animator.ResetTrigger("Jump");
            animator.ResetTrigger("DoubleJump");
        }
        else
        {
            rb.gravityScale = 3f;
            _isCling = false;
            animator.SetBool("IsCling", false);
        }
    }

    private void WallJump()
    {
        if (_isCling && Input.GetKeyDown(KeyCode.Space))
        {
            // Nhảy khỏi tường với vận tốc ngược lại hướng tường
            rb.linearVelocity = new Vector2(-transform.localScale.x * 5f, jumpForce);
            transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
            _isCling = false;
            animator.SetBool("IsCling", _isCling); // Thoát trạng thái bám
            animator.ResetTrigger("Jump");
        }
    }

    private void CheckGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, _raycastDistance, layer);

        // Kiểm tra xem raycast có chạm mặt đất không
        if (hit.collider != null)
        {
            _isTouchingGround = true;
            // animator.SetBool("IsJumping", false);
            animator.SetBool("IsDoubleJumping", false);
            animator.SetBool("IsCling", false);
            _isCling = false;
            rb.gravityScale = 3f;
        }
        else
        {
            _isTouchingGround = false;
        }
    }
}