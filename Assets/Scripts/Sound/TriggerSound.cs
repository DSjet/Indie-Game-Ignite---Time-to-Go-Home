using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSound : MonoBehaviour
{
    public void trigger(AudioClip audio){
        SoundManager.Instance.PlaySound(audio);
    }
}
