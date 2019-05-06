using System.Collections;
using System.Collections.Generic;
using AutoChess.Parametrs;

namespace AutoChess
{    
    public class Unit : ICell
    {
        public int ID { get; private set; }

        public int X { get; private set; }

        public int Y { get; private set; }

        public int Level { get; private set; }

        public TypeCell typeCell { get; private set; }

        private IUnit control;

        public Map map;

        private bool dead;

        #region Stats
        public UnitStats stats { get; private set; }

        private float hp;
        private float mp;
        private float armor;
        private float magicArmor;
        private float damage;
        private float attakSpeed;
        #endregion


        public Unit(UnitStats param)
        {
            Level = 1;
            stats = param;
        }           

        public Unit(UnitStats param, Map map, int x, int y, TypeCell type, int id)
        {
            dead = false;
            Level = 1;
            this.map = map;
            stats = param;
            CreateXY(x, y);
            typeCell = type;
            ID = id;          
        }

        public void Damage(float value)
        {
            float damageIn = value;

            if (damageIn < 0)
                damageIn = 0;

            if (hp - damageIn < 0)
            {
                hp = 0;
                dead = true;
                control.Dead();
                map.RemoveUnitInButtle(this);
            }
            else
            {
                hp -= damageIn;
            }

            control.UpdateHealth(hp);
        }

        public void Heal(float value)
        {
            float heal = value;

            if (heal < 0)
                heal = 0;

            if (hp + heal > stats.GetHP(Level - 1))
            {
                hp = stats.GetHP(Level - 1);
            }
            else
            {
                hp += heal;
            }

            control.UpdateHealth(hp);
        }

        public (int, int) CreateTo(float x, float y)
        {
            int checkX = CheckX((int)x);
            int checkY = CheckY((int)y);

            if (y < 0)            
                return OnStock();

            CreateXY(checkX, checkY);
            if(typeCell == TypeCell.union)
            return map.CreateUnionMapUnit(checkX, checkY, this);

            return map.CreateUnitOnMap(checkX, checkY, this);
        }

        public void LevelUp()
        {
            Level++;
            UpdateStats();
            control.LevelUp(Level);
        }

        public bool CheckUnits(Unit unit)
        {
            return unit != null && typeCell == unit.typeCell && unit.Level < 3 && this != unit && Level == unit.Level && stats.name == unit.stats.name;
        }

        public void RemoveUnitPosition()
        {
            if (Y < 0)
                map.stock.RemoveUnit(this);
            else
            {
                map.RemoveCell(this);
                map.RemoveUnitOnMap(this);
            }
        }

        public void DestroyUnit()
        {
            RemoveUnitPosition();
            control.DestroyUnit();
        } 
        
        public void DestroyInButtle()
        {
            map.RemoveUnitInButtle(this);
            control.DestroyUnit();
        }

        public void ActionUnit()
        {
            Unit enemyUnit = CheckAttak();
            if (enemyUnit != null)
                control.Attak(enemyUnit, attakSpeed, Damage());
            else
                control.Move(Move().Item1, Move().Item2);
        }

        public void StartButtle()
        {
            control.StartButtle();
        }

        public void AddControl(IUnit control)
        {
            this.control = control;
            UpdateStats();
        }

        public bool CheckToDamage()
        {
            return !dead;
        }

        private void UpdateStats()
        {
            hp = stats.GetHP(Level-1);
            damage = stats.GetDamage(Level - 1);
            armor = stats.armor;
            magicArmor = stats.magicArmor;
            mp = stats.maxMP;
            attakSpeed = stats.GettAttakSpeed(Level-1);
        }

        private void CreateXY(int x, int y)
        {
            X = x;
            Y = y;
        }

        private Unit CheckAttak()
        {
            return map.GetUnit(typeCell, stats.attakRange, X, Y);
        }

        private float Damage()
        {
            return damage;
        }

        private (int, int) OnStock()
        {
            if (map.stock.CheckStock(this))
            {              
                X = -1;
                Y = -1;
                return (map.stock.OnStock(this), -1);
            }
            else
                return (X, Y);
        }

        private (int, int) Move()
        {
            return (0, 0);
        }       

        private int CheckX(int x)
        {
            if (x < 0)
                return 0;
            if (x > 7)
                return 7;
            return x;
        }

        private int CheckY(int y)
        {
            if (y > 3)
                return 3;
            return y;
        }
    }
}
