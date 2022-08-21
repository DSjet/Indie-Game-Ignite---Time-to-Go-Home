using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void ChangeMasterVolume(float value){
        AudioListener.volume = value;
    }
}
