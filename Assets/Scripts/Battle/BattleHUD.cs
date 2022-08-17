using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHUD : MonoBehaviour
{
    public TMP_Text nameText;
	public TMP_Text levelText;
	public Slider hpSlider;

	public void SetHUD(StatsManager stats)
	{
		nameText.text = stats.CharName;
		levelText.text = "Lvl. " + stats.CharLevel;
		hpSlider.maxValue = stats.MaxHP;
		hpSlider.value = stats.CurrentHP;
	}

	public void SetHP(int hp)
	{
		hpSlider.value = hp;
	}
}
