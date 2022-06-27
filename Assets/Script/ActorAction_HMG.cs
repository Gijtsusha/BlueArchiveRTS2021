using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorAction_HMG : ActorAction
{
    IEnumerator DMove;

    protected override void Start()
    {
        base.Start();
        DMove = null;
    }

    protected override void Move()
    {
        base.Move();

    }


    public override void SetMoveTo(Vector3 movePos)
    {
        if (isMove)
        {
            IdleAction();
            agent.SetDestination(movePos);
            agent.isStopped = false;
            transform.LookAt(movePos);
        }
        else
        {
            IdleAction();
            DMove = DelayMove(movePos);
            StartCoroutine(DMove);
        }

        anim.SetBool(isMoveHash, true);
        isMove = true;
        endPos = movePos;
    }

    protected override void StopMove()
    {
        base.StopMove();
        if (DMove != null)
        {
            StopCoroutine(DMove);
            DMove = null;
        }
    }

    public override void SetAttackTo(Vector3 AttackPos, GameObject AttackRole)
    {
        if (isAttackMove)
        {
            ClearAction();

            attackRole = AttackRole;

            if (Vector3.Distance(transform.position, AttackRole.transform.position) < thisActor.AttackDistance)
            {
                if (!CheckObstacle())
                {
                    DeleteLine();
                    Attack();
                    isAttack = true;
                }

            }
            else
            {
                IdleAction();
                endPos = AttackPos;
                agent.SetDestination(endPos);
                agent.isStopped = false;
                anim.SetBool(isMoveHash, true);
                isAttackMove = true;
            }
        }
        else
        {
            ClearAction();

            attackRole = AttackRole;

            if (Vector3.Distance(transform.position, AttackRole.transform.position) < thisActor.AttackDistance)
            {
                if (!CheckObstacle())
                {
                    DeleteLine();
                    Attack();
                    isAttack = true;
                }

            }
            else
            {
                IdleAction();
                endPos = AttackPos;
                DMove = DelayMove(endPos);
                StartCoroutine(DMove);
                anim.SetBool(isMoveHash, true);
                isAttackMove = true;
            }
        }
        
    }

    protected override void IdleAction()
    {
        base.IdleAction();
    }

    IEnumerator DelayMove(Vector3 movePos)
    {
        yield return new WaitForSeconds(1);
        //Debug.Log("delay");
        transform.LookAt(movePos);
        agent.SetDestination(movePos);
        agent.isStopped = false;

    }
}
