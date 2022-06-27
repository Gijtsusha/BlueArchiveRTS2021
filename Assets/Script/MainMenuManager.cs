using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public GameObject StartGameView;
    public Button StartGameViewBtn;


    // Start is called before the first frame update
    void Start()
    {
        StartGameView.SetActive(false);
        StartGameViewBtn.onClick.AddListener(delegate () 
        {
            StartGameView.SetActive(!StartGameView.activeSelf);
        });
    }

    // Update is called once per frame
    void Update()
    {

    }

}
