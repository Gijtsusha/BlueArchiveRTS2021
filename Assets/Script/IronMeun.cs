using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IronMeun : MonoBehaviour
{
    public RectTransform Meun;
    public void OnMeunBthChick()
    {
        Time.timeScale = 0;
        Meun.position = new Vector3(960, 540, 0);
    }

    public void OnBackBthChick()
    {
        Time.timeScale = 1;
        Meun.position = new Vector3(960, 540 + 800, 0);
    }


    public void OnBackMainBthChick()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void OnExitBthChick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
