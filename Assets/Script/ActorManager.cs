using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ActorManager : MonoBehaviour
{
    public GameOverView gameOverView;

    [SerializeField] private Transform selectedArea;
    public RectTransform SelectImage;
    public List<Actor> allActors = new List<Actor>();
    public List<Actor> allEnemyActors = new List<Actor>();
    public List<Actor> selectedActors = new List<Actor>();

    public List<Build> allBuilds = new List<Build>();
    public List<Build> allEnemyBuilds = new List<Build>();

    private Vector3 dragStartPos, dragEndPos, dragCenter, dragSize;
    private Vector3 mouseStartPos, mouseEndPos;
    bool isDraw;

    public LayerMask mouseDragLayerMask;
    public LayerMask dragSelectLayerMask;

    public bool isSetSupportRole;

    private void Start()
    {
        selectedArea.gameObject.SetActive(false);
        isDraw = false;
        isSetSupportRole = false;
        if (PlayerType.playerType == 0)
        {
            
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Trinity"))
            {
                if (obj.GetComponent<Actor>())
                {
                    allActors.Add(obj.GetComponent<Actor>());
                    if (obj.GetComponent<Actor_AI>())
                    {
                        obj.GetComponent<Actor_AI>().enabled = false;
                    }
                }
                if (obj.GetComponent<Build>())
                {
                    allBuilds.Add(obj.GetComponent<Build>());
                }

            }
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Gehenna"))
            {
                if (obj.GetComponent<Actor>())
                {
                    allEnemyActors.Add(obj.GetComponent<Actor>());
                    if (obj.GetComponentInChildren<LineRenderer>())
                    {
                        obj.GetComponentInChildren<LineRenderer>().gameObject.SetActive(false);
                    }
                }
                if (obj.GetComponent<Build>())
                {
                    allEnemyBuilds.Add(obj.GetComponent<Build>());
                }
            }
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Millennium"))
            {
                if (obj.GetComponent<Actor>())
                {
                    allEnemyActors.Add(obj.GetComponent<Actor>());
                    if (obj.GetComponentInChildren<LineRenderer>())
                    {
                        obj.GetComponentInChildren<LineRenderer>().gameObject.SetActive(false);
                    }
                }
                if (obj.GetComponent<Build>())
                {
                    allEnemyBuilds.Add(obj.GetComponent<Build>());
                }
            }

        }
        else if(PlayerType.playerType == 1)
        {
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Gehenna"))
            {
                if (obj.GetComponent<Actor>())
                {
                    allActors.Add(obj.GetComponent<Actor>());
                    if (obj.GetComponent<Actor_AI>())
                    {
                        obj.GetComponent<Actor_AI>().enabled = false;
                    }
                }
                if (obj.GetComponent<Build>())
                {
                    allBuilds.Add(obj.GetComponent<Build>());
                }

            }
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Trinity"))
            {
                if (obj.GetComponent<Actor>())
                {
                    allEnemyActors.Add(obj.GetComponent<Actor>());
                    if (obj.GetComponentInChildren<LineRenderer>())
                    {
                        obj.GetComponentInChildren<LineRenderer>().gameObject.SetActive(false);
                    }
                }
                if (obj.GetComponent<Build>())
                {
                    allEnemyBuilds.Add(obj.GetComponent<Build>());
                }
            }
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Millennium"))
            {
                if (obj.GetComponent<Actor>())
                {
                    allEnemyActors.Add(obj.GetComponent<Actor>());
                    if (obj.GetComponentInChildren<LineRenderer>())
                    {
                        obj.GetComponentInChildren<LineRenderer>().gameObject.SetActive(false);
                    }
                    
                }
                if (obj.GetComponent<Build>())
                {
                    allEnemyBuilds.Add(obj.GetComponent<Build>());
                }
            }
        }
        else if (PlayerType.playerType == 2)
        {
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Millennium"))
            {
                if (obj.GetComponent<Actor>())
                {
                    allActors.Add(obj.GetComponent<Actor>());
                    if (obj.GetComponent<Actor_AI>())
                    {
                        obj.GetComponent<Actor_AI>().enabled = false;
                    }
                    if (obj.GetComponent<Actor_AI_Turret>())
                    {
                        obj.GetComponent<Actor_AI_Turret>().enabled = true;
                    }
                }
                if (obj.GetComponent<Build>())
                {
                    allBuilds.Add(obj.GetComponent<Build>());
                }

            }
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Gehenna"))
            {
                if (obj.GetComponent<Actor>())
                {
                    allEnemyActors.Add(obj.GetComponent<Actor>());
                    if (obj.GetComponentInChildren<LineRenderer>())
                    {
                        obj.GetComponentInChildren<LineRenderer>().gameObject.SetActive(false);
                    }
                }
                if (obj.GetComponent<Build>())
                {
                    allEnemyBuilds.Add(obj.GetComponent<Build>());
                }
            }
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Trinity"))
            {
                if (obj.GetComponent<Actor>())
                {
                    allEnemyActors.Add(obj.GetComponent<Actor>());
                    if (obj.GetComponentInChildren<LineRenderer>())
                    {
                        obj.GetComponentInChildren<LineRenderer>().gameObject.SetActive(false);
                    }
                }
                if (obj.GetComponent<Build>())
                {
                    allEnemyBuilds.Add(obj.GetComponent<Build>());
                }
            }
        }



    }

    private void Update()
    {
        CheckGameOver();
        MouseInput();
        OnDraw();
        FixedSelect();
        CheckDeadSelect();
        CheckBuild();
    }

    void MouseInput()
    {

            if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            //Debug.Log("rightwasPressed");
            mouseStartPos = Mouse.current.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray,out RaycastHit raycastHit,100, mouseDragLayerMask))
            {
                dragStartPos = raycastHit.point;  
            }
        }
        else if (Mouse.current.rightButton.isPressed)
        {
            //Debug.Log("rightisPressed");
            mouseEndPos = Mouse.current.position.ReadValue();
            isDraw = true;
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit raycastHit, 100, mouseDragLayerMask))
            {
                dragEndPos = raycastHit.point;
            }

            if (Vector3.Distance(dragStartPos, dragEndPos) > 1)
            {
                //selectedArea.gameObject.SetActive(true);

                dragCenter = (dragStartPos + dragEndPos) / 2;
                dragSize = dragEndPos - dragStartPos;
                selectedArea.transform.position = dragCenter;
                selectedArea.transform.localScale = dragSize + Vector3.up/10;
            }
        }
        else if (Mouse.current.rightButton.wasReleasedThisFrame)
        {
            //Debug.Log("rightwasReleased");
            isDraw = false;
            DeselectActors();
            SelectActors();

        }

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (!EventSystem.current.IsPointerOverGameObject() && !isSetSupportRole)
            {
                SetTask();
            }
            
        }

        if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            foreach (Actor actor in selectedActors)
            {
                actor.IdleAction();
            }
        }

        if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            DeselectActors();
            foreach (Actor actor in allActors)
            {
                selectedActors.Add(actor);
                actor.visualHandler.Select();
            }
        }


    }

    void SelectActors()
    {

        dragSize.Set(Mathf.Abs(dragSize.x / 2), 1f, Mathf.Abs(dragSize.z / 2));
        RaycastHit[] hits = Physics.BoxCastAll(dragCenter, dragSize, Vector3.up, Quaternion.identity, 0, dragSelectLayerMask);
        foreach(RaycastHit hit in hits)
        {
            Actor actor = hit.collider.GetComponent<Actor>();
            if (PlayerType.playerType == 0)
            {
                if (actor != null && actor.isAlive && actor.tag== "Trinity")
                {
                    if (!actor.GetComponent<ActorAction_Serina>())
                    {
                        selectedActors.Add(actor);
                        actor.visualHandler.Select();
                    }
                    
                }
            }
            else if (PlayerType.playerType == 1)
            {
                if (actor != null && actor.isAlive && actor.tag == "Gehenna")
                {
                    selectedActors.Add(actor);
                    actor.visualHandler.Select();
                }
            }
            else if (PlayerType.playerType == 2)
            {
                if (actor != null && actor.isAlive && actor.tag == "Millennium")
                {
                    if (!actor.GetComponent<ActorAction_Turret>())
                    {
                        selectedActors.Add(actor);
                        actor.visualHandler.Select();
                    }

                }
            }
        }
    }

    void DeselectActors()
    {
        foreach(Actor actor in selectedActors)
        {
            actor.visualHandler.Deselect();
        }
        selectedActors.Clear();
    }

    void SetTask()
    {
        if (selectedActors.Count == 0)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        Collider collider = null;
        if(Physics.Raycast(ray,out RaycastHit hitInfo, 100))
        {
            collider = hitInfo.collider;
        }

        if (collider.CompareTag("Ground"))
        {
            foreach (Actor actor in selectedActors)
            {
                Vector3 goPoint = new Vector3(hitInfo.point.x, 0, hitInfo.point.z);
                actor.MoveAction(goPoint);
            }
        }

        if (collider.CompareTag("Trinity") || collider.CompareTag("Gehenna") || collider.CompareTag("Millennium"))
        {
            if (collider.GetComponent <Build>() && collider.GetComponent<Build>().isAlive)
            {
                foreach (Actor actor in selectedActors)
                {
                    if (collider.tag != actor.tag)
                    {
                        actor.AttackAction(hitInfo.point, collider.gameObject);
                    }
                }
            }

            if (collider.GetComponent<Actor>() && collider.GetComponent<Actor>().isAlive)
            {
                foreach (Actor actor in selectedActors)
                {
                    if (collider.tag != actor.tag)
                    {
                        actor.AttackAction(hitInfo.point, collider.gameObject);
                    }
                }
            }
        }

        if (collider.CompareTag("StandCover"))
        {
            foreach (Actor actor in selectedActors)
            {
                actor.StandAction(hitInfo.point, collider.gameObject);
            }
        }

        if (collider.CompareTag("KneelCover"))
        {
            foreach (Actor actor in selectedActors)
            {
                actor.KneelAction(hitInfo.point, collider.gameObject);
            }
        }


    }

    private void OnDraw()
    {
        if (isDraw)
        {
            SelectImage.GetComponent<Image>().enabled = true;
            SelectImage.position = new Vector3((mouseStartPos.x + mouseEndPos.x) / 2, (mouseStartPos.y + mouseEndPos.y) / 2, 0);
            SelectImage.sizeDelta = new Vector2(Mathf.Abs(mouseStartPos.x - mouseEndPos.x), Mathf.Abs(mouseStartPos.y - mouseEndPos.y));
        }
        else
        {
            SelectImage.GetComponent<Image>().enabled = false;
        }
    }

    void FixedSelect()
    {
        selectedArea.up = Camera.main.transform.forward;
    }

    void CheckDeadSelect()
    {
        foreach (Actor actor in selectedActors)
        {
            if (!actor.isAlive)
            {
                selectedActors = DeleteDeadSelect(selectedActors);
                return;
            }
        }
        foreach (Actor actor in allActors)
        {
            if (!actor.isAlive)
            {
                allActors = DeleteDeadSelect(allActors);
                return;
            }
        }
        foreach (Actor actor in allEnemyActors)
        {
            if (!actor.isAlive)
            {
                allEnemyActors = DeleteDeadSelect(allEnemyActors);
                return;
            }
        }

        
    }

    List<Actor> DeleteDeadSelect(List<Actor> _actors)
    {
        List<Actor> newSelectedActors = new List<Actor>();
        foreach (Actor actor in _actors)
        {
            if (!actor.isAlive)
            {
                actor.visualHandler.Deselect();
                
            }
            else
            {
                newSelectedActors.Add(actor);
            }
        }
        return newSelectedActors;

    }

    void CheckBuild()
    {
        foreach (Build build in allBuilds)
        {
            if (!build.isAlive)
            {
                allBuilds.Remove(build);
            }
        }
        foreach (Build build in allEnemyBuilds)
        {
            if (!build.isAlive)
            {
                allEnemyBuilds.Remove(build);
            }
        }
    }

    void CheckGameOver()
    {
        if (allBuilds.Count == 0)
        {
            gameOverView.DefeatView();
        }
        else if (allEnemyBuilds.Count == 0)
        {
            gameOverView.VictoryView();
        }
    }
}
