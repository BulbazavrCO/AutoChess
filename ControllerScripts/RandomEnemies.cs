using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RandomEnemies: MonoBehaviour 
{
    public UnitParametrs[] Parametrs;  

    public List<UnitParametrs> EnemiesUnits()
    {
        List<UnitParametrs> par = new List<UnitParametrs>() { Parametrs[0], Parametrs[0], Parametrs[1] };

        return par;
    }   

    public UnitParametrs GetParametrs(int id)
    {
        UnitParametrs param = null;
        for(int i = 0; i < Parametrs.Length; i++)
        {
            if (Parametrs[i].ID == id)
                param = Parametrs[i];
        }
        return param;
    }
}
