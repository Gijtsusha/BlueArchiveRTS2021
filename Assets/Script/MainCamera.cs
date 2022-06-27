using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class MainCamera : MonoBehaviour
{
    public float speed = 1;
    public float mouseSpeed = 1;

    Vector3 pos;
    Vector3 rot;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerType.playerType == 0)
        {
            transform.position = new Vector3(100, 30, 90);
        }
        else if (PlayerType.playerType == 1)
        {
            transform.position = new Vector3(20, 30, 90);
        }
        else if (PlayerType.playerType == 2)
        {
            transform.position = new Vector3(60, 30, 10);
        }
    }


    private void Update()
    {
        pos = transform.position;
        rot = transform.rotation.eulerAngles;

        HeightSet(pos, rot);

        MouseMove(pos,rot);

    }


    void MouseMove(Vector3 pos, Vector3 rot)
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Mouse.current.position.ReadValue().x < Screen.width * 0.05 && transform.position.x > 20)
            {
                transform.position = new Vector3(pos.x - speed, pos.y, pos.z);
            }
            else if (Mouse.current.position.ReadValue().x > Screen.width * 0.95 && transform.position.x < 100)
            {
                transform.position = new Vector3(pos.x + speed, pos.y, pos.z);
            }

            if (Mouse.current.position.ReadValue().y < Screen.height * 0.05 && transform.position.z > 10)
            {
                transform.position = new Vector3(pos.x, pos.y, pos.z - speed);
            }
            else if (Mouse.current.position.ReadValue().y > Screen.height * 0.95 && transform.position.z < 90)
            {
                transform.position = new Vector3(pos.x, pos.y, pos.z + speed);
            }
        }

    }

    void HeightSet(Vector3 pos, Vector3 rot)
    {
        float mouseMid = Mouse.current.scroll.ReadValue().y/120;
        if (pos.y - mouseMid * mouseSpeed < 20)
        {
            transform.position = new Vector3(pos.x, 20, pos.z);

        }
        else if (pos.y - mouseMid * mouseSpeed > 30)
        {
            transform.position = new Vector3(pos.x, 30, pos.z);

        }
        else
        {
            transform.position = new Vector3(pos.x, pos.y - mouseMid * mouseSpeed, pos.z);

        }

        /*if (rot.x - mouseMid * mouseSpeed * 3 < 60)
        {
            transform.rotation = Quaternion.Euler(new Vector3(60, rot.y, rot.z));
        }
        else if (rot.x - mouseMid * mouseSpeed * 3 > 75)
        {
            transform.rotation = Quaternion.Euler(new Vector3(75, rot.y, rot.z));
        }
        else
        {
            transform.rotation = Quaternion.Euler(new Vector3(rot.x - mouseMid * mouseSpeed * 3, rot.y, rot.z));
        }*/
    }

}
