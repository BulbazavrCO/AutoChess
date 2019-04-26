using System.Collections;
using System.Collections.Generic;

namespace AutoChess
{
    public class Unit : ICell
    {
        public int X { get; private set; }

        public int Y { get; private set; }

        public int Level { get; private set; }

        public TypeCell typeCell { get; private set; }

        private IUnit control;

        private Map map;

        private bool dead;

        #region Stats
        public UnitParametrs info { get; private set; }

        private float hp;
        private float mp;
        private float armor;
        private float magicArmor;
        private float damage;
        private float attakSpeed;
        #endregion


        public Unit(UnitParametrs param)
        {
            Level = 1;
            info = param;
        }

        public Unit(UnitParametrs param, Map map, int x, int y, TypeCell type)
        {
            dead = false;
            Level = 1;
            this.map = map;
            info = param;
            CreateXY(x, y);
            typeCell = type;

            UpdateStats();
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

            if (hp + heal > info.parametrs[Level - 1].maxHP)
            {
                hp = info.parametrs[Level - 1].maxHP;
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
            return unit != null && typeCell == unit.typeCell && unit.Level < 3 && this != unit && Level == unit.Level && info.name == unit.info.name;
        }

        public void RemoveUnitPosition()
        {
            if (Y < 0)
                map.stock.RemoveUnit(this);
            else
            {
                map.RemoveCell(this);
                map.RemoveUnit(this);
            }
        }

        public void DestroyUnit()
        {
            RemoveUnitPosition();
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
        }

        private void UpdateStats()
        {
            hp = info.parametrs[Level - 1].maxHP;
            damage = info.parametrs[Level - 1].damage;
            armor = info.armor;
            magicArmor = info.magicArmor;
            mp = info.maxMP;
            attakSpeed = info.parametrs[Level - 1].attakSpeed;
        }

        private void CreateXY(int x, int y)
        {
            X = x;
            Y = y;
        }

        private Unit CheckAttak()
        {
            return map.GetUnit(typeCell, info.attakRange, X, Y);
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
