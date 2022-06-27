using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorAction_Turret : ActorAction
{
    public Transform head;

    protected override void Start()
    {
        base.Start();
    }


    protected override void Update()
    {
        CheckAlive();
        Attacking();
    }


    public override void SetAttackTo(Vector3 AttackPos, GameObject AttackRole)
    {
        ClearAction();

        attackRole = AttackRole;

        if (Vector3.Distance(transform.position, AttackRole.transform.position) < thisActor.AttackDistance)
        {
            if (!CheckObstacle())
            {
                Attack();
                isAttack = true;
            }

        }
    }

    public override void Attack()
    {
        head.transform.LookAt(attackRole.transform);
        anim.SetBool(isAttackHash, true);
    }

    protected override void Attacking()
    {
        if (isAttack)
        {
            if (attackRole)
            {
                head.transform.LookAt(attackRole.transform);
                if (!attackRole.GetComponent<Actor>().isAlive)
                {
                    StopAttack();
                    return;
                }
                if (Vector3.Distance(attackRole.transform.position, transform.position) > thisActor.AttackDistance)
                {
                    StopAttack();
                    return;
                }
                if (CheckObstacle())
                {
                    StopAttack();
                    return;
                }
            }
            

        }
    }

    protected override void IdleAction()
    {
        base.IdleAction();
    }

    protected override void SetDead()
    {
        isDead = true;
        anim.SetTrigger(isDeadHash);
        IdleAction();
        GetComponent<Actor_AI_Turret>().enabled = false;
    }
}
