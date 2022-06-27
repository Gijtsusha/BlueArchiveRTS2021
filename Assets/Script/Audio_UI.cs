using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Audio_UI : MonoBehaviour
{
    public AudioSource onClick;
    public AudioSource[] GameStartView;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            onClick.Play();
        }
    }

}
