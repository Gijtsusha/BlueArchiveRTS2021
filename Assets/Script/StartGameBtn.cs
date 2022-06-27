using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGameBtn : MonoBehaviour
{
    public GameObject StartGameView;

    public void OnBtnClick0()
    {
        PlayerType.playerType = 0;
        SceneManager.LoadScene(1);
    }

    public void OnBtnClick1()
    {
        PlayerType.playerType = 1;
        SceneManager.LoadScene(1);
    }

    public void OnBtnClick2()
    {
        PlayerType.playerType = 2;
        SceneManager.LoadScene(1);
    }

    public void OnBackBtnClick()
    {
        StartGameView.SetActive(!StartGameView.activeSelf);
    }
}
