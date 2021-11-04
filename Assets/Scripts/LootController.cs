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
            GameObject clone = Instantiate(lootPrefab, position, this.transform.rotation);
            clone.gameObject.SetActive(true);

            LootDrop loot = clone.gameObject.GetComponent("LootDrop") as LootDrop;

            loot.dropType = type;
            loot.amountGive = Random.Range(1, amount + 1);
        }
    }
}
