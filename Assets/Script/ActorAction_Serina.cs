using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorAction_Serina : MonoBehaviour
{
    LayerMask roleMask;
    float damageArea;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tag))
        {
            if(other.GetComponent<Actor>() && other.GetComponent<Health>())
            {
                if(other.GetComponent<Actor>().roleType == 0 && !other.GetComponent<Health>().isFullHP())
                {
                    other.GetComponent<Health>().Restore(GetComponent<Actor>().Damage);
                    GetComponent<Actor>().isAlive = false;
                    Destroy(this.gameObject,0.1f);
                }
            }
        }
    }
}
