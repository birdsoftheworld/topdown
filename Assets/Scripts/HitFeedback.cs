using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitFeedback : MonoBehaviour
{
    private HealthTest health;
    private SpriteRenderer render;
    private ParticleSystem particles;

    // Start is called before the first frame update
    void Awake()
    {
        health = this.gameObject.GetComponent<HealthTest>();
        render = this.gameObject.GetComponent<SpriteRenderer>();

        for (int i = 0; i < this.transform.childCount; i++)
        {
            if (this.transform.GetChild(i).gameObject.GetComponent<ParticleSystem>() != null)
            {
                particles = this.transform.GetChild(i).gameObject.GetComponent<ParticleSystem>();
                i = 100;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (health.iFrames == health.iFrameMax)
        {
            particles.Play();
        }
    }
}
