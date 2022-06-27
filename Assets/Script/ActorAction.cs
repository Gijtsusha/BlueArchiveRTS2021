using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;

public class ActorAction : MonoBehaviour
{
    public LayerMask LevelMask;

    protected Actor thisActor;
    protected ActorVisualHandler visualHandler;
    protected Health health;


    [SerializeField] protected GameObject attackRole;
    protected BoxCollider cover;


    protected NavMeshAgent agent;
    protected Animator anim;

    protected int isMoveHash = Animator.StringToHash("isMove");
    protected int isNormalHash = Animator.StringToHash("isNormal");
    protected int isStandHash = Animator.StringToHash("isStand");
    protected int isKneelHash = Animator.StringToHash("isKneel");
    protected int isAttackHash = Animator.StringToHash("isAttack");
    protected int isDeadHash = Animator.StringToHash("isDead");

    public bool isMove;
    public bool isAttackMove;
    [SerializeField] protected bool isAttack;
    [SerializeField] protected bool isSCover;
    [SerializeField] protected bool isKCover;
    [SerializeField] protected bool isDead;

    public bool isCover;

    protected Vector3 endPos;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        thisActor = GetComponent<Actor>();
        visualHandler = GetComponent<ActorVisualHandler>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        health = GetComponent<Health>();
        thisActor.MoveEvent += SetMoveTo;
        thisActor.AttackEvent += SetAttackTo;
        thisActor.IdleEvent += IdleAction;
        health.DeadEvent += SetDead;

        isMove = false;
        isAttackMove = false;
        isAttack = false;
        isSCover = false;
        isKCover = false;
        anim.SetBool(isNormalHash, true);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        CheckAlive();
        Move();
        Attacking();
        
    }

    protected virtual void Move()
    {
        if(isMove)
        {
            DrawLine();
            if (Vector3.Distance(transform.position, endPos) < 0.5)
            {
                StopMove();
                isMove = false;
            }

        }

        if (isAttackMove)
        {
            DrawLine();
            if (Vector3.Distance(transform.position, endPos) < thisActor.AttackDistance)
            {
                StopMove();
                if (!CheckObstacle())
                {
                    isAttackMove = false;
                    Attack();
                    isAttack = true;
                }
            }
        }


    }

    public virtual void SetMoveTo(Vector3 movePos)
    {
        IdleAction();
        transform.LookAt(movePos);
        agent.SetDestination(movePos);
        agent.isStopped = false;
        anim.SetBool(isMoveHash, true);
        isMove = true;
        endPos = movePos;
    }

    protected virtual void StopMove()
    {
        agent.isStopped = true;
        DeleteLine();
        anim.SetBool(isMoveHash, false);
        
    }

    public virtual void SetAttackTo(Vector3 AttackPos,GameObject AttackRole)
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
            transform.LookAt(endPos);
            agent.SetDestination(endPos);
            agent.isStopped = false;
            anim.SetBool(isMoveHash, true);
            isAttackMove = true;
        }
    }

    public virtual void Attack()
    {
        agent.isStopped = true;
        transform.LookAt(attackRole.transform);
        anim.SetBool(isMoveHash, false);
        anim.SetBool(isAttackHash, true);
    }

    protected virtual void StopAttack()
    {
        attackRole = null;
        anim.SetBool(isAttackHash, false);
        isAttack = false;
    }

    

    protected virtual void Attacking()
    {
        if (isAttack)
        {
            if (attackRole)
            {
                transform.LookAt(attackRole.transform);
                if (attackRole.GetComponent<Actor>() && !attackRole.GetComponent<Actor>().isAlive)
                {
                    StopAttack();
                    return;
                }
                if (attackRole.GetComponent<Build>() && !attackRole.GetComponent<Build>().isAlive)
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
            else
            {
                StopAttack();
            }
            
            
            
            
        }
        
    }

    protected virtual void ClearAction()
    {
        if (isMove)
        {
            StopMove();
            isMove = false;
        }
        if (isAttack)
        {
            StopAttack();
        }
        if (isAttackMove)
        {
            StopMove();
            isAttackMove = false;
        }
    }

    protected virtual void SetDead()
    {
        isDead = true;
        anim.SetTrigger(isDeadHash);
        IdleAction();
        GetComponent<Actor_AI>().enabled = false;
        if (GetComponent<NavMeshAgent>())
        {
            GetComponent<NavMeshAgent>().enabled = false;
        }
        //Destroy(this.gameObject, 2);
    }

    protected void CheckAlive()
    {
        if (isDead)
        {
            thisActor.isAlive = false;
            Destroy(this.gameObject, 2);
        }
        if (attackRole)
        {
            if (attackRole.GetComponent<Build>() && !attackRole.GetComponent<Build>().isAlive)
            {
                attackRole = null;
            }
            else if (attackRole.GetComponent<Actor>() && !attackRole.GetComponent<Actor>().isAlive)
            {
                attackRole = null;
            }
        }
    }

    protected bool CheckObstacle()
    {
        RaycastHit[] HitsInfo;

        Ray ray = new Ray(transform.position + Vector3.up * 0.1f, attackRole.transform.position - transform.position + Vector3.up * 0.1f);
        HitsInfo = Physics.RaycastAll(ray, Vector3.Distance(transform.position, attackRole.transform.position), LevelMask);
        foreach(RaycastHit hit in HitsInfo)
        {
            Debug.Log(hit.collider.tag);
            Debug.DrawLine(ray.origin, hit.point, Color.red);
            Debug.Log("HaveObstacle");
            if (hit.collider.tag == "Wall")
            {
                return true;
            }
        }
        return false;
    }

    protected void DrawLine()
    {
        visualHandler.DrawLine(transform.position, endPos);
        visualHandler.Navline.enabled = true;
    }

    protected void DeleteLine()
    {
        visualHandler.Navline.enabled = false;
    }

    protected virtual void IdleAction()
    {
        ClearAction();
    }

}
