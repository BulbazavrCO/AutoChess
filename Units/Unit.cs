using System.Collections;
using System.Collections.Generic;

public class Unit : ICell
{
    public int X { get; private set; }

    public int Y { get; private set; }

    public int Level { get; private set; }

    public TypeCell typeCell { get; private set; }

    private IUnit control;

    private Map map;

    public Buttle buttle { get; private set; }

    #region Stats
    public UnitParametrs info { get; private set; }

    private float hp;
    private float mp;
    private float armor;
    private float magicArmor;
    private float damage;
    #endregion


    public Unit(UnitParametrs param)
    {
        Level = 1;
        info = param;
    }

    public Unit(UnitParametrs param, Map map, int x, int y, IUnit control, TypeCell type)
    {
        Level = 1;
        this.map = map;
        info = param;
        CreateXY(x, y);
        this.control = control;
        typeCell = type;

        hp = param.parametrs[Level].maxHP;
        damage = param.parametrs[Level].damage;
        armor = param.armor;
        magicArmor = param.magicArmor;
        mp = param.maxMP;
    }

    public void Damage(float value)
    {
        damage = value;

        if (damage < 0)
            damage = 0;

        if (hp - damage < 0)
        {
            hp = 0;
        }
        else
        {
            hp -= damage;
        }

        control.UpdateHealth(hp);
    }

    public void Heal(float value)
    {
        float heal = value;

        if (heal < 0)
            heal = 0;

        if(hp + heal > info.parametrs[Level].maxHP)
        {
            hp = info.parametrs[Level].maxHP;
        }
        else
        {
            hp += heal;
        }

        control.UpdateHealth(hp);
    }

    public (int,int) MoveTo(float x, float y)
    {
        if (y < 0)
            return OnStock();
       
            return Move(x, y);
    }

    private (int, int) OnStock()
    {
        if (map.stock.CheckStock(this))
        {
            map.RemoveCell(this);
            X = -1;
            Y = -1;
            return (map.stock.OnStock(this), -1);
        }
        else
            return (X, Y);
    }

    private (int x, int y) Move(float x, float y)
    {
        int checkX = CheckX((int)x);
        int checkY = CheckY((int)y);
        RemoveUnitPosition();
        (int posX, int posY) pos = map.MoveUnit(checkX, checkY, this);        
        CreateXY(pos.posX, pos.posY);
        return pos;
    }

    public void LevelUp()
    {
        Level++;
        control.LevelUp(Level);
    }

    public bool CheckUnits(Unit unit)
    {
        return unit != null && typeCell == unit.typeCell && unit.Level < 3 && this != unit && Level == unit.Level && info.name == unit.info.name;
    }

    public void RemoveUnitPosition()
    {
        map.stock.RemoveUnit(this);
        map.RemoveCell(this);
    }

    public void DestroyUnit()
    {
        RemoveUnitPosition();
        control.DestroyUnit();
    }

    private void CreateXY(int x, int y)
    {
        X = x;
        Y = y;
    }    

    public Unit GetEnemy()
    {
        if (typeCell == TypeCell.enemy)
            return buttle.UnionUnits[0];
        return buttle.UnionUnits[0];
    }

    public void StartButtle()
    {
        control.StartButtle();
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
