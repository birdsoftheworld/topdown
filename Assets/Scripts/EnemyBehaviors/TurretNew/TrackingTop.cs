using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingTop : MonoBehaviour
{
    private GameObject player;

    public string behavior;

    public GameObject turretBase;
    public GameObject sight;

    // Start is called before the first frame update
    void Start()
    {
        behavior = "search";

        player = GameObject.FindGameObjectWithTag("Player");
    }


    // Update is called once per frame
    void Update()
    {
        

    }

    void FixedUpdate()
    {
        if (behavior == "search")
        {
            this.transform.Rotate(0.0f, 0.0f, -1f, Space.World);

            if (checkSightToPlayer())
            {
                behavior = "target";
            }
        }

        if (behavior == "target")
        {
            float passTarg = turretBase.GetComponent<ShootingBottom>().convert(this.transform.localRotation.eulerAngles.z);

            turretBase.GetComponent<ShootingBottom>().target(passTarg);

            if (checkSightToPlayer() == false)
            {
                //Debug.Log(Vector2.Angle(this.transform.position, player.transform.position));
                //Debug.Log(this.transform.eulerAngles.z);
            }
        }
    }

    private bool checkSightToPlayer()
    {
        int layerMask = 1 << 0;
        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position, sight.transform.position - this.transform.position, Mathf.Infinity, layerMask);
        if (hit != null)
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}
