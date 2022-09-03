using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinnerBattleData : MonoBehaviour
{
    public static Character Winner;
    public static int savedTimeHour;
    public static int savedTimeMinute;
    public static int savedTimeSecond;
    public static int savedTimeMiliSecond;

    public static bool isRetrying = false;

    public void retry(){
        isRetrying = true;
    }
}
