using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamIntermid : MonoBehaviour
{
    public GameObject playerTransform;
    public GameObject cameraTransform;
    public float maxDistance;
    public float minDistance;
    public float speed;

    public Vector3 oldDestination;

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 destination = new Vector3(playerTransform.transform.position.x, playerTransform.transform.position.y, 0f);
        float funcSpeed = speed;

        if (Vector2.Distance(this.transform.position, playerTransform.transform.position) > 10)
        {
            this.transform.position = playerTransform.transform.position;
        }

        //Quaternion rotation = playerTransform.transform.rotation;

        //transform.rotation = rotation;

        if (Input.GetMouseButton(2))
        {
            if (Vector2.Distance(this.transform.position, playerTransform.transform.position) < .1)
            {
                this.transform.position = playerTransform.transform.position;
            }
        }
        else
        {
            Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            mouse.z = 0;

            destination = new Vector3(mouse.x, mouse.y, 0f);


            float hypo = Mathf.Pow(Mathf.Pow(mouse.x - playerTransform.transform.position.x, 2f) + Mathf.Pow(mouse.y - playerTransform.transform.position.y, 2f), .5f);


            //Debug.Log(hypo);

            if (hypo > maxDistance)
            {
                destination = new Vector3((maxDistance * Mathf.Acos(1 * (playerTransform.transform.position.x - mouse.x) / hypo)) - (maxDistance * 1.5f), maxDistance * (Mathf.Asin(-1 * (playerTransform.transform.position.y - mouse.y) / hypo)), 0f);
                funcSpeed = maxDistance * 5;
            }
            else
            {
                destination = new Vector3((hypo * Mathf.Acos(1 * (playerTransform.transform.position.x - mouse.x) / hypo)) - (hypo * 1.5f), hypo * (Mathf.Asin(-1 * (playerTransform.transform.position.y - mouse.y) / hypo)), 0f);


                funcSpeed = hypo * 5;
            }

            destination = new Vector2(destination.x + playerTransform.transform.position.x, destination.y + playerTransform.transform.position.y);
        }

        Vector3 playerSpeed = playerTransform.GetComponent<Rigidbody2D>().velocity;

        this.gameObject.GetComponent<Rigidbody2D>().velocity = playerSpeed;

        float speedBoost = (Mathf.Pow(Mathf.Abs(playerSpeed.x) + Mathf.Abs(playerSpeed.y), .25f));

        if (speedBoost < 1)
        {
            speedBoost = 1;
        }

        //Debug.Log(speedBoost);

        this.transform.position = Vector2.MoveTowards(this.transform.position, destination, funcSpeed * Time.deltaTime * speedBoost * speed);
    }
}
