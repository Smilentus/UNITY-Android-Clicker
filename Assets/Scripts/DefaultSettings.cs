using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultSettings : MonoBehaviour
{
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
}
