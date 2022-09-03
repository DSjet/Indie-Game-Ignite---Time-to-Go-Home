using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeWorld : MonoBehaviour
{
    public static int TimeHour = 0;
    public static int TimeMinute = 0;
    public static int TimeSecond = 0;
    public static int TimeMiliSecond = 0;
    public static bool isTimePaused = true;
    private TextMeshProUGUI timerText;
    public static bool isShowed = false;

    void Awake(){
        timerText = GameObject.Find("TimerText").GetComponent<TextMeshProUGUI>();
    }

    public static void setupTimer(){
        TimeHour = 48;
        TimeMinute = 0;
        TimeSecond = 0;
        isShowed = true;
    }
    public static void startTimer(){
        isTimePaused = false;
    }

    public static void pauseTimer(){
        isTimePaused = true;
    }

    public void changeTimer(int seconds){
        int initialChanged = TimeHour*3600 + TimeMinute*60 + TimeSecond + seconds;
        TimeHour = Mathf.FloorToInt(initialChanged/3600);
        initialChanged -= TimeHour * 3600;
        TimeMinute = Mathf.FloorToInt(initialChanged/60);
        initialChanged -= TimeMinute * 60;
        TimeSecond = initialChanged;
    }

    void FixedUpdate(){
        if(!isTimePaused){
            if(--TimeMiliSecond < 0){
                TimeMiliSecond = 50;
                if(--TimeSecond < 0){
                    TimeSecond = 59;
                    if(--TimeMinute < 0){
                        TimeMinute = 59;
                        if(--TimeHour < 0){
                            GameObject.FindObjectOfType<MoveWorld>().MoveScene("GameOver");
                        }
                    }
                }
            }
        }
    }

    void Update(){
        if(isShowed)
            timerText.text = $"{TimeHour:00}:{TimeMinute:00}:{TimeSecond:00}";
    }
}
