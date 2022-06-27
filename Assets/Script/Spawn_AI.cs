using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_AI : MonoBehaviour
{
    [SerializeField] BtnEvent_Role[] canUseBths;
    [SerializeField] List<BtnEvent_Role> randomList = new List<BtnEvent_Role>();

    public int Gem;
    IEnumerator GetG;
    private void Start()
    {
        canUseBths = GetComponentsInChildren<BtnEvent_Role>();
        Gem = 120;
        GetG = GetGem();
        StartCoroutine(GetG);
    }

    private void Update()
    {
        randomList.Clear();
        AddToList();
        if (randomList.Count > 0)
        {
            int index = (int)(Random.value * randomList.Count -0.1f);
            BtnEvent_Role role = randomList[index];
            if (role.GetComponent<BtnEvent_SupportRole>())
            {
                Gem = role.GetComponent<BtnEvent_SupportRole>().SetRole_AI(Gem);

            }
            else
            {
                Gem = role.SetRole_AI(Gem);
            }
        }
    }

    void AddToList()
    {
        foreach (BtnEvent_Role role in canUseBths)
        {
            if (role.GemCost < Gem && role.canSpawn)
            {
                randomList.Add(role);
            }
        }
    }

    IEnumerator GetGem()
    {
        float timer = 0;
        while (true)
        {
            if (timer >= 1)
            {
                timer -= 1;
                Gem += 10;
            }
            timer += Time.deltaTime;
            yield return null;
        }

    }
}
