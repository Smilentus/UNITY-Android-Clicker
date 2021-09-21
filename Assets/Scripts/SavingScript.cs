using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class SavingScript : MonoBehaviour
{
    [System.Serializable]
    public class Saver
    {
        // Переменные типа INT 
        public int Score;
        public int MassPow;
        public int rndAstMassPow;
        public int breakBonus;
        public int rndAstMassPowMin;
        public int Lvl;
        public int scBonuses;
        public int extraMoneyMulti;
        public int autoGainMulti;
        public int scBonusTime;
        public int extraMoney;

        // Переменные типа FLOAT
        public float Shards;
        public float gainPower;
        public float gainAuto;
        public float Mass;
        public float timeToBonus;
        public float extraMoneyFromChange;
        public float exchangeMoneyTime;
        public float gainPowerBonus;
        public float Exp;
        public float ExpHelper;
        public float galacticMoney;
        public float timeToStar;
        public float rndAstMassMin;
        public float scoreSpeed;
        public float rndAstMass;

        // Переменные типа STRING
        public string ShardsMod;

        // Переменные типа BOOl
        public bool scBonusesUsed;
        public bool achSpawned;
        public bool CanChoose;

        // Массивы типа INT
        public int[] upgrades;
        public int[] bots;
        public int[] amounts;
        public int[] scUpgrades;
        public int[] scMaxUpgrades;
        public int[] maxProgresses;
        public int[] Rewards;
        public int[] Date = new int[6];
        public int[] achLvl = new int[10];
        public int[] achMaxLvl = new int[10];

        // Массивы типа FLOAT
        public float[] Progresses;
        public float[] scCosts;
        public float[] scPlusCosts;
        public float[] costs;
        public float[] plusCosts;
        public float[] chanceToFindExtraShards;

        // Массивы типа BOOL
        public bool[] rewardShowed;
        public bool[] rewardTaken;

        public string[] achNames;
        public string[] achDescrs;

    } // Класс сохранения
    public static bool Save = false;

    public GameObject offlinePanel;

    public void SaveGame()
    {
        Saver save = new Saver();
        PlayerPrefs.SetInt("Saved", 1);
        // Сохранения игрока
        save.Score = Player.Score;
        save.Shards = Player.Shards;
        save.gainPower = Player.gainPower;
        save.gainAuto = Player.gainAuto;
        save.Mass = Player.Mass;
        save.MassPow = Player.MassPow;
        save.scoreSpeed = Player.gainAutoSpeed;
        save.rndAstMassPow = Player.rndAstMassPow;
        save.breakBonus = Player.breakBonus;
        save.galacticMoney = Player.galacticMoney;
        save.extraMoney = Player.extraMoney;
        save.timeToStar = Player.timeToStar;
        save.exchangeMoneyTime = Player.exchangeMoneyTime;
        save.extraMoneyFromChange = Player.extraMoneyFromChange;
        save.bots = Player.bots;
        save.gainPowerBonus = Player.gainPowerBonus;
        save.Exp = Player.Exp;
        save.ExpHelper = Player.ExpHelper;
        save.Lvl = Player.Lvl;
        save.scBonuses = Player.scBonuses;
        save.scBonusesUsed = Player.scBonusesUsed;
        save.timeToBonus = Player.timeToBonus;
        save.autoGainMulti = Player.gainAutoMulti;
        save.extraMoneyMulti = Player.extraMoneyFromChangeMulti;
        save.chanceToFindExtraShards = Player.chanceToFindExtraShards;
        save.achSpawned = Player.achSpawned;
        save.CanChoose = Player.CanChoose;

        // Массив времени
        save.Date[0] = DateTime.Now.Year;
        save.Date[1] = DateTime.Now.Month;
        save.Date[2] = DateTime.Now.Day;
        save.Date[3] = DateTime.Now.Hour;
        save.Date[4] = DateTime.Now.Minute;
        save.Date[5] = DateTime.Now.Second;

        // Сохранения магазина
        save.costs = ShopScript.costs;
        save.upgrades = ShopScript.upgrades;
        save.plusCosts = ShopScript.pluscosts;
        save.amounts = ShopScript.amounts;

        // Сохранения учёных
        save.scCosts = ScientistsScript.scCosts;
        save.scMaxUpgrades = ScientistsScript.scMaxUpgrades;
        save.scUpgrades = ScientistsScript.scUpgrades;
        save.scPlusCosts = ScientistsScript.scPlusCosts;
        save.scBonusTime = ScientistsScript.scBonusTime;

        // Сохранения достижений
        save.Rewards = AchievementScript.Rewards;
        save.rewardShowed = AchievementScript.rewardShowed;
        save.Progresses = AchievementScript.Progresses;
        save.maxProgresses = AchievementScript.maxProgresses;
        save.rewardTaken = AchievementScript.rewardTaken;
        save.achNames = AchievementScript.Names;
        save.achDescrs = AchievementScript.Descrs;
        save.achLvl = AchievementScript.achLvl;
        save.achMaxLvl = AchievementScript.achMaxLvl;

        if (!Directory.Exists(Application.persistentDataPath + "/files"))
            Directory.CreateDirectory(Application.persistentDataPath + "/files");
        FileStream fs = new FileStream(Application.persistentDataPath + "/files/save1.sv", FileMode.Create);
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(fs, save);
        fs.Close();

        Debug.Log("Сохранено");
    } // Сохранение
    public void LoadGame()
    {
        if (PlayerPrefs.GetInt("Saved") == 1)
        {
            if (File.Exists(Application.persistentDataPath + "/files/save1.sv"))
            {
                FileStream fs = new FileStream(Application.persistentDataPath + "/files/save1.sv", FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                try
                {
                    Saver save = (Saver)formatter.Deserialize(fs);
                    
                    // Сохранения игрока
                    Player.Score = save.Score;
                    Player.Shards = save.Shards;
                    Player.gainPower = save.gainPower;
                    Player.gainAuto = save.gainAuto;
                    Player.Mass = save.Mass;
                    Player.MassPow = save.MassPow;
                    Player.gainAutoSpeed = save.scoreSpeed;
                    Player.rndAstMassPow = save.rndAstMassPow;
                    Player.breakBonus = save.breakBonus;
                    Player.galacticMoney = save.galacticMoney;
                    Player.extraMoney = save.extraMoney;
                    Player.timeToStar = save.timeToStar;
                    Player.exchangeMoneyTime = save.exchangeMoneyTime;
                    Player.extraMoneyFromChange = save.extraMoneyFromChange;
                    Player.bots = save.bots;
                    Player.gainPowerBonus = save.gainPowerBonus;
                    Player.Exp = save.Exp;
                    Player.ExpHelper = save.ExpHelper;
                    Player.Lvl = save.Lvl;
                    Player.scBonuses = save.scBonuses;
                    Player.scBonusesUsed = save.scBonusesUsed;
                    Player.timeToBonus = save.timeToBonus;
                    Player.gainAutoMulti = save.autoGainMulti;
                    Player.extraMoneyFromChangeMulti = save.extraMoneyMulti;
                    Player.chanceToFindExtraShards = save.chanceToFindExtraShards;
                    Player.CanChoose = save.CanChoose;
                    Player.achSpawned = save.achSpawned;

                    // Сохранения магазина
                    ShopScript.costs = save.costs;
                    ShopScript.upgrades = save.upgrades;
                    ShopScript.pluscosts = save.plusCosts;
                    ShopScript.amounts = save.amounts;

                    // Сохранения учёных
                    ScientistsScript.scCosts = save.scCosts;
                    ScientistsScript.scMaxUpgrades = save.scMaxUpgrades;
                    ScientistsScript.scUpgrades = save.scUpgrades;
                    ScientistsScript.scPlusCosts = save.scPlusCosts;
                    ScientistsScript.scBonusTime = save.scBonusTime;

                    // Сохранения достижений
                    AchievementScript.rewardShowed = save.rewardShowed;
                    AchievementScript.Rewards = save.Rewards;
                    AchievementScript.Progresses = save.Progresses;
                    AchievementScript.maxProgresses = save.maxProgresses;
                    AchievementScript.rewardTaken = save.rewardTaken;
                    AchievementScript.Names = save.achNames;
                    AchievementScript.Descrs = save.achDescrs;
                    AchievementScript.achLvl = save.achLvl;
                    AchievementScript.achMaxLvl = save.achMaxLvl;

                    DateTime dt = new DateTime(save.Date[0], save.Date[1], save.Date[2], save.Date[3], save.Date[4], save.Date[5]);
                    TimeSpan ts = DateTime.Now - dt;
                    float offlineTotal = (float)ts.TotalSeconds * ((Player.gainAuto * Player.gainAutoMulti) / Player.gainAutoSpeed);
                    Player.Shards += offlineTotal;
                    offlinePanel.SetActive(true);
                    offlinePanel.GetComponentInChildren<Text>().text = "Вы отстутствовали: " + ts.Hours + " ч. " + ts.Minutes + " мин. " + ts.Seconds + " сек. \nДобыто: " + offlineTotal.ToString("f3");
                }
                catch (System.Exception e)
                {
                    Debug.Log(e.Message);
                }
                finally
                {
                    fs.Close();
                }
                Debug.Log("Загружено");
            }
        }
    } // Загрузка
    public void DeletedRestart()
    {
        Saver save = new Saver();
        save.Score = DefaultSettings.Score;
        save.Shards = DefaultSettings.Shards;
        save.ShardsMod = "A";
        save.gainPower = DefaultSettings.gainPower;
        save.gainAuto = DefaultSettings.gainAuto;
        save.Mass = DefaultSettings.Mass;
        save.MassPow = DefaultSettings.MassPow;
        save.costs = new float[] { 1, 1.5f, 0.5f, 5, 2, 7 };
        save.upgrades = new int[] { 0, 0, 0, 0, 0, 0 };
        save.plusCosts = new float[] { 1.5f, 2f, 0.7f, 2.75f, 2.25f, 3.5f };
        save.amounts = new int[] { 1, 1, 1, 1, 1, 1 };
        save.scoreSpeed = 5;
        save.rndAstMassPow = DefaultSettings.rndAstMassPow;
        save.breakBonus = DefaultSettings.breakBonus;
        save.galacticMoney = DefaultSettings.galacticMoney;
        save.extraMoney = DefaultSettings.extraMoney;
        save.timeToStar = DefaultSettings.timeToStar;
        save.rndAstMassPowMin = 1;
        save.rndAstMassMin = 1;
        save.extraMoneyFromChange = DefaultSettings.extraMoneyFromChange;
        save.exchangeMoneyTime = DefaultSettings.exchangeMoneyTime;
        save.bots = new int[]{ 0 };
        save.gainPowerBonus = DefaultSettings.gainPowerBonus;
        save.Exp = DefaultSettings.Exp;
        save.ExpHelper = DefaultSettings.ExpHelper;
        save.Lvl = DefaultSettings.Lvl;

        save.scBonuses = DefaultSettings.scBonuses;
        save.scBonusesUsed = DefaultSettings.scBonusesUsed;
        save.timeToBonus = DefaultSettings.timeToBonus;
        save.scCosts = new float[] { 10000, 50000, 250000, 500000, 1000000, 1000000 };
        save.scMaxUpgrades = new int[] { 5, 5, 5, 10, 0, 0 };
        save.scUpgrades = new int[] { 0, 0, 0, 0, 0, 0 };
        save.scPlusCosts = new float[] { 7500, 22500, 150000, 250000, 750000, 0 };

        save.extraMoneyMulti = DefaultSettings.extraMoneyFromChangeMulti;
        save.autoGainMulti = DefaultSettings.gainAutoMulti;
        save.chanceToFindExtraShards = new float[] { 0, 0, 0 };
        save.CanChoose = false;

        save.achSpawned = DefaultSettings.achSpawned;
        save.Progresses = new float[]{ 0, 0, 0, 0, 1 };
        save.maxProgresses = new int[] { 1, 100, 1000, 1, 10 };
        save.Rewards = new int[] { 100, 500, 2000, 1000000, 1000000 };
        save.rewardShowed = new bool[] { false, false, false, false, false };
        save.rewardTaken = new bool[] { false, false, false, false, false };
        save.achLvl = new int[]{ 0, 0, 0, 0, 0 };
        save.achMaxLvl = new int[]{ 5, 5, 5, 5, 5};
        save.achNames = new string[10];
        save.achDescrs = new string[10];

        save.scBonusTime = ScientistsScript.scBonusTime;

        for (int i = 0; i < save.Date.Length; i++)
            save.Date[i] = 0;

        PlayerPrefs.SetInt("Saved", 1);

        if (!Directory.Exists(Application.persistentDataPath + "/files"))
            Directory.CreateDirectory(Application.persistentDataPath + "/files");
        FileStream fs = new FileStream(Application.persistentDataPath + "/files/save1.sv", FileMode.Create);
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(fs, save);
        fs.Close();

        LoadGame();
    } // Перезапуск сохранения

    private void Update()
    {
        if(offlinePanel.activeSelf == true)
            offlinePanel.transform.SetAsLastSibling();
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Home) || Input.GetKeyDown(KeyCode.Menu))
            {
                SaveGame();
                Application.Quit();
            }
        }
    }

    // Цикл сохранений
    private void OnApplicationQuit()
    {
        SaveGame();
    }
    private void OnApplicationPause(bool pause)
    {
        if (pause)
            SaveGame();
    }
    void Awake()
    {
        LoadGame();
        StartCoroutine(AutoSave(7));
    }
    IEnumerator AutoSave(int seconds)
    {
        while (true)
        {
            yield return new WaitForSeconds(seconds);
            SaveGame();
        }
    }
}

