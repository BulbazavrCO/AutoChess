using System.Collections;
using System.Collections.Generic;

public class Map 
{ 
    public Node[,] Grid { get; private set; }    

    public Stock stock { get; private set; }

    public List<Unit> UnionUnits { get; private set; }           

    public Map()
    {
        CreateNodes();
        UnionUnits = new List<Unit>();
        stock = new Stock(this);
    }

    private void CreateNodes()
    {
        Grid = new Node[8, 8];
        for(int i = 0; i < 8; i++)
        {
            for(int j =0; j < 8; j++)
            {
                Grid[i, j] = new Node(i,j);
            }
        }
    }

    private (int, int) CheckPositionOnMap(int x, int y)
    {        
        int d = 1;
        while (true)
        {
            for (int i = -d + x; i <= d + x; i++)
            {
                if (i >= 0 && i < 8)
                {
                    for (int j = -d + y; j <= d + y; j++)
                    {
                        if (j >= 0 && j < 4)
                        {
                            if (Grid[i, j].Cell == null)
                                return (i, j);
                        }
                    }
                }
            }
            d++;
        }        
    }

    private void CheckUnitsOnMap(Unit unit)
    {
        List<Unit> units = new List<Unit>();
        foreach (Unit u in UnionUnits)
        {
            if (u.CheckUnits(unit))
                units.Add(u);
        }

        if (units.Count > 1)
        {
            foreach (Unit u in units)
            {
                u.DestroyUnit();
            }
            unit.LevelUp();
            CheckUnitsOnMap(unit);
        }

    }

    private void CreateCellPosition(int x, int y, ICell cell)
    {
        Grid[x, y].Cell = cell;
    }

    public (int,int) MoveUnit(int x, int y, Unit unit)
    {
        CheckUnitsOnMap(unit);
        UnionUnits.Add(unit);
        if (Grid[x, y].Cell == null)
        {
            CreateCellPosition(x, y, unit);           
            
            return (x, y);
        }
        else
        {          
            (int i, int j) pos = CheckPositionOnMap(x, y);
            CreateCellPosition(pos.i, pos.j, unit);
            return pos;
        }          

    }  

    public void RemoveCell(ICell cell)
    {
        if (cell.X >= 0 && cell.Y >= 0)
        {            
            Grid[cell.X, cell.Y].Cell = null;
            Unit unit = (Unit)cell;
            if (unit != null)
                UnionUnits.Remove(unit);
        }        
    } 

    public void CreateButtle(List<Unit> enemies)
    {
       
    }

    public void EndButtle()
    {

    }

    public bool CheckButtle()
    {
        return false;
    }
}
