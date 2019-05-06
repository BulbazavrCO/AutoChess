using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RandomRoll : MonoBehaviour
{
    public UnitParametrs[] AllUnits;

    private TierUnits[] Units;

    public ChanceRoll[] Chance;

    private void Start()
    {
        Units = new TierUnits[5];
        CreateTier();
    }    

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

    private void CreateTier()
    {
        List<UnitParametrs> units = new List<UnitParametrs>();
        for (int i = 0; i < Units.Length; i++)
        {
            for(int j = 0; j < AllUnits.Length; j++)
            {
                if (AllUnits[j].Level == i + 1)
                    units.Add(AllUnits[j]);
            }
            Units[i] = new TierUnits(units);
        }
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

public class TierUnits
{
    public string tier;
    public UnitParametrs[] Units;

    public TierUnits(List<UnitParametrs> units)
    {
        Units = units.ToArray();
    }

    public UnitParametrs RandomUnit()
    {
        int rand = Random.Range(0, Units.Length);
        return Units[rand];
    }
}



