using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RandomEnemies: MonoBehaviour 
{
    public UnitParametrs param;  

    public List<UnitParametrs> EnemiesUnits()
    {
        List<UnitParametrs> par = new List<UnitParametrs>() { param };

        return par;
    }   
}
