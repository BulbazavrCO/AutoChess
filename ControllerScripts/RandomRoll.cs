using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRoll : MonoBehaviour
{
    public TierUnits[] Units;

    public ChanceRoll[] Chance;

    public List<UnitParametrs> Roll(int level)
    {
        float rand;
        ChanceRoll roll = Chance[level];
        List<UnitParametrs> units = new List<UnitParametrs>();

        for (int i = 0; i < 5; i++)
        {
            rand = Random.Range(1, 101);
            int index = roll.Index(rand);
            units.Add(Units[index].RandomUnit());
        }

       return units;
    }   
}

[SerializeField]
public class ChanceRoll
{
    public string level;
    public int[] chance;   

    public int Index(float rand)
    {
        int count = 0;
        for (int i = 0; i < 5; i++)
        {
            count += chance[i];
            if (count >= rand)
                return i;            
        }

        return 0;
    }
}

[SerializeField]
public class TierUnits
{
    public string tier;
    public UnitParametrs[] Units;

    public UnitParametrs RandomUnit()
    {
        int rand = Random.Range(0, Units.Length);
        return Units[rand];
    }
}



