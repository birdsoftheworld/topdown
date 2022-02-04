using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootController : MonoBehaviour
{
    public GameObject lootPrefab;

    // Update is called once per frame
    public void Drop(Vector2 position, int type, int amount, int quant)
    {
        for (int i = Random.Range(1, quant); i < quant; i++)
        {
            float spin = Random.Range(0f, 360f * Mathf.Deg2Rad);

            GameObject clone = Instantiate(lootPrefab, new Vector2(position.x + 0.6f * Mathf.Cos(spin), position.y + 0.6f * Mathf.Sin(spin)), this.transform.rotation);

            clone.transform.eulerAngles = new Vector3(clone.transform.eulerAngles.x, clone.transform.eulerAngles.y, Random.Range(0f, 360f));

            LootDrop loot = clone.gameObject.GetComponent("LootDrop") as LootDrop;

            loot.dropType = type;
            loot.amountGive = Random.Range(1, amount + 1);

            clone.gameObject.SetActive(true);

            loot.setSprite();
        }
    }
}