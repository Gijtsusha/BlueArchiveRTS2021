using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor_AI_Turret : Actor_AI
{
    protected override void AIAttackAction()
    {
        actor.AttackAction(AttackedRole.transform.position + AttackedRole.transform.forward.normalized * 2, AttackedRole.gameObject);

    }
}
