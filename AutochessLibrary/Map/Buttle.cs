using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace AutoChess
{
    public class Buttle
    {
        private List<Unit> unionUnits;
        private List<Unit> enemiesUnits;

        public Buttle(List<Unit> union, List<Unit> enemies)
        {
            unionUnits = union;
            enemiesUnits = enemies;
            Start(union.Concat(enemies).ToList());
        }

        public void Start(List<Unit> units)
        {
            foreach(var unit in units)
            {
                unit.StartButtle(this);
            }
        }
        
     
        public Unit GetUnit(TypeCell type)
        {
            if (type == TypeCell.union)
                return enemiesUnits[0];

            return unionUnits[0];
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
