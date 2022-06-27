using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorVisualHandler : MonoBehaviour
{
    public SpriteRenderer SelectSprite;
    public LineRenderer Navline;

    public void Start()
    {
        SelectSprite.enabled = false;
    }
    public void Select()
    {
        SelectSprite.enabled = true;
    }

    public void Deselect()
    {
        SelectSprite.enabled = false;
    }

    public void DrawLine(Vector3 start,Vector3 end)
    {
        start = new Vector3(start.x, start.y+0.1f, start.z);
        end = new Vector3(end.x, end.y+0.1f, end.z);
        Navline.SetPosition(0, start);
        Navline.SetPosition(1, end);
    }

}
