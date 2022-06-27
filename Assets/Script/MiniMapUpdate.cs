using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapUpdate : MonoBehaviour
{
    public ActorManager actorManager;

    public GameObject RolePlayer;
    public GameObject RoleEnemy;
    public Transform MiniMap;

    public List<GameObject> RolePlayerList;
    public List<GameObject> RoleEnemyList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject gameObject in RolePlayerList)
        {
            Destroy(gameObject);
        }
        foreach (GameObject gameObject in RoleEnemyList)
        {
            Destroy(gameObject);
        }
        for (int i = 0; i < actorManager.allActors.Count; i++)
        {
            GameObject gameObject = Instantiate(RolePlayer, MiniMap);
            gameObject.transform.localPosition = new Vector3(actorManager.allActors[i].transform.localPosition.x * 3 - 180, actorManager.allActors[i].transform.localPosition.z * 3 - 180, 0);
            RolePlayerList.Add(gameObject);

        }
        for (int i = 0; i < actorManager.allEnemyActors.Count; i++)
        {
            GameObject gameObject = Instantiate(RoleEnemy, MiniMap);
            gameObject.transform.localPosition = new Vector3(actorManager.allEnemyActors[i].transform.localPosition.x * 3 - 180, actorManager.allEnemyActors[i].transform.localPosition.z * 3 - 180, 0);
            RoleEnemyList.Add(gameObject);

        }
    }
}
