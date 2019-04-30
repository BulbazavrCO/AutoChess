using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AutoChess
{
    class Buttle
    {
        private List<Unit> unionUnits;
        private List<Unit> enemiesUnits;
        private Map map;

        public Buttle(List<Unit> union, List<Unit> enemies, Map map)
        {            
            unionUnits = union;
            enemiesUnits = enemies;           
            Start(unionUnits.Concat(enemiesUnits).ToList());
            this.map = map;
        }

        public void Start(List<Unit> units)
        {
            foreach(var unit in units)
            {
                unit.StartButtle();
            }
        }    
       
        public void RemoveUnit(Unit unit)
        {
            if (unit.typeCell == TypeCell.enemy)
                enemiesUnits.Remove(unit);
            else
                unionUnits.Remove(unit);
        }

        public void AddUnit(Unit unit)
        {
            if (unit.typeCell == TypeCell.enemy)
                enemiesUnits.Add(unit);
            else
                unionUnits.Add(unit);
        }

        public int CountEnemies()
        {
            return enemiesUnits.Count;
        }    
        
        public bool CheckButtle()
        {
            return unionUnits.Count == 0 || enemiesUnits.Count == 0;
        }

        public void EndButtle()
        {
            List<Unit> units = unionUnits.Concat(enemiesUnits).ToList();
            foreach(var unit in units)
            {
                unit.DestroyUnit();
            }
        }
    }
}
