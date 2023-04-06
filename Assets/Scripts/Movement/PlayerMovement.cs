using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerMovement : MonoBehaviour
{

    private new Rigidbody2D rigidbody;
    private Vector2 movementForce;
    private Vector2 playerInput;
    [SerializeField] private bool canMove = true;
    [SerializeField] private float speed = 0.25f;
    private Direction direction = Direction.Left;

    private enum Direction
    {
        Right,
        Left
    }

    private void Start()
    {
        rigidbody = this.GetComponent<Rigidbody2D>();

    }
    private void Update()
    {
        Physics2D.gravity = Vector2.zero;
        rigidbody.velocity = movementForce;

        switch (direction)
        {
            case Direction.Left:
                if (playerInput.x > 0)
                {
                    direction = Direction.Right;
                }

                break;
            case Direction.Right:
                if (playerInput.x < 0)
                {
                    direction = Direction.Left;
                }

                break;
        }

    }

    private void OnEnable()
    {
        Controller.OnMove += Move;
    }

    private void OnDisable()
    {
        Controller.OnMove -= Move;
    }

    void Move(Vector2 input)
    {
        if(!canMove)
        {
            input = Vector2.zero;
        }
        playerInput= input;
        input *= speed;
        movementForce = new Vector2(input.x, input.y);

    }


}
