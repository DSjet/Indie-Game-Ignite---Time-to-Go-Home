using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneEncounterSaver : MonoBehaviour
{
    public static int ticker = 0;
    public ChangeEncounterData[] datas;

    void Start(){
        if(ticker == 0){
            if(WinnerBattleData.Winner.charSO.CharName == "You"){
                ticker = 0;
                SceneManager.LoadScene("DetectionCH");
                TimeWorld.setupTimer();
                return;
            }else{
                datas[ticker++].changeData();
            }
        }else if(ticker == 1){
            if(WinnerBattleData.Winner.charSO.CharName == "You"){
                datas[ticker++].changeData();
            }else{
                SceneManager.LoadScene("LosingBattle");
                return;
            }
        }
        SceneManager.LoadScene("EncounterScene");
    }
}
