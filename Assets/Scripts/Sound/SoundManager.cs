using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource, effectSource;

    public static SoundManager Instance;

    void Awake(){
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip audio){
        effectSource.PlayOneShot(audio);
    }

    public void PlayMusic(AudioClip audio){
        musicSource.clip = audio;
        musicSource.Play();
    }

    public void ChangeMasterVolume(float value){
        effectSource.volume = value;
    }

    public void ChangeMasterVolumeMusic(float value){
        musicSource.volume = value;
    }
}
