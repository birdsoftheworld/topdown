using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Tooltip("Base movement speed of the player")]
    public float moveSpeed = 1.0f;

    private Vector2 inputDirection = Vector2.zero;
    private Rigidbody2D body;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        float verticalMove = Input.GetAxisRaw("Vertical");

        inputDirection = new Vector2(horizontalMove, verticalMove).normalized;
    }

    private void FixedUpdate()
    {
        body.MovePosition(inputDirection * moveSpeed);
    }
}
