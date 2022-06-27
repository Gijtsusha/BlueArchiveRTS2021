using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEvent_SG : AnimEvent
{
    public LayerMask roleMask;

    protected override void FireEvent()
    {
        Collider[] findRole = Physics.OverlapSphere(transform.position, Vector3.Distance(transform.position, actor.attackRole.transform.position), roleMask);
        foreach (Collider role in findRole)
        {
            if (Vector3.Angle((role.transform.position - fire.position), fire.forward) < 60)
            {
                if (role.GetComponent<Health>() && role.GetComponent<Actor>().isAlive)
                {
                    role.GetComponent<Health>().BeAttacked(actor.Damage,0);
                }
            }
        }
    }
}
