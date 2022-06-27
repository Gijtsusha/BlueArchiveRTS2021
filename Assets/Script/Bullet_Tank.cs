using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Tank : Bullet
{
    public float damage;
    public float damageArea;
    public LayerMask roleMask;



    void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit");
        if (!other.CompareTag(tag))
        {
            Collider[] damagedRole = Physics.OverlapSphere(transform.position, damageArea, roleMask);
            foreach (Collider role in damagedRole)
            {
                if (role.tag != tag && role.GetComponent<Health>())
                {
                    if (role.GetComponent<Build>() && role.GetComponent<Build>().isAlive)
                    {
                        role.GetComponent<Health>().BeAttacked(damage, 2);
                    }

                    if (role.GetComponent<Actor>() && role.GetComponent<Actor>().isAlive)
                    {
                        role.GetComponent<Health>().BeAttacked(damage, 2);
                    }
                }
            }
            Destroy(this.gameObject);
        }
    }
}
