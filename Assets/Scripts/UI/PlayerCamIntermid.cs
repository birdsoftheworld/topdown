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
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0;

        Vector3 destination = new Vector3(mouse.x, mouse.y, 0f);

        Debug.Log(destination);


        if (Mathf.Pow(Mathf.Pow(mouse.x - playerTransform.transform.position.x, 2f) + Mathf.Pow(mouse.y - playerTransform.transform.position.y, 2f), .5f) > maxDistance)
        {
            destination = new Vector3(Mathf.Acos(playerTransform.transform.position.x - mouse.x), Mathf.Asin(playerTransform.transform.position.y - mouse.y), 0f);
        }
        else
        {
            this.transform.position = destination;
        }



        /*



        Vector3 mouse = Camera.main.WorldToScreenPoint(Input.mousePosition);
        mouse.z = 0;

        Vector3 player = Camera.main.WorldToScreenPoint(playerTransform.transform.position);
        player.z = 0;

        //Vector3 destination = new Vector3((mouse.x + player.x) / 2, (mouse.y + player.y) / 2, 0f);

        //Debug.Log(Vector2.PixelsToPoints(new Vector2((mouse.x + player.x) / 2, (mouse.y + player.y) / 2)));

        //Vector2 change = Camera.main.ScreenToWorldPoint(new Vector2((mouse.x - player.x), (mouse.y - player.y)));
        //Debug.Log(Camera.main.ScreenToWorldPoint(change));

        //Debug.Log(Camera.main.ScreenToWorldPoint(Camera.main.WorldToScreenPoint(playerTransform.transform.position)));

        //Vector2 current = Camera.main.WorldToScreenPoint(playerTransform.transform.position);
        //Debug.Log(Camera.main.ScreenToWorldPoint(current));

        //Vector2 altered = Camera.main.ScreenToWorldPoint(new Vector2(change.x + (current.x / 2), change.y + (current.y / 2)));

        //Debug.Log(Camera.main.ScreenToWorldPoint(altered));

        //Debug.Log(Camera.main.ScreenToWorldPoint(change));


        Debug.Log("");

        //this.transform.position = destination;


        //gameObject.transform.position = mouse;

        //Vector3 destination = new Vector3((playerTransform.transform.position.x + mouse.x) / 2, (playerTransform.transform.position.y + mouse.y) / 2, 0f);
        //Vector3 destination = new Vector3((playerTransform.transform.position.x + cameraTransform.transform.position.x) / 2, (playerTransform.transform.position.y + cameraTransform.transform.position.y) / 2, 0f);


        //float dx = Mathf.Acos(playerTransform.transform.position.x - mouse.x);


        //float distance = Mathf.Pow(Mathf.Pow(playerTransform.transform.position.x + mouse.x, 2) + Mathf.Pow(playerTransform.transform.position.y + mouse.y, 2), 0.5f);

        //Debug.Log(distance);

        //float angle = Mathf.Atan((playerTransform.transform.position.y + mouse.y) / (playerTransform.transform.position.x + mouse.x));


        //Vector3 destination = new Vector3(Mathf.Acos(playerTransform.transform.position.x - mouse.x), Mathf.Asin(playerTransform.transform.position.y - mouse.y), 0f);
        //this.transform.position = destination;

        //Vector3 destination = new Vector3()

        //this.transform.position = destination;







        /* Debug.Log(mouse.x + " " + mouse.y);

         Debug.Log(destination.x + " " + destination.y);
         Debug.Log(playerTransform.transform.position.x + " " + maxDistance);
         Debug.Log(" ");*/

        /*if (destination.x > playerTransform.transform.position.x + maxDistance)
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

        /*if (destination.x > playerTransform.transform.position.x - minDistance && destination.x < playerTransform.transform.position.x + minDistance)
        {
            destination = new Vector3(playerTransform.transform.position.x, destination.y, 0f);
        }

        if (destination.y > playerTransform.transform.position.y - minDistance && destination.y < playerTransform.transform.position.y + minDistance)
        {
            destination = new Vector3(destination.x, playerTransform.transform.position.y, 0f);
        }*/







        /*if (this.transform.position.x > playerTransform.transform.position.x + maxDistance)
        {
            this.transform.position.x = new Vector3(destination.x, this.transform.position.y, 0f);
        }
        if (this.transform.position.x < playerTransform.transform.position.x - maxDistance)
        {
            this.transform.position.x = new Vector3(destination.x, this.transform.position.y, 0f);
        }*/

        //destination = new Vector3(Mathf.Round(destination.x * 10.0f) * 0.1f, Mathf.Round(destination.y * 10.0f) * 0.1f, 0f);

        //destination = new Vector3((Mathf.Round(destination.x * 10.0f) * 0.1f) - Mathf.Abs(Mathf.Round(this.transform.position.x) - this.transform.position.x), (Mathf.Round(destination.y * 10.0f) * 0.1f) - Mathf.Abs(Mathf.Round(this.transform.position.y) - this.transform.position.y), 0f);

        //destination = new Vector3(destination.x  - Mathf.Abs(Mathf.Round(this.transform.position.x) - this.transform.position.x), destination.y - Mathf.Abs(Mathf.Round(this.transform.position.y) - this.transform.position.y), 0f);


        //Debug.Log(destination.x + " " + destination.y);

        /*if (oldDestination != null)
        {
            if (Mathf.Abs(oldDestination.x - destination.x) < .4)
            {
                if (Mathf.Abs(oldDestination.y - destination.y) < .4)
                {
                    destination = oldDestination;
                }
            }
        }

        if (Mathf.Abs(destination.x - this.transform.position.x) > .4)
        {
            if (Mathf.Abs(destination.y - this.transform.position.y) > .4)
            {
                this.transform.position = Vector2.MoveTowards(this.transform.position, destination, speed * Time.deltaTime);

            }
        }*/

        /*
        if (oldDestination != null)
        {
            if (Mathf.Abs(oldDestination.x - destination.x) > .2)
            {
                destination = new Vector3((oldDestination.x + destination.x) / 2, destination.y, 0f);
            }
            else if (Mathf.Abs(oldDestination.x - destination.x) < .05)
            {
                destination = new Vector3((oldDestination.x + destination.x) / 2, destination.y, 0f);
            }

            if (Mathf.Abs(oldDestination.y - destination.y) > .2)
            {
                destination = new Vector3(destination.x, (oldDestination.y + destination.y) / 2, 0f);
            }
            else if (Mathf.Abs(oldDestination.y - destination.y) < .05)
            {
                destination = new Vector3(destination.x, (oldDestination.y + destination.y) / 2, 0f);
            }
        }


        this.transform.position = Vector2.MoveTowards(this.transform.position, destination, speed * Time.deltaTime);


        //this.transform.position = destination;

        oldDestination = destination;*/



    }
}
