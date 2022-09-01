using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencyManager : MonoBehaviour
{
    public static int currentMoney = 0;
    public TMP_Text Text;

    void Start(){
        showCurrency();
    }
    
    public void changeCurrency(int value){
        currentMoney += value;
        showCurrency();
    }

    public void showCurrency(){
        Text.text = currentMoney.ToString();
    }
}
