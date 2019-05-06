using System.Collections;
using System.Collections.Generic;

namespace AutoChess
{
    public class Map
    {
        private Node[,] Grid;

        public Stock stock { get; private set; }

        public List<Unit> UnionUnits { get; private set; }        

        public List<Unit> CloneUnits { get; private set; }

        private Buttle buttle;

        private int mapTime;

        private int buttleTime;

        public int time { get; private set; }

        public Map(int MapTime, int ButtleTime)
        {
            CreateNodes();
            UnionUnits = new List<Unit>();
            CloneUnits = new List<Unit>();
            stock = new Stock(this);            
            mapTime = MapTime;
            buttleTime = ButtleTime;
            time = mapTime;
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
            RemoveCell(unit);
        }     

        public void CreateButtle(List<Unit> enemies)
        {
            CloneUnits.Clear();
            buttle = new Buttle(UnionUnits, enemies, this);                     
            time = buttleTime;           

            foreach(Unit u in UnionUnits)
            {
                CloneUnits.Add(u);
            }
        }

        public void EndButtle()
        {
            buttle.EndButtle();            
            buttle = null;
            time = mapTime;            
        }

        public bool CheckButtle()
        {
            return (!buttle.CheckButtle() && CheckTime());
        }       

        public Unit GetUnit(TypeCell type, int rangeAttak, int x, int y)
        {
            for (int i = x-rangeAttak; i <= x+rangeAttak; i++)
            {
                for(int j = y-rangeAttak; j <= y+rangeAttak; j++)
                {
                    if (i >= 0 && i < 8 && j >= 0 && j < 8)
                    {
                        Unit unit = (Unit)Grid[i, j].Cell;                        

                        if (unit == null)
                            continue;                       

                        if (unit.typeCell != type && unit.CheckToDamage())                            
                            return unit;                       
                    }
                }
            }
            return null;
        }       

        public bool CheckTime()
        {
            return time > 0;
        }

        public void UpdateTime()
        {
            time--;
        }

        public List<Node> GetNeighbours(Node node)
        {
            List<Node> neighbours = new List<Node>();

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0)
                        continue;

                    int checkX = node.X + x;
                    int checkY = node.Y + y;

                    if (checkX >= 0 && checkX < 7 && checkY >= 0 && checkY < 7)
                    {
                        neighbours.Add(Grid[checkX, checkY]);
                    }
                }
            }

            return neighbours;
        }

        public (int,int) GetUnitToMove(Unit unit)
        {
            int r = 1;
            while(r < 8)
            {
                
            }
            return (unit.X, unit.Y);
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
