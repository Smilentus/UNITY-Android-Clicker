using UnityEngine;
using UnityEngine.UI;

public class AchievementScript : MonoBehaviour
{
    public GameObject[] achBases;
    [Header("Ачивки")]
    public Texture[] sprites;
    public static string[] Names = new string[20];
    public static string[] Descrs = new string[20];
    public static bool[] rewardShowed = { false, false, false, false, false };
    public static bool[] rewardTaken = { false, false, false, false, false};
    public static int[] Rewards = { 100, 500, 2000, 1000000, 1000000 };
    public static int[] achLvl = { 0, 0, 0, 0, 0 };
    public static int[] achMaxLvl = { 5, 5, 5, 5, 5 };
    public static float[] Progresses = { 0, 0, 0, 0, 1 };
    public static int[] maxProgresses = { 1, 100, 1000, 1, 10 };

    private void Start()
    {
        InitializeNames();
        InitializeDescrs();
        InitializeImages();
        InitializeAchievements();
    }
    private void Update()
    {
        UpdateProgresses();
    }

    // Награда
    public void TakeReward(int id)
    {
        if (Progresses[id] >= maxProgresses[id] && achBases[id].GetComponentInChildren<Button>().GetComponentInChildren<Text>().text != "Получено")
        {
            Player.galacticMoney += Rewards[id];
            Progresses[2] += Rewards[id];
            rewardTaken[id] = true;
            UpdateAchievement(id);
            UpdateProgresses();
            InitializeAchievements();
            Debug.Log(id);
        }
    }

    // Обновление прогресса 
    public void UpdateProgresses()
    {
        for (int i = 0; i < achBases.Length; i++)
        {
            if(Progresses[i] >= maxProgresses[i])
            {
                achBases[i].GetComponentInChildren<Button>().GetComponentInChildren<Image>().color = Color.yellow;
            }
            achBases[i].GetComponentInChildren<Slider>().maxValue = maxProgresses[i];
            achBases[i].GetComponentInChildren<Slider>().value = Progresses[i];
            achBases[i].GetComponentInChildren<Slider>().GetComponentInChildren<Text>().text = CurrencyConverter.instance.GetCurrency(Progresses[i], "0") + "/" + CurrencyConverter.instance.GetCurrency(maxProgresses[i], "0");

            if (achLvl[i] == achMaxLvl[i])
            {
                achBases[i].GetComponentInChildren<Button>().GetComponentInChildren<Image>().color = Color.yellow;
                achBases[i].GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = "Получено";
            }
        }
        if (Progresses[0] != 1)
            achBases[0].GetComponentInChildren<Slider>().GetComponentInChildren<Text>().text = CurrencyConverter.instance.GetCurrency(Progresses[0], "f3") + "/" + CurrencyConverter.instance.GetCurrency(maxProgresses[0], "0");
    }
    public void UpdateAchievement(int id)
    {
        if (achLvl[id] != achMaxLvl[id])
            achLvl[id]++;
        if (achLvl[id] == 1)
        {
            switch (id)
            {
                case 0:
                    Names[0] = "Шахтёр";
                    Descrs[0] = "Добудьте 100 осколков.";
                    rewardShowed[0] = false;
                    rewardTaken[0] = false;
                    Rewards[0] = 1000;
                    maxProgresses[0] = 100;
                    achBases[0].GetComponentsInChildren<RawImage>()[1].texture = sprites[0];
                    break;
                case 1:
                    Names[1] = "Продвинутый трейдер";
                    Descrs[1] = "Обменяйте 1000 осколков.";
                    rewardShowed[1] = false;
                    rewardTaken[1] = false;
                    Rewards[1] = 5000;
                    maxProgresses[1] = 1000;
                    achBases[1].GetComponentsInChildren<RawImage>()[1].texture = sprites[4];
                    break;
                case 2:
                    Names[2] = "Юный миллионер";
                    Descrs[2] = "Заработайте свой первый миллион.";
                    rewardShowed[2] = false;
                    rewardTaken[2] = false;
                    Rewards[2] = 500000;
                    maxProgresses[2] = 1000000;
                    achBases[2].GetComponentsInChildren<RawImage>()[1].texture = sprites[8];
                    break;
                case 3:
                    Names[3] = "\"Короткое замыкание\"";
                    Descrs[3] = "Почувствуйте мощь магнитного поля!";
                    rewardShowed[3] = false;
                    rewardTaken[3] = false;
                    Rewards[3] = 2500000;
                    Progresses[3] = 0;
                    maxProgresses[3] = 1;
                    achBases[3].GetComponentsInChildren<RawImage>()[1].texture = sprites[12];
                    break;
                case 4:
                    Names[4] = "Путь мудреца";
                    Descrs[4] = "Получите 25 уровень.";
                    rewardShowed[4] = false;
                    rewardTaken[4] = false;
                    Rewards[4] = 25000000;
                    maxProgresses[4] = 25;
                    achBases[4].GetComponentsInChildren<RawImage>()[1].texture = sprites[16];
                    break;
            }
        }
        else if (achLvl[id] == 2)
        {
            switch (id)
            {
                case 0:
                    Names[0] = "Копатель";
                    Descrs[0] = "Добудьте 10.000 осколков.";
                    rewardShowed[0] = false;
                    rewardTaken[0] = false;
                    Rewards[0] = 75000;
                    maxProgresses[0] = 10000;
                    achBases[0].GetComponentsInChildren<RawImage>()[1].texture = sprites[1];
                    break;
                case 1:
                    Names[1] = "Трейдер от бога";
                    Descrs[1] = "Обменяйте 1.000.000 осколков.";
                    rewardShowed[1] = false;
                    rewardTaken[1] = false;
                    Rewards[1] = 5000000;
                    maxProgresses[1] = 1000000;
                    achBases[1].GetComponentsInChildren<RawImage>()[1].texture = sprites[5];
                    break;
                case 2:
                    Names[2] = "Богатый папик";
                    Descrs[2] = "Заработайте 5.000.000";
                    rewardShowed[2] = false;
                    rewardTaken[2] = false;
                    Rewards[2] = 10000000;
                    maxProgresses[2] = 5000000;
                    achBases[2].GetComponentsInChildren<RawImage>()[1].texture = sprites[9];
                    break;
                case 3:
                    Names[3] = "Double trouble";
                    Descrs[3] = "Испытайте клонирующий эффект.";
                    rewardShowed[3] = false;
                    rewardTaken[3] = false;
                    Rewards[3] = 40000000;
                    Progresses[3] = 0;
                    maxProgresses[3] = 1;
                    achBases[3].GetComponentsInChildren<RawImage>()[1].texture = sprites[13];
                    break;
                case 4:
                    Names[4] = "Мегамозг?";
                    Descrs[4] = "Получите 75-ый уровень.";
                    rewardShowed[4] = false;
                    rewardTaken[4] = false;
                    Rewards[4] = 75000000;
                    maxProgresses[4] = 75;
                    achBases[4].GetComponentsInChildren<RawImage>()[1].texture = sprites[17];
                    break;
            }
        }
        else if(achLvl[id] == 3)
        {
            switch (id)
            {
                case 0:
                    Names[0] = "Бог шахт";
                    Descrs[0] = "Соберите 1.000.000 осколков.";
                    rewardShowed[0] = false;
                    rewardTaken[0] = false;
                    Rewards[0] = 2000000;
                    maxProgresses[0] = 1000000;
                    achBases[0].GetComponentsInChildren<RawImage>()[1].texture = sprites[2];
                    break;
                case 1:
                    Names[1] = "Игрок Forex";
                    Descrs[1] = "Обменяйте 10.000.000 осколков.";
                    rewardShowed[1] = false;
                    rewardTaken[1] = false;
                    Rewards[1] = 25000000;
                    maxProgresses[1] = 10000000;
                    achBases[1].GetComponentsInChildren<RawImage>()[1].texture = sprites[6];
                    break;
                case 2:
                    Names[2] = "Билл Гейтс";
                    Descrs[2] = "Заработайте 50.000.000";
                    rewardShowed[2] = false;
                    rewardTaken[2] = false;
                    Rewards[2] = 25000000;
                    maxProgresses[2] = 50000000;
                    achBases[2].GetComponentsInChildren<RawImage>()[1].texture = sprites[10];
                    break;
                case 3:
                    Names[3] = "Барри Аллен?";
                    Descrs[3] = "Отправьте учёных в будущее.";
                    rewardShowed[3] = false;
                    rewardTaken[3] = false;
                    Rewards[3] = 10000000;
                    Progresses[3] = 0;
                    maxProgresses[3] = 1;
                    achBases[3].GetComponentsInChildren<RawImage>()[1].texture = sprites[14];
                    break;
                case 4:
                    Names[4] = "Превзойти Эйнштейна";
                    Descrs[4] = "Получите 100-ый уровень.";
                    rewardShowed[4] = false;
                    rewardTaken[4] = false;
                    Rewards[4] = 1000000000;
                    maxProgresses[4] = 100;
                    achBases[4].GetComponentsInChildren<RawImage>()[1].texture = sprites[18];
                    break;
            }
        }
        else if (achLvl[id] == 4)
        {
            switch (id)
            {
                case 0:
                    Names[0] = "Шахты - жизнь";
                    Descrs[0] = "Накопайте 100.000.000 осколков.";
                    rewardShowed[0] = false;
                    rewardTaken[0] = false;
                    Rewards[0] = 750000000;
                    maxProgresses[0] = 100000000;
                    achBases[0].GetComponentsInChildren<RawImage>()[1].texture = sprites[3];
                    break;
                case 1:
                    Names[1] = "Что ты такое?";
                    Descrs[1] = "Обменяйте 100.000.000 осколков.";
                    rewardShowed[1] = false;
                    rewardTaken[1] = false;
                    Rewards[1] = 100000000;
                    maxProgresses[1] = 100000000;
                    achBases[1].GetComponentsInChildren<RawImage>()[1].texture = sprites[7];
                    break;
                case 2:
                    Names[2] = "Разработчик";
                    Descrs[2] = "Заработайте 100.000.000.";
                    rewardShowed[2] = false;
                    rewardTaken[2] = false;
                    Rewards[2] = 1000000000;
                    maxProgresses[2] = 100000000;
                    achBases[2].GetComponentsInChildren<RawImage>()[1].texture = sprites[11];
                    break;
                case 3:
                    Names[3] = "Доктор Кто?";
                    Descrs[3] = "Встретьте Доктора Кто.";
                    rewardShowed[3] = false;
                    rewardTaken[3] = false;
                    Rewards[3] = 1000000000;
                    Progresses[3] = 0;
                    maxProgresses[3] = 1;
                    achBases[3].GetComponentsInChildren<RawImage>()[1].texture = sprites[15];
                    break;
                case 4:
                    Names[4] = "Чак Норрис?";
                    Descrs[4] = "Получите 200-ый уровень.";
                    rewardShowed[4] = false;
                    rewardTaken[4] = false;
                    Rewards[4] = 2000000000;
                    maxProgresses[4] = 200;
                    achBases[4].GetComponentsInChildren<RawImage>()[1].texture = sprites[19];
                    break;
            }
        }
    }

    // Инициализация ачивок
    public void InitializeAchievements()
    {
        for(int i = 0; i < achBases.Length; i++)
        {       
            achBases[i].GetComponentsInChildren<Text>()[0].text = Names[i];
            achBases[i].GetComponentsInChildren<Text>()[1].text = Descrs[i];

            achBases[i].GetComponentInChildren<Slider>().maxValue = maxProgresses[i];
            achBases[i].GetComponentInChildren<Slider>().value = Progresses[i];
            achBases[i].GetComponentInChildren<Slider>().GetComponentInChildren<Text>().text = CurrencyConverter.instance.GetCurrency(Progresses[i], "0") + "/" + CurrencyConverter.instance.GetCurrency(maxProgresses[i], "0");

            achBases[i].GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = "Награда - " + CurrencyConverter.instance.GetCurrency(Rewards[i], "0");
            achBases[i].GetComponentInChildren<Button>().GetComponentInChildren<Image>().color = Color.gray;
        }
    }
    public void InitializeImages()
    {
        for(int i = 0; i < achBases.Length; i++)
        {
            switch(i)
            {
                case 0:
                    if(achLvl[i] == 1)
                    { achBases[i].GetComponentsInChildren<RawImage>()[1].texture = sprites[0]; }
                    if (achLvl[i] == 2)
                    { achBases[i].GetComponentsInChildren<RawImage>()[1].texture = sprites[1]; }
                    if (achLvl[i] == 3)
                    { achBases[i].GetComponentsInChildren<RawImage>()[1].texture = sprites[2]; }
                    if (achLvl[i] == 4)
                    { achBases[i].GetComponentsInChildren<RawImage>()[1].texture = sprites[3]; }
                    break;
                case 1:
                    if (achLvl[i] == 1)
                    { achBases[i].GetComponentsInChildren<RawImage>()[1].texture = sprites[4]; }
                    if (achLvl[i] == 2)
                    { achBases[i].GetComponentsInChildren<RawImage>()[1].texture = sprites[5]; }
                    if (achLvl[i] == 3)
                    { achBases[i].GetComponentsInChildren<RawImage>()[1].texture = sprites[6]; }
                    if (achLvl[i] == 4)
                    { achBases[i].GetComponentsInChildren<RawImage>()[1].texture = sprites[7]; }
                    break;
                case 2:
                    if (achLvl[i] == 1)
                    { achBases[i].GetComponentsInChildren<RawImage>()[1].texture = sprites[8]; }
                    if (achLvl[i] == 2)
                    { achBases[i].GetComponentsInChildren<RawImage>()[1].texture = sprites[9]; }
                    if (achLvl[i] == 3)
                    { achBases[i].GetComponentsInChildren<RawImage>()[1].texture = sprites[10]; }
                    if (achLvl[i] == 4)
                    { achBases[i].GetComponentsInChildren<RawImage>()[1].texture = sprites[11]; }
                    break;
                case 3:
                    if (achLvl[i] == 1)
                    { achBases[i].GetComponentsInChildren<RawImage>()[1].texture = sprites[12]; }
                    if (achLvl[i] == 2)
                    { achBases[i].GetComponentsInChildren<RawImage>()[1].texture = sprites[13]; }
                    if (achLvl[i] == 3)
                    { achBases[i].GetComponentsInChildren<RawImage>()[1].texture = sprites[14]; }
                    if (achLvl[i] == 4)
                    { achBases[i].GetComponentsInChildren<RawImage>()[1].texture = sprites[15]; }
                    break;
                case 4:
                    if (achLvl[i] == 1)
                    { achBases[i].GetComponentsInChildren<RawImage>()[1].texture = sprites[16]; }
                    if (achLvl[i] == 2)
                    { achBases[i].GetComponentsInChildren<RawImage>()[1].texture = sprites[17]; }
                    if (achLvl[i] == 3)
                    { achBases[i].GetComponentsInChildren<RawImage>()[1].texture = sprites[18]; }
                    if (achLvl[i] == 4)
                    { achBases[i].GetComponentsInChildren<RawImage>()[1].texture = sprites[19]; }
                    break;
            }
        }
    }
    public void InitializeNames()
    {
        if (achLvl[0] == 0)
            Names[0] = "Начало начал";
        if (achLvl[1] == 0)
            Names[1] = "Начинающий трейдер";
        if (achLvl[2] == 0)
            Names[2] = "Богатый малый";
        if (achLvl[3] == 0)
            Names[3] = "\"Андрюха, ща засосёт!\"";
        if (achLvl[4] == 0)
            Names[4] = "Опытный";
    }
    public void InitializeDescrs()
    {
        if (achLvl[0] == 0)
            Descrs[0] = "Откопайте свой первый осколок.";
        if(achLvl[1] == 0)
            Descrs[1] = "Обменяйте 100 осколков.";
        if (achLvl[2] == 0)
            Descrs[2] = "Заработайте свою первую тысячу.";
        if (achLvl[3] == 0)
            Descrs[3] = "Столкнитесь с чёрной дырой.";
        if (achLvl[4] == 0)
            Descrs[4] = "Получите 10-ый уровень.";
    }
}
