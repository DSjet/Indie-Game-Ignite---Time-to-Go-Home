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

	public void SetHUD(Character character)
	{
		nameText.text = character.Char.CharName;
		levelText.text = "Lvl. " + character.Level;
		hpSlider.maxValue = character.MaxHP;
		hpSlider.value = character.HP;
	}

	public void SetHP(int hp)
	{
		hpSlider.value = hp;
	}
}
