using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanBeDragged : MonoBehaviour
{
    public DragHolder holder;

    public bool held = false;
    public int priorLocation = 0;
    public int location = 0;

    public int number;

    public Transform target;

    private bool hovering = false;

    void Awake()
    {
        holder = this.transform.parent.gameObject.GetComponent<DragHolder>();
    }

    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            held = true;

            holder.slots[location].GetComponent<BoxCollider2D>().size = new Vector2(1f, 1f);
        }

    }

    void OnMouseOver()
    {
        hovering = true;
    }

    void OnMouseExit()
    {
        hovering = false;
    }

    void Update()
    {
        if (hovering == true)
        {
            if (Input.GetMouseButtonDown(1))
            {
                for (int i = 0; i < holder.descriptionBox.transform.childCount; i++)
                {
                    if (i == number)
                    {
                        holder.descriptionBox.transform.GetChild(i).gameObject.SetActive(true);
                    }
                    else
                    {
                        holder.descriptionBox.transform.GetChild(i).gameObject.SetActive(false);
                    }
                }
            }
        }


        if (Input.GetMouseButton(0))
        {
            if (held == true)
            {
                this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 10;

                Vector3 mousePos = Input.mousePosition;
                mousePos.z = Camera.main.nearClipPlane;
                Vector2 destination = Camera.main.ScreenToWorldPoint(mousePos);

                Vector2 currentPos = this.transform.position;

                this.transform.position = Vector2.MoveTowards(currentPos, destination, 1f);
            }
        }
        else if (held == true)
        {
            held = false;
            this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 5;


            for (int a = 0; a < holder.draggables.Length; a++)
            {
                if (holder.draggables[a].GetComponent<CanBeDragged>().location == location)
                {
                    if (holder.draggables[a].transform.position != this.transform.position) //this could break on an exact drop
                    {
                        holder.draggables[a].GetComponent<CanBeDragged>().location = priorLocation;

                        holder.draggables[a].GetComponent<CanBeDragged>().priorLocation = holder.draggables[a].GetComponent<CanBeDragged>().location;

                    }
                }
            }
            priorLocation = location;
        }

        if (held == false)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, holder.slots[location].transform.position, 1f);
            holder.slots[location].GetComponent<BoxCollider2D>().size = new Vector2(.1f, .1f);
        }
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "DragSlot")
        {
            for (int i = 0; i < holder.slots.Length; i++)
            {
                if (holder.slots[i].transform.position == col.transform.position)
                {
                    location = i;
                }
            } //make slot that you're going to drop it into light up?
        }
    }

}
