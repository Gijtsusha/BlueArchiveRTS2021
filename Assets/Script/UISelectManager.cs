using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISelectManager : MonoBehaviour
{
    public ActorManager actorManager;
    public Transform content;

    public GameObject SelectRole;

    private void Update()
    {
        DelectRole();
        InsertRole();
    }

    void DelectRole()
    {
        for(int i = 0; i < content.childCount; i++)
        {
            Destroy(content.GetChild(i).gameObject);
        }
    }

    void InsertRole()
    {
        for (int i = 0; i < actorManager.selectedActors.Count; i++)
        {
            GameObject gameObject = Instantiate(SelectRole, content);
            gameObject.GetComponent<Image>().sprite = actorManager.selectedActors[i].GetComponent<Actor>().roleImage;
            gameObject.GetComponentInChildren<Slider>().value = actorManager.selectedActors[i].GetComponent<Health>().GetPerHP();

        }
    }
}
