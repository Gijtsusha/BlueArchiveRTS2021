using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    public Slider BGM_Slider;
    public Slider SFX_Slider;
    public Slider UI_Slider;
    public Button ExitGame;

    private void Start()
    {
        //BGM_Slider.onValueChanged.AddListener(delegate (float value) { GameObject.Find("AudioManager").GetComponent<SettingManager>().SetBGMVolume(value); });
        //SFX_Slider.onValueChanged.AddListener(delegate (float value) { GameObject.Find("AudioManager").GetComponent<SettingManager>().SetBGMVolume(value); });
        //UI_Slider.onValueChanged.AddListener(delegate (float value) { GameObject.Find("AudioManager").GetComponent<SettingManager>().SetBGMVolume(value); });


        ExitGame.onClick.AddListener(delegate ()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        });
    }

    
}
