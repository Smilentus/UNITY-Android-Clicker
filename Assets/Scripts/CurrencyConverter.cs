using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyConverter : MonoBehaviour
{
    public static CurrencyConverter instance;

    private void Awake()
    {
        CreateInstance();
    }

    // Конвертер валюты, объяснять не нужно
    void CreateInstance()
    {
        if (instance == null)
            instance = this;
    }
    public string GetCurrency(float valueToConvert, string tostr)
    {
        string convertedValue;
        if (valueToConvert >= 1000000000000000000)
            convertedValue = (valueToConvert / 1000000000000000000f).ToString("f2") + "N";
        else if (valueToConvert >= 1000000000000000)
            convertedValue = (valueToConvert / 1000000000000000f).ToString("f2") + "O";
        else if (valueToConvert >= 1000000000000)
            convertedValue = (valueToConvert / 1000000000000f).ToString("f2") + "Q";
        else if (valueToConvert >= 1000000000)
            convertedValue = (valueToConvert / 1000000000f).ToString("f2") + "T";
        else if (valueToConvert >= 1000000)
            convertedValue = (valueToConvert / 1000000f).ToString("f2") + "M";
        else if (valueToConvert >= 1000)
            convertedValue = (valueToConvert / 1000f).ToString("f2") + "K";
        else
            convertedValue = valueToConvert.ToString(tostr);
        return convertedValue;
    } 
    public string GetShardsValue(float  valueToConvert)
    {
        string convertedValue;
        if (valueToConvert >= 1000000000000000000)
            convertedValue = (valueToConvert / 1000000000000000000f).ToString("f3") + "N";
        else if (valueToConvert >= 1000000000000000)
            convertedValue = (valueToConvert / 1000000000000000f).ToString("f3") + "O";
        else if (valueToConvert >= 1000000000000)
            convertedValue = (valueToConvert / 1000000000000f).ToString("f3") + "Q";
        else if (valueToConvert >= 1000000000)
            convertedValue = (valueToConvert / 1000000000f).ToString("f3") + "T";
        else if (valueToConvert >= 1000000)
            convertedValue = (valueToConvert / 1000000f).ToString("f3") + "M";
        else if (valueToConvert >= 1000)
            convertedValue = (valueToConvert / 1000f).ToString("f3") + "K";
        else
            convertedValue = valueToConvert.ToString("f3");
        return convertedValue;
    }
}
