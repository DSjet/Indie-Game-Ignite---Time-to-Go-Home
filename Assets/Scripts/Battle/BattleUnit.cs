using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnit : MonoBehaviour
{
    
    [SerializeField] bool isFriendlyUnit;
    [SerializeField] BattleHUD hud;
    
    public Character Char{ get; set; }
    public BattleHUD HUD { get{return hud;}}
    public bool IsFriendlyUnit {get{return isFriendlyUnit;}}

    public void Setup(Character character){
        Char = character;
        if (isFriendlyUnit){
            // friendly unit setup
        } else {
            // hostile unit setup
        }

        hud.SetHUD(character);
        // image.color = originalColor
        // Play some battle animation
    }

    public void PlayHitAnimation(){
        // play hit animation to desired target
    }

    public void PlayFaintAnimation(){
        // play faint animation to desired target
    }
}
