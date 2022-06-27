using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceManager : MonoBehaviour
{
    public List<Sprite> face;
    
    public Sprite FindFace(int _index)
    {
        return face[_index];
    }
}
