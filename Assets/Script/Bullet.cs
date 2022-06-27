using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 1;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Debug.Log(tag);
        Destroy(this.gameObject, 5f);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        transform.position += transform.forward * Time.deltaTime*speed;
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("hit");
        if (!other.CompareTag(tag))
        {
            Destroy(this.gameObject);
        }
    }
}
