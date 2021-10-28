using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;

    void Update()
    {
        this.transform.position = new Vector3(player.position.x, player.position.y, -10);

        /*Vector3 p = Input.mousePosition; //fully functioning, just feels janky
        p.z = 20;
        Vector3 pos = Camera.main.ScreenToWorldPoint(p);


        float vert = (pos.y - player.position.y);
        float horiz = (pos.x - player.position.x);

        if (Mathf.Abs(vert) > 3)
        {
            vert = 3 * (vert / Mathf.Abs(vert));
        }
        if (Mathf.Abs(horiz) > 3)
        {
            horiz = 3 * (horiz / Mathf.Abs(horiz));
        }

        horiz = horiz / 2;
        vert = vert / 2;

        if (Vector3.Distance(new Vector3(this.transform.position.x + horiz, this.transform.position.y + vert, -10), new Vector3(player.transform.position.x, player.transform.position.y, -10)) > 4.9)
        {
            if (Vector2.Distance(this.transform.position, player.transform.position) > 3)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(player.transform.position.x, player.transform.position.y, -10), 10 * Time.deltaTime);
            }
        }
        else if (Vector2.Distance(this.transform.position, player.transform.position) < 3)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(this.transform.position.x + horiz, this.transform.position.y + vert, -10), 10 * Time.deltaTime);
        }
        else
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(player.transform.position.x, player.transform.position.y, -10), 10 * Time.deltaTime);
        }*/
    }
}