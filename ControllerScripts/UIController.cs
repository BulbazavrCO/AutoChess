﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public void BuyUnit(UnitParametrs param)
    {
        MapController.instance.BuyUnit(param);
    }

    public void StartButtle()
    {
        MapController.instance.StartButtle();
    }
}
