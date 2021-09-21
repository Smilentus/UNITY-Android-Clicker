using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{
    public GameObject infoPanel;
    public GameObject shopPanel;
    public GameObject[] buttons;
    public static string[] Descrs = new string[20];
    public static float[] costs = { 1, 1.5f, 0.5f, 5, 2, 7 };
    public static float[] pluscosts = { 1.5f, 2f, 0.7f, 2.75f, 2.25f, 3.5f};
    public static int[] upgrades = { 0, 0, 0, 0, 0, 0 };
    public static int[] amounts = { 1, 1, 1, 1, 1, 1 };
    public static bool Success = false;

    // Начало
    private void Start()
    {
        InitializeDescrs();
        UpdateCosts();
    }

    private void Update()
    {
        FixBuy();
    }

    public void InitializeDescrs()
    {
        Descrs[0] = ("Увеличивает скорость автодобычи астероидов. (-0.1 сек) " +
        "\nТекущая скорость: " + Player.gainAutoSpeed.ToString("0.00"));
        Descrs[1] = ("Повышает эффективность автодобычи. (+0.025) " +
            "\nТекущая сила: " + Player.gainAuto.ToString("0.000"));
        Descrs[2] = ("Увеличивает доход за клик. (+0.006) " +
                "\nТекущая сила: " + Player.gainPower.ToString("0.000"));
        Descrs[3] = ("Увеличивает размеры астероидов, улучшая бонус после разрушения. (*10^1) " +
              "\nТекущий макс. размер: 9*10^" + Player.rndAstMassPow);
        Descrs[4] = ("Обмен приносит больше золота. (+0.1)" + 
            "\nТекущий бонус: " + Player.extraMoneyFromChange.ToString("0.0"));
        Descrs[5] = ("Нанять робота: обменивает осколки по удвоенной цене(улучшается учёными) каждые 10 сек. (-0.1 сек)" +
            "Текущая скорость обмена: " + Player.exchangeMoneyTime.ToString("0.00"));
    }

    // Покупка и смена количества
    public void BuySomething(int buyableNum)
    {
        if(Player.galacticMoney < costs[buyableNum] && Player.extraMoney > 0)
        {
            Player.galacticMoney += 1000000000000000;
            Player.extraMoney--;
        }
        switch(buyableNum)
        {
            case 0:
                if(Player.galacticMoney >= updatedCost(0) && upgrades[0] != 40)
                {
                    Player.galacticMoney -= updatedCost(0);
                    costs[0] = updatedCost(0) + pluscosts[0];
                    upgrades[0] += 1 * amounts[0];
                    Player.gainAutoSpeed -= 0.1f * amounts[0];
                    Success = true;
                }
                break;
            case 1:
                if(Player.galacticMoney >= updatedCost(1))
                {
                    Player.galacticMoney -= updatedCost(1);
                    costs[1] = updatedCost(1) + pluscosts[1];
                    upgrades[1] += 1 * amounts[1];
                    Player.gainAuto += 0.025f * amounts[1];
                    Success = true;
                }
                break;
            case 2:
                if(Player.galacticMoney >= updatedCost(2))
                {
                    Player.galacticMoney -= updatedCost(2);
                    costs[2] = updatedCost(2) + pluscosts[2];
                    upgrades[2] += 1 * amounts[2];
                    Player.gainPower += 0.006f * amounts[2];
                    Success = true;
                }
                break;
            case 3:
                if(Player.galacticMoney >= updatedCost(3))
                {
                    Player.galacticMoney -= updatedCost(3);
                    costs[3] = updatedCost(3) + pluscosts[3];
                    upgrades[3] += 1 * amounts[3];
                    Player.rndAstMassPow += 1 * amounts[3]; 
                    Success = true;
                }
                break;
            case 4:
                if(Player.galacticMoney >= updatedCost(4))
                {
                    Player.galacticMoney -= updatedCost(4);
                    costs[4] = updatedCost(4) + pluscosts[4];
                    upgrades[4] += 1 * amounts[4];
                    Player.extraMoneyFromChange += 0.1f * amounts[4];
                    Success = true;
                }
                break;
            case 5:
                if(Player.galacticMoney >= updatedCost(5) && upgrades[5] != 50)
                {
                    if(Player.bots[0] == 0)
                    {
                        Player.bots[0] = 1;
                    }
                    Player.galacticMoney -= updatedCost(5);
                    costs[5] = updatedCost(5) + pluscosts[5];
                    upgrades[5] += 1 * amounts[5];
                    Player.exchangeMoneyTime -= 0.1f * amounts[5];
                    Success = true;
                }
                break;
        }
        //if (Success == false)
        //    ShowInfo("Недостаточно монет для покупки! :c");
        Success = false;
        InitializeDescrs();
        UpdateCosts();
        FixBuy();
    }
    public void SwitchAmount(int buttonID)
    {
        switch(buttonID)
        {
            case 0:
                if (amounts[0] == 1)
                    amounts[0] = 5;
                else if (amounts[0] == 5)
                    amounts[0] = 15;
                else if (amounts[0] == 15)
                    amounts[0] = 40;
                else if (amounts[0] == 40)
                    amounts[0] = 1;
                break;
            case 1:
                if (amounts[1] == 1)
                    amounts[1] = 10;
                else if (amounts[1] == 10)
                    amounts[1] = 25;
                else if (amounts[1] == 25)
                    amounts[1] = 50;
                else if (amounts[1] == 50)
                    amounts[1] = 100;
                else if (amounts[1] == 100)
                    amounts[1] = 1;
                break;
            case 2:
                if (amounts[2] == 1)
                    amounts[2] = 10;
                else if (amounts[2] == 10)
                    amounts[2] = 25;
                else if (amounts[2] == 25)
                    amounts[2] = 50;
                else if (amounts[2] == 50)
                    amounts[2] = 100;
                else if (amounts[2] == 100)
                    amounts[2] = 1;
                break;
            case 3:
                if (amounts[3] == 1)
                    amounts[3] = 10;
                else if (amounts[3] == 10)
                    amounts[3] = 25;
                else if (amounts[3] == 25)
                    amounts[3] = 50;
                else if (amounts[3] == 50)
                    amounts[3] = 100;
                else if (amounts[3] == 100)
                    amounts[3] = 1;
                break;
            case 4:
                if (amounts[4] == 1)
                    amounts[4] = 10;
                else if (amounts[4] == 10)
                    amounts[4] = 25;
                else if (amounts[4] == 25)
                    amounts[4] = 50;
                else if (amounts[4] == 50)
                    amounts[4] = 100;
                else if (amounts[4] == 100)
                    amounts[4] = 1;
                break;
            case 5:
                if (amounts[5] == 1)
                    amounts[5] = 10;
                else if (amounts[5] == 10)
                    amounts[5] = 25;
                else if (amounts[5] == 25)
                    amounts[5] = 50;
                else if (amounts[5] == 50)
                    amounts[5] = 1;
                break;
        }
        UpdateCosts();
        FixBuy();
    }

    // Множество функций для улучшений покупки (нет) 
    public void FixBuy()
    {
        if (upgrades[0] >= 40)
        {
            updatedCost(0);
            upgrades[0] = 40;
            Player.gainAutoSpeed = 1f;
            buttons[0].GetComponentsInChildren<Text>()[0].text = "MAX";
            buttons[0].GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = "Куплено";
        }
        if (upgrades[5] >= 50)
        {
            updatedCost(5);
            upgrades[5] = 50;
            Player.exchangeMoneyTime = 5f;
            buttons[5].GetComponentsInChildren<Text>()[0].text = "MAX";
            buttons[5].GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = "Куплено";
        }
    }
    public float updatedCost(int ID)
    {
        float loccost = 0;
        float oldcost = costs[ID];
        float savecost = costs[ID];
        if (amounts[ID] > 1)
        {
            for (int i = 0; i < amounts[ID] - 1; i++)
            {
                oldcost = costs[ID] += pluscosts[ID];
                loccost += oldcost;
            }
            costs[ID] = savecost;
            loccost += costs[ID];
            return loccost;
        }
        else
        {
            costs[ID] = savecost;
            loccost = costs[ID];
            return loccost;
        }
    }
    public void UpdateCosts()
    {
        FixBuy();
        for (int i = 0; i < buttons.Length; i++)
        { 
            buttons[i].GetComponentsInChildren<Text>()[0].text = CurrencyConverter.instance.GetCurrency(updatedCost(i), "f2");
            buttons[i].GetComponentsInChildren<Text>()[1].text = upgrades[i].ToString();
            buttons[i].GetComponentsInChildren<Text>()[2].text = Descrs[i];
            buttons[i].GetComponentsInChildren<Button>()[1].GetComponentInChildren<Text>().text = amounts[i].ToString();
        }
        
    }
    public void ShowInfo(string text)
    {
        infoPanel.SetActive(true);
        shopPanel.SetActive(false);
        infoPanel.GetComponentInChildren<Text>().text = text;
    }
    public void HideInfo()
    {
        infoPanel.SetActive(false);
        Player.CanPress = true;
    }
}
