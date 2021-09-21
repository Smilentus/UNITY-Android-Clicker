using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    #region Объявленные переменные в Юньке
    System.Random rnd = new System.Random();
    public GameObject neonStar;

    public GameObject infoPanel;

    public GameObject PlusText;
    public GameObject AchText;
    public Canvas mainCanvas;

    public Transform BackStars;

    [Header("Ссылки на скрипты")]
    public ButtonsScript bs;
    public SavingScript ss;

    [Header("Время до звездопада")]
    public Slider time;

    [Header("Полоска опыта")]
    public Slider expBar;

    [Header("Бонус учёных")]
    public GameObject scBonusBarObj;
    public Slider scBonusBar;
    public Text timeText;
    #endregion

    #region Переменные
    public static int Score = 0;
    public static float ExpHelper = 20f;
    public static float Exp = 0f;
    public static int Lvl = 1;
    public static float Shards = 0f;

    public static float[] chanceToFindExtraShards = { 0, 0, 0 };
    public static float galacticMoney = 0f;
    public static int extraMoney = 0;
    public static float extraMoneyFromChange = 0f;
    public static float exchangeMoneyTime = 10f;
    public static int extraMoneyFromChangeMulti = 2;

    public static int[] bots = new int[1];
    public static int scBonuses = 0;
    public static bool scBonusesUsed = false;
    public static float gainPower = 0.002f;
    public static float gainPowerBonus = 1;
    public static float gainAuto = 0.002f;
    public static float gainAutoSpeed = 5f;
    public static int gainAutoMulti = 1;
    public static float Mass = 3;
    public static int MassPow = 3;

    public static int rndAstMassPow = 5;
    public static int breakBonus = 4;
    public static float timeToStar = 120;
    public static float timeToStarMax = 120;
    public static float timeToBonus = ScientistsScript.scBonusTime;

    public static bool CanChoose = false;
    public static bool CanPress = true;
    public static bool achSpawned = false;
    #endregion

    // Нажатие и прочее
    public void CalculateChance()
    {
        int num = rnd.Next(0, 3);
        float locBonus = 0;
        float chance = rnd.Next(0, 101);
        switch (num)
        {
            case 0:
                if (chance <= chanceToFindExtraShards[0])
                {
                    locBonus = rnd.Next(100 * Lvl, 500 * Lvl);
                    Shards += locBonus;
                    AchievementScript.Progresses[0] += locBonus;
                    SpawnPlusText(locBonus, Color.red);
                }
                break;
            case 1:
                if (chance <= chanceToFindExtraShards[1])
                {
                    locBonus = rnd.Next(250 * Lvl, 2500 * Lvl);
                    Shards += locBonus;
                    AchievementScript.Progresses[0] += locBonus;
                    SpawnPlusText(locBonus, Color.red);
                }
                break;
            case 2:
                if (chance <= chanceToFindExtraShards[2])
                {
                    locBonus = rnd.Next(10000 * Lvl, 1000000 * Lvl);
                    Shards += locBonus;
                    AchievementScript.Progresses[0] += locBonus;
                    SpawnPlusText(locBonus, Color.red);
                }
                break;
        }
    }
    public void OnMouseDown()
    {
        if (CanPress)
        {
            float locBonus = 0;
            if (ScientistsScript.scUpgrades[2] > 0)
                CalculateChance();
            locBonus = gainPower * gainPowerBonus * Lvl;
            Shards += locBonus;
            Exp += 0.05f;
            AchievementScript.Progresses[0] += locBonus;
            Mass -= (gainPower * gainPowerBonus) / 2;
            SpawnPlusText(locBonus, Color.white);
        }
    }
    public void ChooseBonus(int bonus)
    {
        StartCoroutine(choosedBonus(bonus));
        bs.HideBonusInfo();
    }
    public void SpawnMeteor()
    {
        Exp += 5 * breakBonus;
        Shards += breakBonus * 10;
        galacticMoney += breakBonus * 4;
        Mass = Random.Range(1, 10);
        MassPow = rnd.Next(1, rndAstMassPow + 1);
        breakBonus = MassPow;
    }
    public void SpawnPlusText(float text, Color color)
    {
        if (CanPress)
        {
            PlusText.GetComponent<Text>().text = "+" + CurrencyConverter.instance.GetCurrency(text, "f3");
            if (color == Color.red)
                PlusText.GetComponent<Text>().color = Color.red;
            else if (color == Color.white)
                PlusText.GetComponent<Text>().color = Color.white;
            else if (color == Color.cyan)
                PlusText.GetComponent<Text>().color = Color.cyan;
            GameObject fix = Instantiate(PlusText, new Vector3(Random.Range(-160, 160), Random.Range(5, 45), 1), Quaternion.identity) as GameObject;
            fix.transform.SetParent(mainCanvas.transform, false);
        }
    }
    public void SpawnAchText(string text)
    {
        achSpawned = true;
        AchText.GetComponentsInChildren<Text>()[1].text = text;
        GameObject ach = Instantiate(AchText, new Vector3(600, 25, 0), Quaternion.identity) as GameObject;
        ach.transform.SetParent(mainCanvas.transform, false);
    }

    // Информационное окно
    public void ShowInfo(string Text)
    {
        CloseAllWindows();
        CanPress = false;
        infoPanel.SetActive(true);
        infoPanel.GetComponentInChildren<Text>().text = "Новая информация! \n" + Text;
    }
    public void HideInfo()
    {
        infoPanel.SetActive(false);
        CanPress = true;
    }
    public void CloseAllWindows()
    {
        bs.HideCentrePanel();
        bs.HideIconsInfo();
    }

    // Начало
    private void Start()
    {
        StartCoroutine(ScoreFromSpeed());
        StartCoroutine(SpawnBonusTimer());
        if (timeToBonus <= 0)
            timeToBonus = ScientistsScript.scBonusTime;
        if (bots[0] >= 1)
        {
            StartCoroutine(exchangeBotTimer());
        }
        if (scBonusesUsed)
        {
            scBonusBarObj.SetActive(true);
            StartCoroutine("scientistsBonusTimer");
        }
    }
    private void Update()
    {
        CheckAchievements();

        if (galacticMoney < 0) galacticMoney = 0;
        if (galacticMoney >= 1000000000000000000f)
        {
            for (int i = 0; i < (galacticMoney / 1000000000000000000f); i++)
            {
                extraMoney++;
                galacticMoney -= 1000000000000000000f;
            }
        }

        if (bots[0] == 1)
        {
            StartCoroutine(exchangeBotTimer());
            bots[0] = 2;
        }
        if (Exp >= ExpHelper)
        {
            Lvl++;
            AchievementScript.Progresses[4]++;
            Exp -= ExpHelper;
            ExpHelper += (ExpHelper / 2);
        }

        if (timeToBonus <= 0)
            timeToBonus = 0;
        if (timeToBonus > scBonusBar.maxValue)
        {
            scBonusBar.value = scBonusBar.maxValue;
            timeToBonus = scBonusBar.value;
        }

        expBar.maxValue = ExpHelper;
        expBar.value = Exp;

        scBonusBar.maxValue = ScientistsScript.scBonusTime;
        scBonusBar.value = timeToBonus;
        scBonusBar.GetComponentInChildren<Text>().text = "Бонус: " + timeToBonus.ToString("0");

        if (timeToStar > time.maxValue)
        {
            time.value = time.maxValue;
            timeToStar = time.value;
        }
        time.value = timeToStar;
        time.maxValue = timeToStarMax;
        timeText.text = "До звездопада: " + timeToStar.ToString("0");

        BackStars.Rotate(new Vector3(0, 0, 0.03f));

        if (scBonuses > 0 && scBonusesUsed == false)
        {
            scBonusesUsed = true;
            scBonusBarObj.SetActive(true);
            StartCoroutine("scientistsBonusTimer");
        }
        if (gainAutoSpeed <= 0.02f)
            gainAutoSpeed = 0.02f;
        if (Mass <= 0)
        {
            if (MassPow <= 0 && Mass <= 0)
            {
                Score++;
                SpawnMeteor();
            }
            else
            {
                while (Mass <= 0)
                {
                    Mass += 10;
                    MassPow--;
                    Exp += 2;
                }
            }
        }
        if (Mass >= 10)
        {
            Mass -= 10;
            MassPow++;
        }
    }

    public void CheckAchievements()
    {
        for (int i = 0; i < AchievementScript.Progresses.Length; i++)
        {
            if (AchievementScript.Progresses[i] >= (int)AchievementScript.maxProgresses[i])
            {
                if (achSpawned == false && AchievementScript.rewardShowed[i] == false)
                {
                    AchievementScript.rewardShowed[i] = true;
                    SpawnAchText(AchievementScript.Names[i]);
                }
            }
        }
    }
    public void CheckBonus()
    {
        CloseAllWindows();
        if (CanChoose)
            bs.ShowBonusInfo();
        else
            StartCoroutine("choosedBonusRandom");
    }

    // Таймеры
    IEnumerator ScoreFromSpeed()
    {
        while (true)
        {
            float locBonus = gainAuto * gainAutoMulti;
            Shards += locBonus;
            AchievementScript.Progresses[0] += locBonus;
            Mass -= (gainAuto * gainAutoMulti) / 2;
            SpawnPlusText(locBonus, Color.cyan);
            yield return new WaitForSeconds(gainAutoSpeed);
        }
    } // Автодобыча
    IEnumerator SpawnBonusTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (timeToStar <= 0)
            {
                gainPowerBonus += 5;
                for (int i = 0; i < rnd.Next(5, 17); i++)
                {
                    Instantiate(neonStar, new Vector3(Random.Range(-2, 2), 6, 10), Quaternion.identity);
                    yield return new WaitForSeconds(1);
                }
                yield return new WaitForSeconds(5);
                gainPowerBonus -= 5;
                timeToStar = timeToStarMax;
            }
            else
                timeToStar -= 1;
        }
    }  // Таймер звездопада
    IEnumerator exchangeBotTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(exchangeMoneyTime);
            bs.ChangeShardsButton();
        }
    } // Робот автообменщик
    IEnumerator scientistsBonusTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (timeToBonus <= 0)
            {
                StopCoroutine("scientistsBonusTimer");
                CheckBonus();
            }
            else
                timeToBonus -= 1f;
        }
    } // Таймер до бонуса с учёных
    IEnumerator choosedBonus(int bonus)
    {
        switch (bonus)
        {
            case 0:
                float loc = gainPower * 1000 * gainPowerBonus * Lvl * 100;
                Shards += loc;
                ShowInfo(("Учёные случайно устроили временной парадокс и переместились во времени, принеся из будущего немножечко осколков! \nПолучено: " + loc.ToString("f3") + " осколков."));
                if (AchievementScript.achLvl[3] == 3)
                    AchievementScript.Progresses[3] = 1;
                break;
            case 1:
                ShowInfo("Учёные случайно создали искусственную чёрную дыру, эффект был непредсказуемым, мощность добычи увеличилась на 30 секунд! \nЭффект: Мощность клика x30.");
                if (AchievementScript.achLvl[3] == 0)
                    AchievementScript.Progresses[3] = 1;
                gainPowerBonus += 30;
                yield return new WaitForSeconds(30);
                gainPowerBonus -= 30;
                ShowInfo("Чёрная дыра испарилась сама по себе, вот это да, учёные шокированы.");
                break;
            case 2:
                ShowInfo("Произошёл очень мощный выброс сперм..магнитного поля из лаборатории учёных, роботы начали усиленно работать! \nЭффект: Автодобыча x25, Автопродажа x15.");
                gainAutoMulti += 25;
                extraMoneyFromChangeMulti += 15;
                if (AchievementScript.achLvl[3] == 1)
                    AchievementScript.Progresses[3] = 1;
                yield return new WaitForSeconds(15);
                gainAutoMulti -= 25;
                extraMoneyFromChangeMulti -= 15;
                ShowInfo("Действие магнитного поля закончилось! :c");
                break;
            case 3:
                ShowInfo("Учёные придумали способ клонирования любых вещей! Удивительно, а сходить в туалет, не обоссав ободок, они так и не научились.!. \nЭффект: Удвоение осколков и монет.");
                Shards *= 2;
                galacticMoney *= 2;
                if (AchievementScript.achLvl[3] == 2)
                    AchievementScript.Progresses[3] = 1;
                break;
        }
        yield return new WaitForSeconds(5);
        timeToBonus = ScientistsScript.scBonusTime;
        StartCoroutine("scientistsBonusTimer");
        StopCoroutine("choosedBonus");
    } 
    IEnumerator choosedBonusRandom()
    {
        int rand = rnd.Next(0, 4);
        switch (rand)
        {
            case 0:
                float loc = gainPower * 1000 * gainPowerBonus * Lvl * 100;
                Shards += loc;
                ShowInfo(("Учёные случайно устроили временной парадокс и переместились во времени, принеся из будущего немножечко осколков! \nПолучено: " + loc.ToString("f3") + " осколков."));
                if (AchievementScript.achLvl[3] == 3)
                    AchievementScript.Progresses[3] = 1;
                break;
            case 1:
                ShowInfo("Учёные случайно создали искусственную чёрную дыру, эффект был непредсказуемым, мощность добычи увеличилась на 30 секунд! \nЭффект: Мощность клика x30.");
                if (AchievementScript.achLvl[3] == 0)
                    AchievementScript.Progresses[3] = 1;
                gainPowerBonus += 30;
                yield return new WaitForSeconds(30);
                gainPowerBonus -= 30;
                ShowInfo("Чёрная дыра испарилась сама по себе, вот это да, учёные шокированы.");
                break;
            case 2:
                ShowInfo("Произошёл очень мощный выброс сперм..магнитного поля из лаборатории учёных, роботы начали усиленно работать! \nЭффект: Автодобыча x25, Автопродажа x15.");
                gainAutoMulti += 25;
                extraMoneyFromChangeMulti += 15;
                if (AchievementScript.achLvl[3] == 1)
                    AchievementScript.Progresses[3] = 1;
                yield return new WaitForSeconds(15);
                gainAutoMulti -= 25;
                extraMoneyFromChangeMulti -= 15;
                ShowInfo("Действие магнитного поля закончилось! :c");
                break;
            case 3:
                ShowInfo("Учёные придумали способ клонирования любых вещей! Удивительно, а сходить в туалет, не обоссав ободок, они так и не научились.!. \nЭффект: Удвоение осколков и монет.");
                Shards *= 2;
                galacticMoney *= 2;
                if (AchievementScript.achLvl[3] == 2)
                    AchievementScript.Progresses[3] = 1;
                break;
        }
        yield return new WaitForSeconds(5);
        timeToBonus = ScientistsScript.scBonusTime;
        StartCoroutine("scientistsBonusTimer");
        StopCoroutine("choosedBonusRandom");
    }
}
