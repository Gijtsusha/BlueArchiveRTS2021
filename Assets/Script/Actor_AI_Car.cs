using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor_AI_Car : Actor_AI
{
    public ActorManager actorManager;

    protected override void Start()
    {
        base.Start();
        actorManager = GameObject.Find("ActorManager").GetComponent<ActorManager>();
    }

    protected override void AutoAction()
    {
        if (actor.isAlive)
        {

            FindTeammate();
            if (AttackedRole != null)
            {
                AIAttackAction();
            }
        }
    }

    void FindTeammate()
    {
        foreach(Actor actor in actorManager.allEnemyActors)
        {
            if (actor.tag == tag && actor.roleType == 0 && actor.GetComponent<Actor>().canBeSearch && actor.GetComponent<Actor>().isAlive && !actor.GetComponent<Health>().isFullHP())
            {
                AttackedRole = actor.GetComponent<Collider>();
            }
        }
    }
}
