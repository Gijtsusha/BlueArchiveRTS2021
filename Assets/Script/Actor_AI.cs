using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor_AI : MonoBehaviour
{
    public Actor actor;
    protected Animator anim;

    public float horizon;
    [SerializeField] protected Collider AttackedRole=null;
    [SerializeField] protected List<Collider> CanUseCover = new List<Collider>();

    public LayerMask roleMask;
    public LayerMask CoverMask;

    public Transform midTrans;
    public Transform endTrans;
    bool attachMid;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        midTrans = GameObject.Find("MidTrans").transform;
        endTrans = null;
        attachMid = false;
    }

    // Update is called once per frame
    void Update()
    {
        AutoAction();
        ChickAttachMid();
        ChickAttack();
    }

    protected virtual void AutoAction()
    {
        if (actor.isAlive)
        {
            
            FindEnemy();
            if (AttackedRole != null)
            {
                AIAttackAction();
            }
            else
            {
                if (!GetComponent<ActorAction_Turret>() && !GetComponent<ActorAction_Serina>())
                {
                    AIMoveAction();
                }
            }
        }
    }

    protected virtual void AIAttackAction()
    {
        if (GetComponent<ActorAction_AR>())
        {
            if (!anim.GetBool("isKneel") && !anim.GetBool("isStand"))
            {
                FindCover();
                if (CanUseCover.Count != 0)
                {
                    UseCover(CanUseCover);

                }
                else
                {
                    actor.AttackAction(AttackedRole.transform.position + AttackedRole.transform.forward.normalized * 2, AttackedRole.gameObject);
                }
            }
            else
            {
                actor.AttackAction(AttackedRole.transform.position, AttackedRole.gameObject);
            }
        }
        if (GetComponent<ActorAction_SR>() || GetComponent<ActorAction_SMG>() || GetComponent<ActorAction_GM>() || GetComponent<ActorAction_GLM>())
        {
            if (!anim.GetBool("isKneel"))
            {
                FindCover();
                if (CanUseCover.Count != 0)
                {
                    UseCover(CanUseCover);

                }
                else
                {
                    actor.AttackAction(AttackedRole.transform.position + AttackedRole.transform.forward.normalized * 2, AttackedRole.gameObject);
                }

            }
            else
            {
                actor.AttackAction(AttackedRole.transform.position, AttackedRole.gameObject);
            }
        }
        if (GetComponent<ActorAction_MG>())
        {
            if (!anim.GetBool("isStand"))
            {
                FindCover();
                if (CanUseCover.Count != 0)
                {
                    UseCover(CanUseCover);

                }
                else
                {
                    actor.AttackAction(AttackedRole.transform.position + AttackedRole.transform.forward.normalized * 2, AttackedRole.gameObject);
                }
            }
            else
            {
                actor.AttackAction(AttackedRole.transform.position, AttackedRole.gameObject);
            }
        }
        if (GetComponent<ActorAction_Car>())
        {
            actor.AttackAction(AttackedRole.transform.position, AttackedRole.gameObject);
        }
    }

    protected void AIMoveAction()
    {
        if (!anim.GetBool("isMove") && !anim.GetBool("isAttack"))
        {
            if (!attachMid)
            {
                actor.MoveAction(midTrans.position);
            }
            else
            {
                if (endTrans != null)
                {
                    actor.MoveAction(endTrans.position);
                }
            }
        }
        
    }

    void ChickAttack()
    {
        if (AttackedRole)
        {
            
            if (AttackedRole.GetComponent<Actor>() && !AttackedRole.GetComponent<Actor>().isAlive)
            {
                AttackedRole = null;
            }
            else if(AttackedRole.GetComponent<Build>() && !AttackedRole.GetComponent<Build>().isAlive)
            {
                AttackedRole = null;
            }

        }
        
    }

    void ChickAttachMid()
    {
        if (!attachMid && Vector3.Distance(transform.position,midTrans.position) < 5)
        {
            attachMid = true;
            actor.IdleAction();
        }
    }

    void FindEnemy()
    {
        
        Collider[] FindRole = Physics.OverlapSphere(transform.position, horizon, roleMask);
        
        if (FindRole.Length != 0)
        {
            float nearestRole = horizon;
            foreach (Collider role in FindRole)
            {
                if (role.GetComponent<Actor>())
                {
                    if (role.GetComponent<Actor>().canBeSearch && role.tag != tag && role.GetComponent<Actor>().isAlive && Vector3.Distance(transform.position, role.transform.position) < nearestRole)
                    {
                        endTrans = GameObject.Find("EndTrans"+ role.tag).transform;
                        nearestRole = Vector3.Distance(transform.position, role.transform.position);
                        AttackedRole = role;
                    }
                }
                else if (role.GetComponent<Build>())
                {
                    if (role.tag != tag && role.GetComponent<Build>().isAlive && Vector3.Distance(transform.position, role.transform.position) < nearestRole)
                    {
                        endTrans = GameObject.Find("EndTrans" + role.tag).transform;
                        nearestRole = Vector3.Distance(transform.position, role.transform.position);
                        AttackedRole = role;
                    }
                }

            }
        }
        else
        {
            AttackedRole = null;
        }
    }

    void FindCover()
    {
        CanUseCover.Clear();
        Collider[] FindCover = Physics.OverlapSphere(transform.position, Vector3.Distance(transform.position,AttackedRole.transform.position), CoverMask);
        if (FindCover.Length != 0)
        {
            
            Vector3 toEnemyVec = (AttackedRole.transform.position - transform.position).normalized;
            foreach(Collider cover in FindCover)
            {
                Vector3 toCoverVec = (cover.transform.position - transform.position).normalized;
                if (Vector3.Angle(toEnemyVec,toCoverVec) < 45 && Vector3.Distance(cover.transform.position,AttackedRole.transform.position)<actor.AttackDistance)
                {
                    if (GetComponent<ActorAction_AR>())
                    {
                        if (cover.tag == "KneelCover" || cover.tag == "StandCover")
                        {
                            CanUseCover.Add(cover);
                        }
                    }
                    else if (GetComponent<ActorAction_SR>())
                    {
                        if (cover.tag == "KneelCover")
                        {
                            CanUseCover.Add(cover);
                        }
                    }
                    else if (GetComponent<ActorAction_MG>())
                    {
                        if (cover.tag == "StandCover")
                        {
                            CanUseCover.Add(cover);
                        }
                    }
                }
            }
        }
        else
        {
            CanUseCover.Clear();
        }
    }

    void UseCover(List<Collider> covers)
    {
        Collider usedCover=null;
        float nearestCover = Vector3.Distance(AttackedRole.transform.position, transform.position);
        foreach (Collider cover in covers)
        {
            if(Vector3.Distance(cover.transform.position, transform.position) < nearestCover)
            {
                nearestCover = Vector3.Distance(cover.transform.position, transform.position);
                usedCover = cover;

            }
        }
        if (usedCover)
        {
            if (usedCover.tag == "StandCover")
            {
                actor.StandAction(usedCover.transform.position, usedCover.gameObject);
            }
            else if (usedCover.tag == "KneelCover")
            {
                actor.KneelAction(usedCover.transform.position, usedCover.gameObject);
            }
        }
    }


/*
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Color c = new Color(1, 0, 0, 0.1f);
        UnityEditor.Handles.color = c;
        Vector3 forward = Quaternion.Euler(0, -horizon * 0.5f, 0) * transform.forward;
        UnityEditor.Handles.DrawSolidArc(transform.position, transform.up, forward, 360, horizon);
    }
#endif
*/
}
