using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_GLM : Bullet
{
    public float damage;
    public float damageArea;
    public LayerMask roleMask;

    public GameObject damageRole;
    public float totalDis;
    public float nowDis;

    protected override void Start()
    { 
        tag = GetComponentInParent<BulletManager>().tag;
        Debug.Log(tag);
        totalDis = Vector3.Distance(damageRole.transform.position, transform.position);
        nowDis = totalDis;
        Destroy(this.gameObject, 2);
    }

    protected override void Update()
    {
        if (damageRole)
        {
            nowDis = Vector3.Distance(damageRole.transform.position, transform.position);
            if (nowDis < totalDis * 0.5 || nowDis < 2)
            {
                transform.LookAt(damageRole.transform.position);
            }
            transform.position += transform.forward * Time.deltaTime * speed;
        }
        else
        {
            Destroy(this.gameObject);
        }
        
        
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("hit");
        if (!other.CompareTag(tag))
        {
            Collider[] damagedRole = Physics.OverlapSphere(transform.position, damageArea, roleMask);
            foreach (Collider role in damagedRole)
            {
                if (role.tag != tag && role.GetComponent<Health>())
                {
                    if (role.GetComponent<Build>() && role.GetComponent<Build>().isAlive)
                    {
                        role.GetComponent<Health>().BeAttacked(damage, 1);
                    }

                    if (role.GetComponent<Actor>() && role.GetComponent<Actor>().isAlive)
                    {
                        role.GetComponent<Health>().BeAttacked(damage, 1);
                    }
                }
            }
            Destroy(this.gameObject);
        }
    }
}
