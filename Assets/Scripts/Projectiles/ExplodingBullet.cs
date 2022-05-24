using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingBullet : MonoBehaviour
{
    public GameObject explosionPrefab;
    public int damage;
    public int size;
    public int push;

    void OnDestroy()
    {
        //Debug.Log("OnDestroy1");

        GameObject clone = Instantiate(explosionPrefab, this.transform.position, this.transform.rotation);
        clone.gameObject.SetActive(true);

        Explosion explosion = clone.gameObject.GetComponent("Explosion") as Explosion;

        explosion.damage = damage;
        explosion.startSpeed = push;
        explosion.explosionSize = size;

        clone.gameObject.SetActive(true);
    }
}
