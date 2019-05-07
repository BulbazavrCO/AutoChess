using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    public Text timeText;

    private void Start()
    {
        instance = this;
        UpdateTime("Map: 15");
    }

    public void UpdateTime(string time)
    {
        timeText.text = time;
    }

    public void BuyUnit(UnitParametrs param)
    {
        MapController.instance.BuyUnit(param);
    }    
}
