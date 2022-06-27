using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorAction_AR : ActorAction
{
    protected override void Start()
    {
        base.Start();
        thisActor.StandEvent += SetStandCoverTo;
        thisActor.KneelEvent += SetKneelCoverTo;
    }

    protected override void Move()
    {
        base.Move();

        if (isSCover)
        {
            visualHandler.DrawLine(transform.position, endPos);
            visualHandler.Navline.enabled = true;
            if (Vector3.Distance(transform.position, endPos) < 0.5)
            {
                StandCover();
            }
        }
        if (isKCover)
        {
            visualHandler.DrawLine(transform.position, endPos);
            visualHandler.Navline.enabled = true;
            if (Vector3.Distance(transform.position, endPos) < 0.5)
            {
                KneelCover();
            }
        }
    }

    void SetStandCoverTo(Vector3 SCoverPos, GameObject SCoverObj)
    {
        IdleAction();

        float coverDis = Vector3.Distance(transform.position, SCoverObj.transform.position);
        List<BoxCollider> coverAreaList = new List<BoxCollider>();
        foreach (BoxCollider coverArea in SCoverObj.GetComponentsInChildren<BoxCollider>())
        {
            if (Vector3.Distance(transform.position, coverArea.transform.position) < coverDis)
            {
                cover = coverArea;
                coverDis = Vector3.Distance(transform.position, coverArea.transform.position);
            }

        }
        if (cover)
        {
            endPos = new Vector3(cover.transform.position.x, 0, cover.transform.position.z);
            agent.SetDestination(endPos);
            agent.isStopped = false;
            anim.SetBool(isMoveHash, true);
            isSCover = true;
        }
    }

    void StandCover()
    {
        agent.isStopped = true;
        visualHandler.Navline.enabled = false;
        transform.rotation = cover.transform.rotation;
        transform.position = endPos;
        anim.SetBool(isMoveHash, false);
        anim.SetBool(isNormalHash, false);
        anim.SetBool(isStandHash, true);
    }

    void StopStandCover()
    {
        cover = null;
        anim.SetBool(isStandHash, false);
        anim.SetBool(isNormalHash, true);
        isSCover = false;
    }

    void SetKneelCoverTo(Vector3 KCoverPos, GameObject KCoverObj)
    {
        IdleAction();

        float coverDis = Vector3.Distance(transform.position, KCoverObj.transform.position);
        List<BoxCollider> coverAreaList = new List<BoxCollider>();
        foreach (BoxCollider coverArea in KCoverObj.GetComponentsInChildren<BoxCollider>())
        {
            if (Vector3.Distance(transform.position, coverArea.transform.position) < coverDis)
            {
                cover = coverArea;
                coverDis = Vector3.Distance(transform.position, coverArea.transform.position);
            }

        }
        if (cover)
        {
            endPos = new Vector3(cover.transform.position.x, 0, cover.transform.position.z);
            agent.SetDestination(endPos);
            agent.isStopped = false;
            anim.SetBool(isMoveHash, true);
            isKCover = true;
        }
    }

    void KneelCover()
    {
        agent.isStopped = true;
        visualHandler.Navline.enabled = false;
        transform.rotation = cover.transform.rotation;
        transform.position = endPos;
        anim.SetBool(isMoveHash, false);
        anim.SetBool(isNormalHash, false);
        anim.SetBool(isKneelHash, true);
    }

    void StopKneelCover()
    {
        cover = null;
        anim.SetBool(isKneelHash, false);
        anim.SetBool(isNormalHash, true);
        isKCover = false;
    }


    protected override void IdleAction()
    {
        base.IdleAction();
        if (isSCover)
        {
            StopStandCover();
        }
        if (isKCover)
        {
            StopKneelCover();
        }
    }
}
