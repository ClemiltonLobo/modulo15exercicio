using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public Rigidbody2D playerBody;
    public Transform footPosition;
    public LayerMask floor;

    [Header("Speed Setup")]
    public Vector2 friction = new Vector2(.1f, 0);
    public float speedRun;
    public float speedPlayer;
    public float jumpForce = 2;

    [Header("Animation Setup")]
    public float jumpScaleY = 0.7f;
    public float jumpScaleX = 1.5f;
    public float animatioDuration = .3f;
    public Ease ease = Ease.OutBack;
    public Animator animator;

    private float _currentSpeed;
    private float direction;
    private bool feetOnTheGround;
    private bool lookRight = true;

    private void OnValidate()
    {
        if (animator == null) animator = GetComponent<Animator>();
        if (playerBody == null) playerBody = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        HandleJump();
        HandleMoviment();
        GiveABoost();
    }

    private void HandleMoviment()
    {
        feetOnTheGround = Physics2D.OverlapCircle(footPosition.position, 0.3f, floor);
        animator.SetBool("feetOnTheGround", feetOnTheGround);
        animator.SetFloat("SpeedY", playerBody.velocity.y);

        direction = Input.GetAxisRaw("Horizontal");

        playerBody.velocity = new Vector2(direction * speedPlayer, playerBody.velocity.y);
        animator.SetFloat("Speed", Mathf.Abs(direction));

        if ((direction < 0 && lookRight) || (direction > 0 && !lookRight))
        {
            lookRight = !lookRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }

    private void HandleJump()
    {
        if (feetOnTheGround && Input.GetKeyDown(KeyCode.Space))
        {
            playerBody.velocity = Vector2.up * jumpForce;
            playerBody.transform.localScale = Vector2.one;

            DOTween.Kill(playerBody.transform);

            HandleScaleJump();
        }
    }

    private void HandleScaleJump()
    {
        playerBody.transform.DOScaleY(jumpScaleY, animatioDuration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
        playerBody.transform.DOScaleX(jumpScaleX, animatioDuration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
    }

    private void GiveABoost()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            _currentSpeed = speedRun;
            animator.speed = 2;
        }
        else
        {
            _currentSpeed = speedPlayer;
            animator.speed = 1;
        }
    }
}
