using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Tooltip("Base movement speed of the player")]
    public float moveSpeed = 0.25f;

    private Vector2 inputDirection = Vector2.zero;
    private Rigidbody2D body;


    private Rigidbody2D rb2D;
    private Vector2 destination;


    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        float verticalMove = Input.GetAxisRaw("Vertical");

        inputDirection = new Vector2(horizontalMove, verticalMove);
        if(inputDirection.magnitude > 1)
        {
            inputDirection = inputDirection.normalized;
        }



        ////////////
        rb2D = GetComponent<Rigidbody2D>();


        //destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        destination = Camera.main.ScreenToWorldPoint(mousePos);

        Vector2 currentPos = this.transform.position;

        destination = destination - currentPos;

        Vector3 destinationN = destination.normalized;

        float angle = Mathf.Atan2(destinationN.y, destinationN.x) * Mathf.Rad2Deg;

        Quaternion rotation = new Quaternion();
        rotation.eulerAngles = new Vector3(0, 0, angle + 90);

        transform.rotation = rotation;



    }

    private void FixedUpdate()
    {
        //body.position = body.position + (inputDirection * moveSpeed);

        body.velocity = body.velocity/3;
        //body.velocity = Vector3.zero;
        body.AddForce(inputDirection * moveSpeed * 100f);

    }
}
