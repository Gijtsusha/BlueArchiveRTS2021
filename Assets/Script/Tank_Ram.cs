using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank_Ram : MonoBehaviour
{
    public float ramDamage;

    public Actor thisActor;

    [SerializeField] int count = 0;
    private void FixedUpdate()
    {
        count++;
        count %= 60;
    }
    private void OnTriggerStay(Collider other)
    {
        if (thisActor.GetComponent<ActorAction_Tank>().isMove || thisActor.GetComponent<ActorAction_Tank>().isAttackMove)
        {
            if (other.GetComponent<Health>() && other.GetComponent<Actor>().isAlive && other.tag != thisActor.tag && count == 0)
            {
                other.GetComponent<Health>().BeAttacked(ramDamage,4);
            }
        }
    }
}
