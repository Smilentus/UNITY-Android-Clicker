using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public Text[] TextMass;

    // Отрисовка всех текстов, почти всех
    private void Update()
    {
        TextMass[0].text = CurrencyConverter.instance.GetShardsValue(Player.Shards);
        TextMass[1].text = Player.Score.ToString();
        TextMass[2].text = CurrencyConverter.instance.GetCurrency(Player.galacticMoney, "f2");
        if (Player.MassPow <= 0)
            TextMass[3].text = Player.Mass.ToString("0.000");
        else
            TextMass[3].text = Player.Mass.ToString("0.000") + "*10^" + Player.MassPow;
        TextMass[4].text = ("1 = " + (2 + Player.extraMoneyFromChange).ToString("f1"));
        TextMass[5].text = Player.Lvl.ToString();
        TextMass[6].text = CurrencyConverter.instance.GetCurrency(Player.extraMoney, "0");
    }
}
