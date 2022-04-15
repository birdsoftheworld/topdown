using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamIntermid : MonoBehaviour
{
    public GameObject playerTransform;
    public GameObject cameraTransform;
    public float maxDistance;
    public float speed;

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0;
        //gameObject.transform.position = mouse;

        Vector3 destination = new Vector3((playerTransform.transform.position.x + mouse.x) / 2, (playerTransform.transform.position.y + mouse.y) / 2, 0f);
        //Vector3 destination = new Vector3((playerTransform.transform.position.x + cameraTransform.transform.position.x) / 2, (playerTransform.transform.position.y + cameraTransform.transform.position.y) / 2, 0f);


       /* Debug.Log(mouse.x + " " + mouse.y);

        Debug.Log(destination.x + " " + destination.y);
        Debug.Log(playerTransform.transform.position.x + " " + maxDistance);
        Debug.Log(" ");*/

        if (destination.x > playerTransform.transform.position.x + maxDistance)
        {
            destination = new Vector3(playerTransform.transform.position.x + maxDistance, destination.y, 0f);
        }
        else if (destination.x < playerTransform.transform.position.x - maxDistance)
        {
            destination = new Vector3(playerTransform.transform.position.x - maxDistance, destination.y, 0f);
        }

        if (destination.y > playerTransform.transform.position.y + maxDistance)
        {
            destination = new Vector3(destination.x, playerTransform.transform.position.y + maxDistance, 0f);
        }
        else if (destination.y < playerTransform.transform.position.y - maxDistance)
        {
            destination = new Vector3(destination.x, playerTransform.transform.position.y - maxDistance, 0f);
        }
        //Debug.Log(destination.x + " " + destination.y);

        this.transform.position = Vector2.MoveTowards(this.transform.position, destination, speed * Time.deltaTime);
    }
}
