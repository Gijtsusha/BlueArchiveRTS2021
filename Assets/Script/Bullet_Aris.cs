using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Aris : Bullet
{
    public float damage;

    float timer;
    protected override void Start()
    {
        tag = GetComponentInParent<BulletManager>().tag;
        Debug.Log(tag);
        Destroy(this.gameObject, 1f);
        timer = 0;
    }




    
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit"+other.gameObject.name);
        timer = 0;
        deltaDamage(other);
    }
    void OnTriggerStay(Collider other)
    {
        Debug.Log("StayHit" + other.gameObject.name);

        if (timer > 0.2f)
        {
            deltaDamage(other);
        }

        timer += Time.deltaTime;
    }

    void deltaDamage(Collider other)
    {
        if (!other.CompareTag(tag))
        {
            if (other.GetComponent<Health>())
            {
                if (other.GetComponent<Build>() && other.GetComponent<Build>().isAlive)
                {
                    other.GetComponent<Health>().BeAttacked(damage, 3);
                }

                if (other.GetComponent<Actor>() && other.GetComponent<Actor>().isAlive)
                {
                    other.GetComponent<Health>().BeAttacked(damage, 3);
                }
            }
        }
    }
}
