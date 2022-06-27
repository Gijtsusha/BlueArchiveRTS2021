using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public delegate void PosDelegate();
    public event PosDelegate DeadEvent;

    public SpriteRenderer HealthSprite;
    public float HP;
    float per;
    float maxHP;



    Animator Anim;

    // Start is called before the first frame update
    void Start()
    {
        maxHP = HP;
        Anim = GetComponent<Animator>();
        per = 7.14f / HP;
    }

    // Update is called once per frame
    void Update()
    {
        HealthSprite.size = new Vector2(HP * per, HealthSprite.size.y);
    }

    public void BeAttacked(float dmg, int bulletType)//Type 0-ÆÕÍ¨×Óµ¯ 1-Áñµ¯ 2-ÅÚµ¯||µ¼µ¯ 3-ÄÜÁ¿ÎäÆ÷ 4-ÔØ¾ß×²»÷
    {
        if (GetComponent<Build>())
        {
            Debug.Log("BuildBeAttacked" + HP);
        }
        else if (GetComponent<Actor>())
        {
            if (GetComponent<Actor>().roleType == 1)
            {
                if (bulletType == 0)
                {
                    dmg /= 2;
                }
                else if (bulletType == 3)
                {
                    dmg *= 2;
                }
            }
            else if (GetComponent<Actor>().roleType == 0)
            {
                if (Anim.GetBool("isStand").Equals(true) || Anim.GetBool("isKneel").Equals(true))
                {
                    if (bulletType == 1 || bulletType == 3)
                    {

                    }
                    else
                    {
                        dmg /= 2;
                    }
                }
            }
        }
        
        
        HP -= dmg;
        if (HP <= 0)
        {
            HP = 0;
            DeadEvent();
        }
        
    }

    public void Restore(float healing)
    {
        HP += healing;
        if (HP > maxHP)
        {
            HP = maxHP;
        }
    }

    public bool isFullHP()
    {
        if (HP == maxHP) return true;
        else             return false;
    }

    public float GetPerHP()
    {
        return HP/maxHP;
    }
}
