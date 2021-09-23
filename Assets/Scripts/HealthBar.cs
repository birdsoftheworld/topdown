using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    public HealthTest playerHealth;

 //   public GameObject canvas;

    private void Start()
    {
        //playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthTest>();
        healthBar = GetComponent<Slider>();
        healthBar.maxValue = playerHealth.maxHealth;
        healthBar.value = playerHealth.maxHealth;

        this.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
    }

    public void SetHealth(int hp)
    {
        healthBar.value = hp;
    }
}
