using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TextClip : PlayableAsset
{
    [TextArea(3,10)]
    public string text;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<TextBehavior>.Create(graph);

        TextBehavior textBehavior = playable.GetBehaviour();
        textBehavior.text = text;

        return playable;
    }
}
