using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


public class AudioManager : MonoBehaviour
{
    [System.Serializable]
    public class Sound
    {
        public AudioClip clip;

        public AudioMixerGroup outputGroup;

        [Range(0,1)]
        public float volume;

        public bool playOnAwake;

        public bool loop;
    }

    public List<Sound> sounds;
    private Dictionary<string, AudioSource> audiosDic;

    private void Awake()
    {
        audiosDic = new Dictionary<string, AudioSource>();
    }

    private void Start()
    {
        foreach(var sound in sounds)
        {
            GameObject obj = new GameObject(sound.clip.name);
            obj.transform.SetParent(transform);

            AudioSource source = obj.AddComponent<AudioSource>();
            source.clip = sound.clip;
            source.volume = sound.volume;
            source.playOnAwake = sound.playOnAwake;
            source.loop = sound.loop;
            source.outputAudioMixerGroup = sound.outputGroup;

            if (sound.playOnAwake)
            {
                source.Play();
            }

            audiosDic.Add(sound.clip.name, source);
        }

        DontDestroyOnLoad(this);
    }

}
