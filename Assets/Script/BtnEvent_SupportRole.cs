using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class BtnEvent_SupportRole : BtnEvent_Role
{
    public Transform[] AISetTrans;
    bool isSetIn;

    public override void OnRoleBtnChick()
    {
        if (canSpawn)
        {
            actorManager.isSetSupportRole = true;
            canSpawn = false;
            LoadingMask.GetComponent<Image>().fillAmount = 1;
            playerInfoManager.Gem -= GemCost;
            _Role = Instantiate(Role, SpawnPlace.position, Quaternion.identity, RoleManager);
            _Role.GetComponent<Actor>().canBeSearch = false;
            _Role.GetComponent<Collider>().enabled = false;
            if (_Role.GetComponent<Actor_AI>())
            {
                _Role.GetComponent<Actor_AI>().enabled = false;
            }
            if (_Role.GetComponent<Actor>().roleType == 1)
            {
                actorManager.allActors.Add(_Role.GetComponent<Actor>());
            }
            isSetIn = true;
        }
    }

    protected override void Update()
    {
        base.Update();
        if (isSetIn)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            Physics.Raycast(ray, out RaycastHit hitInfo, 100);
            Collider collider = hitInfo.collider;
            Vector3 setPoint = new Vector3(hitInfo.point.x, 0, hitInfo.point.z);
            _Role.transform.position = setPoint;
            if (Mouse.current.leftButton.isPressed && collider.CompareTag("Ground"))
            {
                actorManager.isSetSupportRole = false;
                _Role.GetComponent<Actor>().canBeSearch = true;
                _Role.GetComponent<Collider>().enabled = true;
                if (_Role.GetComponent<Actor_AI>())
                {
                    _Role.GetComponent<Actor_AI>().enabled = true;
                }
                isSetIn = false;
            }
        }
    }

    public override int SetRole_AI(int _Gem)
    {
        Transform transform = AISetTrans[(int)(Random.value * AISetTrans.Length - 0.1f)];
        if (canSpawn)
        {
            canSpawn = false;
            LoadingMask.GetComponent<Image>().fillAmount = 1;
            _Gem -= GemCost;
            _Role = Instantiate(Role, transform.position, transform.rotation, RoleManager);
            actorManager.allEnemyActors.Add(_Role.GetComponent<Actor>());
        }
        return _Gem;
    }
}
