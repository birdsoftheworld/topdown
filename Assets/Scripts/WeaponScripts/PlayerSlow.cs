using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlow : MonoBehaviour
{
    public Player player;

    public int recharge;

    public Sprite inactive;
    public Sprite active;

    public int pauseTimerMax;
    public int pauseTimer;
    public float pauseAmount;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        pauseTimer = 4040;

        Debug.Log(Time.fixedDeltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (player.waiting4 == 0)
            {
                this.transform.GetChild(0).gameObject.SetActive(true);

                player.waiting4 = recharge;
                Time.fixedDeltaTime = .02f * pauseAmount;
                Time.timeScale = pauseAmount;
                pauseTimer = pauseTimerMax;
                this.gameObject.GetComponent<SpriteRenderer>().sprite = active;
                player.moveSpeed = player.moveSpeed / (pauseAmount * .5f);
            }
            else if (pauseTimer != 4040 && pauseTimer > 0)
            {
                pauseTimer = 0;
                player.waiting4 = (player.waiting4 / 4) + (recharge / 2);
            }
        }
    }

    void FixedUpdate()
    {
        if (pauseTimer == 0)
        {
            this.transform.GetChild(0).gameObject.SetActive(false);

            this.gameObject.GetComponent<SpriteRenderer>().sprite = inactive;
            Time.timeScale = 1;
            Time.fixedDeltaTime = .02f;
            player.moveSpeed = player.moveSpeed * (pauseAmount * .5f);
            pauseTimer = 4040;
        }
        else if (pauseTimer != 4040)
        {
            pauseTimer--;
        }
    }
}
