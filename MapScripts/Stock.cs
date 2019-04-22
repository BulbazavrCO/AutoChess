using System.Collections;
using System.Collections.Generic;

public class Stock
{
    private Map map;

    public Unit[] backUnits { get; private set; }

    List<Unit> unitsMach;

    public Stock(Map map)
    {
        this.map = map;
        backUnits = new Unit[8];        
    }   

    public int OnStock(Unit unit)
    {
        if (LevelUpUnit(unit))
            return OnStock(unit);

        for (int i = 0; i < backUnits.Length; i++)
        {
            if (backUnits[i] == null)
            {
                backUnits[i] = unit;               

                return i;
            }
        }        
        return 0;
    }

    private bool LevelUpUnit(Unit unit)
    {
        unitsMach = CheckUnits(unit);
        if (unitsMach.Count > 1)
        {            
            foreach (Unit u in unitsMach)
            {
                u.DestroyUnit();
            }
            unit.LevelUp();
            unitsMach.Clear();
            return true;
        }
        return false;
    }

    private List<Unit> CheckUnits(Unit unit)
    {
        List<Unit> units = new List<Unit>();

        for (int i = 0; i < backUnits.Length; i++)
        {
            if (unit.CheckUnits(backUnits[i]))
                units.Add(backUnits[i]);
        }

        return units;
    }    

    public bool CheckStock(Unit unit)
    {       
        unitsMach = CheckUnits(unit);        

        for (int i = 0; i < backUnits.Length; i++)
        {
            if (backUnits[i] == null)
            {
                return true;
            }
        }

        return unitsMach.Count > 1;
    }

    public void RemoveUnit(Unit unit)
    {
        for (int i = 0; i < backUnits.Length; i++)
        {
            if (backUnits[i] == unit)
            {
                backUnits[i] = null;
                return;
            }
        }
    }


}
