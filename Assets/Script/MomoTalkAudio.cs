using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MomoTalkAudio : MonoBehaviour
{

    public List<AudioSource> sources=new List<AudioSource>();

    bool isOpen;
    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;

        GetComponent<Button>().onClick.AddListener(delegate() 
        {
            if (isOpen == false)
            {
                sources[0].Play();
                isOpen = true;
            }
            else
            {
                sources[1].Play();
                isOpen = false;
            }
        });
    }

}
