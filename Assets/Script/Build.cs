using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour
{
    public bool isAlive;
    public Spawn_AI SpawnAI;

    protected Health health;
    private void Start()
    {
        isAlive = true;
        health = GetComponent<Health>();
        health.DeadEvent += SetDead;
    }


    void SetDead()
    {
        isAlive = false;
        SpawnAI.enabled = false;
    }
}
