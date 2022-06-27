using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BtnEvent_Role : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    public PlayerInfoManager playerInfoManager;
    public GameObject Role;
    public Transform SpawnPlace;
    public ActorManager actorManager;
    public Transform RoleManager;
    public GameObject LoadingMask;
    public RectTransform RoleInfo;


    public AnimationCurve CDCurve;
    public AnimationCurve InfoCurve;


    protected IEnumerator LCD;
    protected IEnumerator SMove;
    protected IEnumerator infoEnter;
    protected IEnumerator infoExit;

    public int GemCost;
    public float CD;
    public bool canSpawn;
    [SerializeField] bool inCD;

    [SerializeField] protected GameObject _Role;




    private void Start()
    {
        playerInfoManager = GameObject.Find("PlayerInfo").GetComponent<PlayerInfoManager>();
        actorManager = GameObject.Find("ActorManager").GetComponent<ActorManager>();
        GetComponent<Image>().sprite = Role.GetComponent<Actor>().roleImage;
        LCD = null;
        _Role = null;
        canSpawn = false;
        LoadingMask.GetComponent<Image>().fillAmount = 1;
        inCD = false;
    }
    protected virtual void Update()
    {
        ChickRole();
        ChickCost();
    }

    void ChickRole()
    {

        if (LCD != null && LoadingMask.GetComponent<Image>().fillAmount == 0)
        {
            StopCoroutine(LCD);
            LCD = null;
            inCD = false;
        }
        if (_Role!=null)
        {
            if (!_Role.GetComponent<Actor>().isAlive)
            {
                _Role = null;
                LCD = LoadingCD();
                StartCoroutine(LCD);
                inCD = true;
            }
        }
    }
    public virtual void OnRoleBtnChick()
    {
        if (canSpawn)
        {
            canSpawn = false;
            LoadingMask.GetComponent<Image>().fillAmount = 1;
            playerInfoManager.Gem -= GemCost;
            _Role = Instantiate(Role, SpawnPlace.position, SpawnPlace.rotation, RoleManager);
            actorManager.allActors.Add(_Role.GetComponent<Actor>());
            _Role.GetComponent<Actor_AI>().enabled = false;
            SMove = SpawnMove(_Role);
            StartCoroutine(SMove);
        }
            

    }

    public virtual int SetRole_AI(int _Gem)
    {
        if (canSpawn)
        {
            canSpawn = false;
            LoadingMask.GetComponent<Image>().fillAmount = 1;
            _Gem -= GemCost;
            _Role = Instantiate(Role, SpawnPlace.position, SpawnPlace.rotation, RoleManager);
            _Role.GetComponentInChildren<LineRenderer>().gameObject.SetActive(false);
            actorManager.allEnemyActors.Add(_Role.GetComponent<Actor>());
            SMove = SpawnMove(_Role);
            StartCoroutine(SMove);
            
        }
        return _Gem;
    }


    void ChickCost()
    {
        if(!inCD && _Role == null)
        {
            if(playerInfoManager.Gem >= GemCost)
            {
                canSpawn = true;
                LoadingMask.GetComponent<Image>().fillAmount = 0;
            }
            else
            {
                canSpawn = false;
                LoadingMask.GetComponent<Image>().fillAmount = 1;
            }
        }
    }

    IEnumerator LoadingCD()
    {
        float timer = 0;
        while (timer <= 1)
        {
            LoadingMask.GetComponent<Image>().fillAmount = 1-CDCurve.Evaluate(timer);
            timer += Time.deltaTime/CD;
            yield return null;
        }
        LoadingMask.GetComponent<Image>().fillAmount = 0;
    }

    protected IEnumerator SpawnMove(GameObject Role)
    {
        yield return null;
        Role.GetComponent<ActorAction>().SetMoveTo(SpawnPlace.position + SpawnPlace.forward);
        StopCoroutine(SMove);
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Down");
        RoleInfo.GetComponent<UIRoleInfoManager>().GetID(Role);
        infoEnter = InfoEnter();
        
        StartCoroutine(infoEnter);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Up");
        StopCoroutine(infoEnter);
        RoleInfo.localScale = Vector3.zero;
    }

    IEnumerator InfoEnter()
    {
        yield return new WaitForSeconds(0.5f);
        float timer = 0;
        while (timer <= 1)
        {
            RoleInfo.localScale = new Vector3(InfoCurve.Evaluate(timer) * 0.5f, InfoCurve.Evaluate(timer) * 0.5f, 0);
            timer += Time.deltaTime;
            yield return null;
        }

    }

}
