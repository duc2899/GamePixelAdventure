using System;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        private const float RaycastDistanceGround = 1.5f;
        private const float RaycastDistanceWall = 1f;
        private const float SpeedWallFall = 1f;
        private const float ClingGravityScale = 0f;
        private const float NormalGravityScale = 3f;

        private static readonly int MoveX = Animator.StringToHash("MoveX");
        private static readonly int MoveY = Animator.StringToHash("MoveY");
        private static readonly int Jump = Animator.StringToHash("Jump");
        private static readonly int DoubleJump = Animator.StringToHash("DoubleJump");
        private static readonly int IsJumping = Animator.StringToHash("IsJumping");
        private static readonly int IsCling = Animator.StringToHash("IsCling");
        private static readonly int IsDoubleJumping = Animator.StringToHash("IsDoubleJumping");


        [SerializeField] private float speed = 5f;
        [SerializeField] private float jumpForce = 10f;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private Animator animator;
        [SerializeField] private LayerMask layers;

        private bool _isTouchingGround = true;
        private bool _isDoubleJump, _isCling;

        private void Update()
        {
            CheckGrounded();
            Jumping();
            ClingWall();
            WallJump();
            CheckJumping();
        }

        private void FixedUpdate()
        {
            Moving();

            animator.SetFloat(MoveX, rb.linearVelocity.x);
            animator.SetFloat(MoveY, rb.linearVelocity.y);
        }

        private void Moving()
        {
            var moveHorizontal = Input.GetAxisRaw("Horizontal");

            var newVelocity = new Vector2(moveHorizontal * speed, rb.linearVelocity.y);

            FlipPlayer(moveHorizontal);

            rb.linearVelocity = newVelocity;
        }

        private void FlipPlayer(float moveHorizontal)
        {
            if (moveHorizontal != 0)
            {
                transform.localScale = new Vector3(Mathf.Sign(moveHorizontal), transform.localScale.y,
                    transform.localScale.z);
            }
        }

        private void Jumping()
        {
            if (!Input.GetKeyDown(KeyCode.Space)) return;

            if (_isTouchingGround)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                _isTouchingGround = false;
                _isDoubleJump = false;

                animator.SetTrigger(Jump);
            }
            else if (!_isDoubleJump && !_isTouchingGround)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                _isDoubleJump = true;

                animator.SetTrigger(DoubleJump);
            }
        }

        private void CheckJumping()
        {
            switch (_isTouchingGround)
            {
                case false when rb.linearVelocity.y > 0:
                    animator.SetBool(IsJumping, true);
                    _isTouchingGround = false;
                    break;
                case false when rb.linearVelocity.y <= 0:
                default:
                    animator.SetBool(IsJumping, false);
                    break;
            }
        }

        private void ClingWall()
        {
            var hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, RaycastDistanceWall,
                layers);

            if (hit.collider && !_isTouchingGround)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, -SpeedWallFall);
                rb.gravityScale = ClingGravityScale;
                _isCling = true;

                animator.SetBool(IsCling, true);
                animator.ResetTrigger(Jump);
                animator.ResetTrigger(DoubleJump);
            }
            else
            {
                rb.gravityScale = NormalGravityScale;
                _isCling = false;
                animator.SetBool(IsCling, false);
            }
        }

        private void WallJump()
        {
            if (!_isCling || !Input.GetKeyDown(KeyCode.Space)) return;

            rb.linearVelocity = new Vector2(-transform.localScale.x, jumpForce);
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            _isCling = false;

            animator.SetBool(IsCling, _isCling);
            animator.ResetTrigger(Jump);
        }

        private void CheckGrounded()
        {
            var hit = Physics2D.Raycast(transform.position, Vector2.down, RaycastDistanceGround, layers);

            if (hit.collider != null)
            {
                _isTouchingGround = true;
                rb.gravityScale = NormalGravityScale;
            }
            else
            {
                _isTouchingGround = false;
            }

            _isDoubleJump = false;
            _isCling = false;

            animator.SetBool(IsDoubleJumping, _isDoubleJump);
            animator.SetBool(IsCling, _isCling);
        }

        // private void ShowSmokeEffectRun()
        // {
        //     if (rb.linearVelocity.magnitude > 0.1f)
        //     {
        //         if (!smokeEffectRun.isPlaying && !_isTouchingGround)
        //             smokeEffectRun.Play();
        //     }
        //     else
        //     {
        //         if (smokeEffectRun.isPlaying)
        //             smokeEffectRun.Stop();
        //     }
        // }
    }
}