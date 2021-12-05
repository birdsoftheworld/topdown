using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTexture : MonoBehaviour
{
    public int type;

    void Awake()
    {
        Sprite[] sprites;

        if (type == 0)
        {
            sprites = GameObject.FindGameObjectWithTag("LevelGenerator").gameObject.GetComponent<Level>().floorSprites;
        }
        else
        {
            sprites = GameObject.FindGameObjectWithTag("LevelGenerator").gameObject.GetComponent<Level>().wallSprites;
        }

        this.gameObject.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];

        //Vector3 pos = this.transform.position;

        //this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, this.transform.eulerAngles.y, 90 * Random.Range(0, 4));

        //this.transform.position = pos;

    }

    void Update()
    {
        this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, this.transform.eulerAngles.y, 90 * Random.Range(0, 4));

        this.enabled = false;
    }
}
