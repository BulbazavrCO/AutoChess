using System.Collections;
using System.Collections.Generic;

namespace AutoChess
{
    public class Map
    {
        private Node[,] Grid;

        public Stock stock { get; private set; }

        public List<Unit> UnionUnits { get; private set; }

        private Buttle buttle;

        public Map()
        {
            CreateNodes();
            UnionUnits = new List<Unit>();
            stock = new Stock(this);
        }      

        public (int, int) CreateUnionMapUnit(int x, int y, Unit unit)
        {
            CheckUnitsOnMap(unit);
            UnionUnits.Add(unit);
            return CreateUnitOnMap(x, y, unit);
        }

        public void RemoveUnitOnMap(Unit unit)
        {
            UnionUnits.Remove(unit);
        }

        public (int, int) CreateUnitOnMap(int x, int y, Unit unit)
        {
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
            Grid[cell.X, cell.Y].Cell = null;
        }

        public (int, int) MoveUnit(int x, int y)
        {
            return (0, 0);
        }       

        public void AddUnitInButtle(Unit unit)
        {
            buttle.AddUnit(unit);
        }

        public void RemoveUnitInButtle(Unit unit)
        {
            buttle.RemoveUnit(unit);
        }     

        public void CreateButtle(List<Unit> enemies)
        {
            buttle = new Buttle(UnionUnits, enemies, this);
        }

        public void EndButtle()
        {
            buttle.EndButtle();
            buttle = null;
        }

        public bool CheckButtle()
        {
            return buttle.CheckButtle();
        }

        public List<Unit> GetButtleUnits()
        {
            return null;
        }

        public Unit GetUnit(TypeCell type, int rangeAttak, int x, int y)
        {
            for (int i = x-rangeAttak; i <= x+rangeAttak; i++)
            {
                for(int j = y-rangeAttak; j <= y+rangeAttak; j++)
                {
                    if (i >= 0 && i < 8 && j >= 0 && j < 8)
                    {
                        ICell cell = Grid[i, j].Cell;                        

                        if (cell == null)
                            continue;

                        if (cell.typeCell == TypeCell.netral)
                            continue;

                        if (cell.typeCell != type)                            
                            return (Unit)cell;                       
                    }
                }
            }
            return null;
        }

        private void CreateNodes()
        {
            Grid = new Node[8, 8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Grid[i, j] = new Node(i, j);
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
    }
}
