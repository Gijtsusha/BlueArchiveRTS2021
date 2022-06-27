using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEvent : MonoBehaviour
{
    protected Actor actor;
    public Transform BulletManager;
    public Transform fire;
    public GameObject bullet;
    // Start is called before the first frame update
    protected void Start()
    {
        BulletManager = GameObject.Find("BulletManager"+tag).transform;

        actor = GetComponent<Actor>();
    }

    // Update is called once per frame
    protected void Update()
    {
        
    }

    protected virtual void FireEvent()
    {
        GameObject _bullet = Instantiate(bullet, fire.position, fire.rotation, BulletManager);
        _bullet.tag = tag;
        if (_bullet.GetComponent<Bullet_Tank>())
        {
            fire.forward = (actor.attackRole.transform.position - fire.position).normalized;
            _bullet.GetComponent<Bullet_Tank>().damage = actor.Damage;
        }
        else if (_bullet.GetComponent<Bullet_GLM>())
        {
            _bullet.GetComponent<Bullet_GLM>().damage = actor.Damage;
            _bullet.GetComponent<Bullet_GLM>().damageRole = actor.attackRole;
        }
        else if (_bullet.GetComponent<Bullet_Aris>())
        {
            _bullet.GetComponent<Bullet_Aris>().damage = actor.Damage;
        }
        else
        {
            if (actor.attackRole)
            {
                actor.attackRole.GetComponent<Health>().BeAttacked(actor.Damage, 0);
            }
        }
    }

    public void AudioEvent()
    {
        GetComponent<AudioSource>().Play();
    }
}
