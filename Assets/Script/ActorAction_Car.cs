using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorAction_Car : ActorAction
{
    public float healingArea;
    public LayerMask roleMask;
    float timer = 0;


    protected override void Start()
    {
        base.Start();

    }


    protected override void Update()
    {
        base.Update();
        Healing();
    }

    protected override void Move()
    {
        base.Move();


    }

    public override void SetMoveTo(Vector3 movePos)
    {
        IdleAction();

        agent.SetDestination(movePos);
        agent.isStopped = false;
        anim.SetBool(isMoveHash, true);
        isMove = true;
        endPos = movePos;
    }


    protected override void IdleAction()
    {
        base.IdleAction();
    }

    void Healing()
    {

        if (timer >= 1)
        {
            Collider[] damagedRole = Physics.OverlapSphere(transform.position, healingArea, roleMask);
            foreach (Collider role in damagedRole)
            {
                Debug.Log(role.name);
                if (role.tag == tag && role.GetComponent<Health>())
                {

                    if (role.GetComponent<Actor>() && role.GetComponent<Actor>().roleType==0 && role.GetComponent<Actor>().isAlive)
                    {
                       
                        role.GetComponent<Health>().Restore(thisActor.Damage);
                    }
                }
            }
            timer -= 1;
        }

        timer += Time.deltaTime;
    }
}
