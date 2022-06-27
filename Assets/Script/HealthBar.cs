using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    Vector3 lookPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lookPoint = Vector3.ProjectOnPlane(gameObject.transform.position - Camera.main.transform.position, Camera.main.transform.forward);
        transform.LookAt(lookPoint+Camera.main.transform.position);
    }
}
