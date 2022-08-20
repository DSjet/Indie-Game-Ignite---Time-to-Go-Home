using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using TMPro;

public class TextTrackMixer : PlayableBehaviour
{

    private int index = 0;
    private float timer = 0;
    private string currText = "";
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        TextMeshProUGUI textWindow = playerData as TextMeshProUGUI;
        string text = "";

        if(!textWindow){
            return;
        }

        int inputCount = playable.GetInputCount();

        for(int i = 0; i < inputCount; i++){
            if(playable.GetInputWeight(i) > 0){
                ScriptPlayable<TextBehavior> inputPlayable = (ScriptPlayable<TextBehavior>)playable.GetInput(i);
                TextBehavior input = inputPlayable.GetBehaviour();
                if(input.text != currText){
                    currText = input.text;
                    index = 0;
                    timer = 0;
                }
                text = input.text;
            }
        }
        if(index <= text.Length){
            timer -= Time.deltaTime;
        }
        if(timer <= 0f){
            timer += 0.025f;
            index++;
        }

        if((text.Length != 0 || text != null) && index < text.Length) textWindow.text = text.Substring(0, index);
        else textWindow.text = text;
    }
}
