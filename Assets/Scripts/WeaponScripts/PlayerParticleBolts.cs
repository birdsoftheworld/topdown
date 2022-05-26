using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerParticleBolts : MonoBehaviour
{


    ParticleSystem ps;

    // these lists are used to contain the particles which match
    // the trigger conditions each frame.
    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
    List<ParticleSystem.Particle> exit = new List<ParticleSystem.Particle>();

    public GameObject damagerPrefab;


    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    private void OnParticleTrigger()
    {

        // get the particles which matched the trigger conditions this frame
        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        int numExit = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);

        // iterate through the particles which entered the trigger and make them red
        for (int i = 0; i < numEnter; i++)
        {
            ParticleSystem.Particle p = enter[i];


            Vector2 position = p.position;



            GameObject clone = Instantiate(damagerPrefab, position, this.transform.parent.rotation);
            clone.gameObject.SetActive(true);

            AutoDamage damager = clone.gameObject.GetComponent("AutoDamage") as AutoDamage;

            damager.bulletDamage = 1;
            //Debug.Log(clone.transform.position);

            p.startColor = new Color32(255, 0, 0, 255);
            enter[i] = p;
        }

        // iterate through the particles which exited the trigger and make them green
        for (int i = 0; i < numExit; i++)
        {
            ParticleSystem.Particle p = exit[i];



            p.startColor = new Color32(0, 255, 0, 255);
            exit[i] = p;
        }

        // re-assign the modified particles back into the particle system
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);


        /*
        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        int numExit = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);

        Debug.Log(numEnter);

        //Debug.Log("piunk");
        for (int i = 0; i < numEnter; i++)
        {

            Vector2 position = enter[i].position;
            GameObject clone = Instantiate(bulletPrefab, position, this.transform.rotation);
            clone.gameObject.SetActive(true);

            Projectile bullet = clone.gameObject.GetComponent("Projectile") as Projectile;

            bullet.bulletSpeed = 0;
            bullet.bulletFaction = bulletFaction;
            bullet.bulletDamage = 1;

            Debug.Log(i);
        }
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);

    

            /*if (ps.trigger.GetCollider(i) != null)
            {
                //Debug.Log("collider");
                Component component = ps.trigger.GetCollider(i);

                if (component.GetComponent<Hittable>() != null)
                {
                    //Debug.Log("hittable");

                    Hittable hitted = component.GetComponent<Hittable>();
                    Faction hitFact = hitted.faction;

                    Debug.Log(component.gameObject);

                    if (component.GetComponent<Collider2D>().isTrigger == true)
                    {

                    }
                    else if (hitted != null)
                    {
                        if (hitted.CanHit(0) == true)
                        {
                            //Debug.Log("damageable");

                            //Debug.Log(hitFact);

                            HealthTest health = component.GetComponent<HealthTest>();
                            //Debug.Log(health);


                            if (health != null)
                            {

                                Vector2 position = bulletOrigin.position;
                                GameObject clone = Instantiate(bulletPrefab, position, bulletOrigin.rotation);
                                clone.gameObject.SetActive(true);

                                Projectile bullet = clone.gameObject.GetComponent("Projectile") as Projectile;

                                bullet.bulletSpeed = bulletSpeed;
                                bullet.bulletFaction = bulletFaction;
                                bullet.bulletDamage = bulletDamage;


                                /*Debug.Log("damaged");

                                health.curHealth--;

                                health.DealDamage(1, this.transform.parent.position);*/
        /* }
     }
 }

}*/
        //}
        /*else
        {
            i = 10;
        }*/

    }

}