using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScientistsScript : MonoBehaviour
{
    public GameObject[] Buttons;
    public static string[] scDescrs = new string[20];
    public static float[] scCosts = { 10000, 50000, 250000, 500000, 1000000, 1000000 }; 
    public static float[] scPlusCosts = { 7500, 22500, 150000, 250000, 750000, 0 }; 
    public static int[] scUpgrades = { 0, 0, 0, 0, 0, 0 }; 
    public static int[] scMaxUpgrades = { 5, 5, 5, 10, 0, 0 };
    public static int scBonusTime = 300;

    private void Start()
    {
        InitializeDescrs();
        UpdateButtons();
    }

    public void BuyScientist(int ID)
    {
        if(Player.galacticMoney >= scCosts[ID] && scUpgrades[ID] != scMaxUpgrades[ID])
        {
            Player.galacticMoney -= scCosts[ID];
            scCosts[ID] += scPlusCosts[ID];
            scUpgrades[ID]++;
            switch(ID)
            {
                case 0:
                    if (scUpgrades[0] == 5)
                        scBonusTime -= 75;
                    else if (scUpgrades[0] == 8)
                        scBonusTime -= 75;
                    else if (scUpgrades[0] == 11)
                        scBonusTime -= 25;
                    else if (scUpgrades[0] == 14)
                        scBonusTime -= 25;
                    if(Player.scBonuses < 4)
                        Player.scBonuses++;
                    break;
                case 1:
                    Player.gainAutoMulti++;
                    Player.extraMoneyFromChangeMulti++;
                    break;
                case 2:
                    Player.chanceToFindExtraShards[0] += 1.5f;
                    Player.chanceToFindExtraShards[1] += 0.5f;
                    Player.chanceToFindExtraShards[2] += 0.1f;
                    break;
                case 3:
                    for (int i = 0; i < scMaxUpgrades.Length; i++)
                    {
                        if (i == 3 || i == 5)
                            continue;
                        else
                            scMaxUpgrades[i]++;
                        if (scUpgrades[3] >= 10)
                            scMaxUpgrades[5] = 1;
                    }
                    break;
                case 4:
                    Player.timeToStarMax -= 10;
                    break;
                case 5:
                    Player.CanChoose = true;
                    break;
            }
        }
        InitializeDescrs();
        UpdateButtons();
    } // Покупка учёного

    public void InitializeDescrs()
    {   
        scDescrs[0] = "Физики занимаются разработкой новых технологий и улучшением старых. \n(Случайный бонус каждые " + scBonusTime + " сек.)";
        scDescrs[1] = "Инженеры создают и улучшают роботов, придавая им новые возможности \n(Множитель обмена +1; Множитель добычи +1)";
        scDescrs[2] = "Астрономы изучают новые астероиды, увеличивая шанс добычи редких руд и кристаллов \n(Шанс найти редкие минералы) \nТекущие шансы: \n" + Player.chanceToFindExtraShards[0].ToString("f2") + " | " + Player.chanceToFindExtraShards[1].ToString("f2") + " | " + Player.chanceToFindExtraShards[2].ToString("f2");
        scDescrs[3] = "Биологи изучают технологии проживания учёных, позволяя им жить более комфортно, улучшая их работоспособность \n(Больше учёных)";
        scDescrs[4] = "Инопланетяне изучают технологии искажения времени. (Уменьшение времени звездопада)";
        scDescrs[5] = "Физики-ядерщики улучшают изучения физиков. (Выбор эффекта бонуса)";
    } // Объявление описаний
    public void UpdateButtons()
    {
        for (int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i].GetComponentsInChildren<Text>()[1].text = scDescrs[i];
            Buttons[i].GetComponentsInChildren<Text>()[2].text = CurrencyConverter.instance.GetCurrency(scCosts[i], "f2");
            Buttons[i].GetComponentsInChildren<Text>()[3].text = scUpgrades[i] + "/" + scMaxUpgrades[i];
        }
    } // Обновление панелей покупки
}
