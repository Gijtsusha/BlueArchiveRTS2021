using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    public AudioMixer mixer;

    public void Start()
    {
        GameObject.Find("SetBGMVolume").GetComponentInChildren<Slider>().onValueChanged.AddListener(delegate (float value) 
        {
            GameObject.Find("AudioManager").GetComponent<SettingManager>().SetBGMVolume(value);
        });
        GameObject.Find("SetSFXVolume").GetComponentInChildren<Slider>().onValueChanged.AddListener(delegate (float value)
        {
            GameObject.Find("AudioManager").GetComponent<SettingManager>().SetSFXVolume(value);
        });
        GameObject.Find("SetUIVolume").GetComponentInChildren<Slider>().onValueChanged.AddListener(delegate (float value)
        {
            GameObject.Find("AudioManager").GetComponent<SettingManager>().SetUIVolume(value);
        });

    }
    public void SetBGMVolume(float value)
    {
        mixer.SetFloat("BGM", value);
    }

    public void SetSFXVolume(float value)
    {
        mixer.SetFloat("SFX", value);
    }

    public void SetUIVolume(float value)
    {
        mixer.SetFloat("UI", value);
    }
}
