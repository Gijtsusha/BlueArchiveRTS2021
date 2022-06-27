using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor_AI_Tank : Actor_AI
{
    protected override void AIAttackAction()
    {
        if (GetComponent<ActorAction_Tank>())
        {
            actor.AttackAction(AttackedRole.transform.position, AttackedRole.gameObject);
        }

    }
}
