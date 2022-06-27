using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Actor : MonoBehaviour
{
    public bool canBeSearch;
    public bool isAlive;

    public int roleType;//0普通单位 1机械单位 2道具

    public Sprite roleImage;

    public delegate void PosDelegate(Vector3 Pos);
    public event PosDelegate MoveEvent;
    

    public delegate void PosWithObjDelegate(Vector3 Pos,GameObject Obj);
    public event PosWithObjDelegate AttackEvent;
    public event PosWithObjDelegate StandEvent;
    public event PosWithObjDelegate KneelEvent;

    public delegate void VoidDelegate();
    public event VoidDelegate IdleEvent;


    public float AttackDistance;
    public float MinAttackDistance;
    public float Damage;

    [HideInInspector] public ActorVisualHandler visualHandler;
    [HideInInspector] public GameObject attackRole;

    private void Awake()
    {
        visualHandler = GetComponent<ActorVisualHandler>();
        isAlive = true;
        canBeSearch = true;
    }


    public void MoveAction(Vector3 movePos)
    {
        MoveEvent(movePos);
    }


    public void AttackAction(Vector3 AttackPos,GameObject AttackRole)
    {
        attackRole = AttackRole;
        AttackEvent(AttackPos, AttackRole);

    }


    public void StandAction(Vector3 movePos, GameObject StandCover)
    {
        if(GetComponent<ActorAction_AR>()|| GetComponent<ActorAction_MG>())
        {
            StandEvent(movePos, StandCover);
        }
        
    }

    public void KneelAction(Vector3 movePos, GameObject KneelCover)
    {
        if(GetComponent<ActorAction_AR>() || GetComponent<ActorAction_SR>() || GetComponent<ActorAction_SMG>())
        {
            KneelEvent(movePos, KneelCover);
        }
        
    }

    public void IdleAction()
    {
        IdleEvent();
    }
}
