using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListManager : MonoBehaviour
{
    public RectTransform[] roleLists;
    private void Awake()
    {
        roleLists[PlayerType.playerType].position = new Vector3(roleLists[PlayerType.playerType].position.x - 200, roleLists[PlayerType.playerType].position.y, roleLists[PlayerType.playerType].position.z);
        roleLists[PlayerType.playerType].GetComponent<Spawn_AI>().enabled = false;
    }
}
