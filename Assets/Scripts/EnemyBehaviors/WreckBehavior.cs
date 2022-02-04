using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WreckBehavior : MonoBehaviour
{
    public List<Sprite> sprites = new List<Sprite>();

    public HealthTest health;

    void Update()
    {
        if (health.curHealth <= 0)
        {
            //levelGen.GetComponent<LootController>().Drop(this.transform.position, 0, 2, 2);
            Destroy(gameObject);
        }
    }

    public void setSprite(int num)
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = sprites[num];
    }
}
