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
    public Transform resourceCounter;

    public Transform resourceStore;

    private bool hovering = false;

    public bool hasResource;
    public bool isItem;
    public bool isPower;

    void Awake()
    {
        holder = this.transform.parent.gameObject.GetComponent<DragHolder>();

        if (hasResource)
        {
            resourceStore = resourceCounter.parent;
        }
    }

    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            held = true;

            holder.slots[location].GetComponent<BoxCollider2D>().size = new Vector2(.3f, .3f);
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

                //this.transform.position = Vector2.MoveTowards(this.transform.position, destination, 1f);

                //this.transform.position = new Vector2(destination.x, destination.y);
                this.transform.position = new Vector3(destination.x, destination.y, this.transform.position.z);
                //this.transform.position = destination;

                //Debug.Log(destination);

                //this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(destination.x, destination.y, 0f), 1f);
                //this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0f);

            }
        }
        else if (held == true)
        {
            held = false;
            this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 5;
            
            if (this.location == this.priorLocation)
            {
                for (int a = 0; a < holder.selectableItems; a++)
                {
                    bool free = true;

                    for (int i = 0; i < holder.draggables.Length; i++)
                    {
                        if (holder.draggables[i].GetComponent<CanBeDragged>().location == a)
                        {
                            free = false;
                        }
                    }

                    if (free == true)
                    {
                        location = a;
                        a = holder.selectableItems;
                    }
                }

                /*int i = 0;
                int lowest = 5;

                for (int a = 0; a < holder.draggables.Length; a++)
                {
                    if (holder.draggables[a].GetComponent<CanBeDragged>().location < holder.selectableItems - 1)
                    {
                        if (holder.draggables[a].GetComponent<CanBeDragged>().location < lowest)
                        {
                            lowest = holder.draggables[a].GetComponent<CanBeDragged>().location;
                        }
                        i++;
                    }


                }
                if (i > 0 && i < holder.selectableItems + 1)
                {
                    location = lowest;
                }*/
            }

            if (this.location != this.priorLocation)
            {

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
            }


            priorLocation = location;
        }

        if (held == false)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(holder.slots[location].transform.position.x, holder.slots[location].transform.position.y, holder.slots[location].transform.position.z), 1f);
            holder.slots[location].GetComponent<BoxCollider2D>().size = new Vector2(.1f, .1f);

            SetResource();
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

    public void SetResource()
    {
        if (hasResource)
        {
            if (location < 3)
            {
                if (isItem)
                {
                    resourceCounter.SetParent(resourceStore.GetChild(3));
                    resourceCounter.localPosition = new Vector3(0, 0, 0);
                    resourceCounter.gameObject.SetActive(true);
                }
                else if (isPower)
                {
                    resourceCounter.gameObject.SetActive(true);
                }
                else
                {
                    resourceCounter.SetParent(resourceStore.GetChild(location));
                    resourceCounter.localPosition = new Vector3(0, 0, 0);
                    resourceCounter.gameObject.SetActive(true);
                }
            }
            else
            {
                resourceCounter.SetParent(resourceStore);
                resourceCounter.localPosition = new Vector3(0, 0, 0);
                resourceCounter.gameObject.SetActive(false);
            }
        }
    }

}
