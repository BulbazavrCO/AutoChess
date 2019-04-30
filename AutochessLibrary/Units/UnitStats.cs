using System.Collections;
using System.Collections.Generic;

namespace AutoChess.Parametrs
{

    #region AllClassUnits

    public enum ClassUnits
    {

    }

    #endregion

    #region AllRaceUnits

    public enum RaceUnits
    {

    }

    #endregion

    #region TypeAttakUnit

    public enum TypeAttakUnit
    {
        range,
        melee
    }

    #endregion

    [System.Serializable]
    public class UnitStats
    {
        public float maxMP;

        public int LevelUnit;

        public string name;

        public int armor;
        public int magicArmor;
        public int attakRange = 1;
        public int cost = 1;

        public Parametrs[] parametrs;

        public RaceUnit[] AllRace;

        public ClassUnit[] AllClasses;

        public TypeAttakUnit Attaktype;

        public float GetHP(int index)
        {
            return parametrs[index].maxHP;
        }

        public float GetDamage(int index)
        {
            return parametrs[index].damage;
        }

        public float GettAttakSpeed(int index)
        {
            return parametrs[index].attakSpeed;
        }
    }

    #region UnitParametrs

    [System.Serializable]
    public class Parametrs
    {
        public float damage;

        public float maxHP;

        public float attakSpeed = 1;
    }

    [System.Serializable]
    public class ClassUnit
    {
        public ClassUnits Race;

        public string discription;

        public float value;
    }

    [System.Serializable]
    public class RaceUnit
    {
        public RaceUnits Race;

        public string discription;

        public float value;
    }

    #endregion
}
