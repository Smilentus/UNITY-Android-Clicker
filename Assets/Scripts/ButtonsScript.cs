using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsScript : MonoBehaviour
{
    public GameObject[] Panels;
    public ShopScript ss;
    public Player pl;

    public Canvas mainCanvas;
    public GameObject plusText;

    public static bool CanOpen = true;

    private void Start()
    {
        CanOpen = true;
    }

    // Переключение между панелями/канвасами/сценами
    public void SwitchScene(int sceneNum)
    {
        switch (sceneNum)
        {
            case 0:
                SceneManager.LoadScene("GameScene");
                break;
        }
    }
    public void ShowHideCentrePanel()
    {
        Player.CanPress = !Player.CanPress;
        Panels[0].SetActive(!Panels[0].activeSelf);
    }
    // Побочные добавки начало
    public void ShowCentrePanel()
    {
        Player.CanPress = false;
        Panels[0].SetActive(true);
    }
    public void HideCentrePanel()
    {
        Player.CanPress = true;
        Panels[0].SetActive(false);
    } 
    // Побочные добавки конец
    public void ChangeShardsButton()
    {
        if ((int)Player.Shards > 0)
        {
            int rShards = (int)Player.Shards;
            Player.galacticMoney += rShards * (2 + Player.extraMoneyFromChange);
            Player.Shards -= rShards;
            AchievementScript.Progresses[2] += ((2 + Player.extraMoneyFromChange) * rShards);
            AchievementScript.Progresses[1] += rShards;
            SpawnPlusText(((2 + Player.extraMoneyFromChange) * rShards));
            Debug.Log("Shards: " + Player.Shards + "\nrShards: " + rShards);
        }
    }
    public void SpawnPlusText(float text)
    {
        plusText.GetComponentInChildren<Text>().text = "+" + CurrencyConverter.instance.GetCurrency(text, "f2");
        GameObject fix = Instantiate(plusText, new Vector3(-180, -565, -10), Quaternion.identity) as GameObject;
        fix.transform.SetParent(mainCanvas.transform, false);
    }

    // Открытие и закрытие панелей
    public void ShowIconsInfo(int num)
    {
        Debug.Log(num);
        Player.CanPress = false;
        Panels[5].SetActive(true);
        switch(num)
        {
            case 0: // Разрушенные астероиды
                Panels[5].GetComponentInChildren<Text>().text = "Количество разрушенных астероидов. \nБонусные монеты при разрушении: " + Player.breakBonus * 4 +
                    "\nБонусный опыт при разрушении: " + Player.breakBonus * 5 +
                    "\nБонусные осколки при разрушении: " + Player.breakBonus * 10;
                break;
            case 1: // Осколки
                Panels[5].GetComponentInChildren<Text>().text = "Количество осколков. Кликай и получишь больше!" +
                    "\nТекущая сила за клик: " + CurrencyConverter.instance.GetCurrency(Player.gainPower * Player.gainPowerBonus * Player.Lvl, "f3") +
                    "\nТекущий бонус умножения: " + Player.gainPowerBonus;
                break;
            case 2: // Деньги
                Panels[5].GetComponentInChildren<Text>().text = "Монеты, служащие обменной валютой. \nБонус при обмене: " + Player.extraMoneyFromChange.ToString("f1") + 
                    "\nБез конвертирования: " + Player.galacticMoney.ToString("0");
                break;
            case 3: // Масса
                Panels[5].GetComponentInChildren<Text>().text = "Масса текущего астероида. \nМаксимальный размер: 9.9*10^" + Player.rndAstMassPow;
                break;
            case 4: // Уровень
                Panels[5].GetComponentInChildren<Text>().text = "Уровень. Не придумал описания ;c";
                break;
            case 5: // Бонус учёных
                Panels[5].GetComponentInChildren<Text>().text = "Случайный бонус от физиков. \n[1] - Временной парадокс." +
                    "\n[2] - Чёрная дыра." + 
                    "\n[3] - Магнитный импульс." +
                    "\n[4] - Клонирование.";
                break;
            case 6: // Экстра мани
                Panels[5].GetComponentInChildren<Text>().text = "Сжатые монеты. \n1 сжатая монета = 1.000.000.000.000.000 монет. \nОбменивается автоматически.";
                break;
        }
    }
    public void HideIconsInfo()
    {
        Player.CanPress = true;
        Panels[5].SetActive(false);
    }

    public void ShowBonusInfo()
    {
        Player.CanPress = false;
        Panels[6].SetActive(true);
        Debug.Log("Можно ли нажимать? - " + Player.CanPress);
    }
    public void HideBonusInfo()
    {
        Player.CanPress = true;
        Panels[6].SetActive(false);
        Debug.Log("Можно ли нажимать? - " + Player.CanPress);
    }

    public void CloseAnotherPanels(int avoid)
    {
        for(int i = 1; i < Panels.Length; i++)
        {
            if (i == avoid)
                continue;
            Panels[i].SetActive(false);
        }
    }
    public void ShowHideShopPanel()
    {
        ss.UpdateCosts();
        ss.InitializeDescrs();
        Panels[1].SetActive(!Panels[1].activeSelf);
        CloseAnotherPanels(1);
    }
    public void ShowHidePeoplePanel()
    {
        Panels[2].SetActive(!Panels[2].activeSelf);
        CloseAnotherPanels(2);
    }
    public void ShowHideAchievementsPanel()
    {
        Panels[3].SetActive(!Panels[3].activeSelf);
        CloseAnotherPanels(3);
    }
    public void ShowHideSettingsPanel()
    {
        Panels[4].SetActive(!Panels[4].activeSelf);
        CloseAnotherPanels(4);
    }
}
