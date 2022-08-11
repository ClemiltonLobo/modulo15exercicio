using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public Rigidbody2D MyRigidbody2D;

    [Header("Speed Setup")]
    public Vector2 friction = new Vector2(.1f, 0);
    public float speedRun;
    public float speed;
    public float forceJump = 2;

    [Header("Animation Setup")]
    public float jumpScaleY = 0.7f;
    public float jumpScaleX = 1.5f;
    public float squatScaleX = 1.5f;
    public float squatScaleY = 0.7f;
    public float animatioDuration = .3f;
    public Ease ease = Ease.OutBack;

    private float _currentSpeed;

    public void Update()
    {
        HandleJump();
        HandleMoviment();
    }

    private void HandleMoviment()
    {
        if (Input.GetKey(KeyCode.LeftShift))

            _currentSpeed = speedRun;
        else
            _currentSpeed = speed;


        if (Input.GetKey(KeyCode.LeftArrow))
        {
            MyRigidbody2D.velocity = new Vector2(-_currentSpeed, MyRigidbody2D.velocity.y);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            MyRigidbody2D.velocity = new Vector2(_currentSpeed, MyRigidbody2D.velocity.y);
        }


        if (MyRigidbody2D.velocity.x > 0)
        {
            MyRigidbody2D.velocity += friction;
        }
        else if (MyRigidbody2D.velocity.x < 0)
        {
            MyRigidbody2D.velocity -= friction;
        }
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MyRigidbody2D.velocity = Vector2.up * forceJump;
            MyRigidbody2D.transform.localScale = Vector2.one;

            DOTween.Kill(MyRigidbody2D.transform);

            HandleScaleJump();
            HandleScaleSquat();
        }
    }

    private void HandleScaleJump()
    {
        MyRigidbody2D.transform.DOScaleY(jumpScaleY, animatioDuration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
        MyRigidbody2D.transform.DOScaleX(jumpScaleX, animatioDuration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
    }

    private void HandleScaleSquat()
    {
        MyRigidbody2D.transform.DOScaleY(squatScaleY, animatioDuration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
        MyRigidbody2D.transform.DOScaleX(squatScaleX, animatioDuration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
    }
}
