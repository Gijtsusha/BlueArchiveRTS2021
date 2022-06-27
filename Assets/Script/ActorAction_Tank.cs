using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorAction_Tank : ActorAction
{
    public Transform head;
    public Transform gun;
    public Transform fire;

    public LayerMask RoleMask;

    bool isTurnBody;
    Vector3 bodyTurnForward;
    float bodyAngleLerp;
    float bodyTurnSpeed = 40f;
    public bool isTurn;
    Vector3 TurnForward;
    public Vector3 headRot;

    float AngleLerp=60f;

    float Angle;
    public bool isTurnGun;


    protected override void Start()
    {
        base.Start();
        
        isTurnBody = false;
    }

    protected override void Move()
    {
        if (isTurnBody)
        {
            visualHandler.DrawLine(transform.position, endPos);
            visualHandler.Navline.enabled = true;
            TurnBody();
        }
        
        if (isTurn)
        {
            CheckAttack();
            RotateHead();
            RotateGun();
        }

        if (isMove)
        {
            visualHandler.DrawLine(transform.position, endPos);
            visualHandler.Navline.enabled = true;
            if (Vector3.Distance(transform.position, endPos) < 0.5)
            {
                StopMove();
                isMove = false;
            }
        }
        if (isAttackMove)
        {
            visualHandler.DrawLine(transform.position, endPos);
            visualHandler.Navline.enabled = true;
            if (Vector3.Distance(transform.position, endPos) < thisActor.AttackDistance)
            {
                StopMove();
                isAttackMove = false;
                if (!CheckObstacle())
                {
                    
                    if (AngleLerp < 1)
                    {
                        isTurn = false;
                        Attack();
                        isAttack = true;
                    }
                    else
                    {
                        isTurn = true;
                    }
                }
            }
        }
    }

    public override void SetMoveTo(Vector3 movePos)
    {
        ClearAction();
        endPos = movePos;

        bodyTurnForward = (endPos - transform.position).normalized;
        bodyAngleLerp = Vector3.Angle(bodyTurnForward, transform.forward);
        anim.SetBool(isMoveHash, true);
        if (bodyAngleLerp < 5)
        {
            isMove = true;
            MoveTo();
        }
        else
        {
            isTurnBody = true;
        }
        
    }

    void TurnBody()
    {
        bodyAngleLerp = Vector3.Angle(bodyTurnForward, transform.forward);
        if (bodyAngleLerp < 1)
        {
            isTurnBody = false;
            isMove = true;

            MoveTo();
        }
        float turnTime = bodyAngleLerp / bodyTurnSpeed;
        transform.forward = Vector3.Slerp(transform.forward, bodyTurnForward, Time.deltaTime / turnTime);
        

    }

    private void RotateHead()
    {
        if (head != null)
        {
            Vector3 localTargetPos = transform.InverseTransformPoint(transform.forward * 100f);
            if (attackRole)
            {
                localTargetPos = transform.InverseTransformPoint(attackRole.transform.position);
            }
            localTargetPos.y = 0.0f;
            Vector3 clampedLocalVec2Target = localTargetPos;
            if (localTargetPos.x >= 0.0f)
                clampedLocalVec2Target = Vector3.RotateTowards(Vector3.forward, localTargetPos, Mathf.Deg2Rad * 180f, float.MaxValue);
            else
                clampedLocalVec2Target = Vector3.RotateTowards(Vector3.forward, localTargetPos, Mathf.Deg2Rad * 180f, float.MaxValue);

            Quaternion rotationGoal = Quaternion.LookRotation(clampedLocalVec2Target);
            Quaternion newRotation = Quaternion.RotateTowards(head.localRotation, rotationGoal, AngleLerp * Time.deltaTime);

            head.localRotation = newRotation;
        }
    }

    private void RotateGun()
    {
        if (head != null && gun != null)
        {
            Vector3 localTargetPos = head.InverseTransformPoint(transform.forward * 100f);
            if (attackRole)
            {
                localTargetPos = head.InverseTransformPoint(attackRole.transform.position);
            }

            localTargetPos.x = 0.0f;

            Vector3 clampedLocalVec2Target = localTargetPos;
            if (localTargetPos.y >= 0.0f)
                clampedLocalVec2Target = Vector3.RotateTowards(Vector3.forward, localTargetPos, Mathf.Deg2Rad * 5f, float.MaxValue);
            else
                clampedLocalVec2Target = Vector3.RotateTowards(Vector3.forward, localTargetPos, Mathf.Deg2Rad * 5f, float.MaxValue);

            Quaternion rotationGoal = Quaternion.LookRotation(clampedLocalVec2Target);
            Quaternion newRotation = Quaternion.RotateTowards(gun.localRotation, Quaternion.Euler(rotationGoal.eulerAngles.z, rotationGoal.eulerAngles.y, rotationGoal.eulerAngles.x), AngleLerp * Time.deltaTime);

            gun.localRotation = newRotation;
        }
    }

    void CheckAttack()
    {
        GetAngleLerp();
        if (Angle < 2.5)
        {
            isTurn = false;
            Attack();
            isAttack = true;
        }
        else
        {
            isTurn = true;
        }
    }

    void GetAngleLerp()
    {
        if (attackRole)
        {
            TurnForward = (attackRole.transform.position - gun.transform.position);
        }
        else
        {
            TurnForward = transform.forward;
        }

        Angle = Vector3.Angle(TurnForward, -gun.transform.right);
    }


    void MoveTo()
    {
        transform.LookAt(endPos);
        agent.SetDestination(endPos);
        agent.isStopped = false;
    }

    public override void SetAttackTo(Vector3 AttackPos, GameObject AttackRole)
    {
        ClearAction();
        attackRole = AttackRole;
        

        if(Vector3.Distance(transform.position, AttackRole.transform.position) < thisActor.AttackDistance)
        {
            if (Vector3.Distance(attackRole.transform.position, transform.position) < thisActor.MinAttackDistance) { return; }


            if (!CheckObstacle())
            {
                visualHandler.Navline.enabled = false;
                CheckAttack();

            }
        }
        else
        {
            endPos = AttackPos;
            bodyTurnForward = (endPos - transform.position).normalized;
            bodyAngleLerp = Vector3.Angle(bodyTurnForward, transform.forward);
            visualHandler.Navline.enabled = true;
            anim.SetBool(isMoveHash, true);
            Debug.Log(bodyAngleLerp);
            if (bodyAngleLerp < 2)
            {
                isAttackMove = true;
                MoveTo();
            }
            else
            {
                isTurnBody = true;
            }
        }
    }
    public override void Attack()
    {
        //agent.isStopped = true;
        anim.SetBool(isMoveHash, false);
        anim.SetBool(isAttackHash, true);
    }

    protected override void Attacking()
    {
        if (isAttack)
        {
            CheckAttack();
            if (!attackRole)
            {
                StopAttack();
                return;
            }

            if (!attackRole.GetComponent<Actor>().isAlive)
            {
                StopAttack();
                return;
            }
            if (Vector3.Distance(attackRole.transform.position,transform.position) < thisActor.MinAttackDistance|| Vector3.Distance(attackRole.transform.position, transform.position) > thisActor.AttackDistance)
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


    protected override void ClearAction()
    {
        if (isMove)
        {
            StopMove();
            isMove = false;
        }
        if (isAttackMove)
        {
            StopMove();
            isAttackMove = false;
        }
    }

    protected override void IdleAction()
    {
        base.IdleAction();
        if (isAttack)
        {
            StopAttack();
        }
    }

    protected override void StopAttack()
    {
        base.StopAttack();
        if (thisActor.isAlive)
        {
            isTurn = true;
        }
    }

    

}
